// ***********************************************************************
// <author>Stephan Burguchev</author>
// <copyright company="Stephan Burguchev">
//   Copyright (c) Stephan Burguchev 2012-2013. All rights reserved.
// </copyright>
// <summary>
//   ConnectionManagerTests.cs
// </summary>
// ***********************************************************************

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Moq;
using Ninject;
using NUnit.Framework;
using SharpTestsEx;
using STM.Core.Data;

namespace STM.Core.Tests
{
    [TestFixture]
    public class ConnectionManagerTests
    {
        private List<Mock<IConnection>> connectionMocks;
        private List<ConnectionState> connectionStates;
        private StandardKernel kernel;

        [Test]
        public void It_should_open_connection()
        {
            this.BuildConnectionMock();

            var cm = this.kernel.Get<ConnectionManager>();
            cm.Open(
                new ConnectionInfo
                    {
                        Name = "connection 1"
                    });

            var connectionMock = this.connectionMocks[0];
            connectionMock.Verify(c => c.Open(), Times.Once());
            cm.ActiveConnections.Should().Have.Count.EqualTo(1);
            cm.ActiveConnections.First().Should().Be.EqualTo(connectionMock.Object.Info);
        }

        [Test]
        public void It_should_open_parent_connection_first()
        {
            this.BuildConnectionMock();
            this.BuildConnectionMock();

            var parentConnectionInfo = new ConnectionInfo
                {
                    Name = "Parent Connection"
                };
            var childConnectionInfo = new ConnectionInfo
                {
                    Name = "Child Connection",
                    Parent = parentConnectionInfo
                };
            var cm = this.kernel.Get<ConnectionManager>();
            cm.Open(childConnectionInfo);

            var parentConnectionMock = this.connectionMocks[0];
            var childConnectionMock = this.connectionMocks[1];
            parentConnectionMock.Verify(c => c.Open(), Times.Once());
            childConnectionMock.Verify(c => c.Open(), Times.Once());
            cm.ActiveConnections.Should().Have.SameSequenceAs(parentConnectionInfo, childConnectionInfo);
        }

        [Test]
        public void It_should_restart_unintentionally_closed_connection()
        {
            this.BuildConnectionMock();

            var usm = this.kernel.Get<IUserSettingsManager>();
            usm.Settings.LostConnectionRestartInterval = TimeSpan.FromMilliseconds(100);
            var cm = this.kernel.Get<ConnectionManager>();
            cm.Open(
                new ConnectionInfo
                    {
                        Name = "connection 1"
                    });
            this.CloseConnection(0);

            Thread.Sleep(TimeSpan.FromMilliseconds(500));
            var connectionMock = this.connectionMocks[0];
            connectionMock.Verify(c => c.Open(), Times.Exactly(2));
            cm.ActiveConnections.Should().Have.Count.EqualTo(1);
            cm.ActiveConnections.First().Should().Be.EqualTo(connectionMock.Object.Info);
        }

        [SetUp]
        public void SetUp()
        {
            this.kernel = new StandardKernel();
            this.kernel.Bind<IUserSettingsManager>().To<UserSettingsManager>().InSingletonScope();
            this.BuildConnectionFactoryMock();
            this.connectionStates = new List<ConnectionState>();
            this.connectionMocks = new List<Mock<IConnection>>();
        }

        private void BuildConnectionFactoryMock()
        {
            int index = 0;
            var connectionFactoryMock = new Mock<IConnectionFactory>();
            connectionFactoryMock.Setup(cf => cf.CreateConnection()).Returns(() => this.connectionMocks[index++].Object);
            this.kernel.Bind<IConnectionFactory>().ToConstant(connectionFactoryMock.Object);
        }

        private void BuildConnectionMock()
        {
            var connectionMock = new Mock<IConnection>();
            int index = this.connectionStates.Count;
            this.connectionStates.Add(ConnectionState.Closed);
            this.connectionMocks.Add(connectionMock);
            connectionMock.Setup(c => c.Open()).Callback(() => this.OpenConnection(index));
            connectionMock.SetupGet(c => c.State).Returns(() => this.connectionStates[index]);
            connectionMock.SetupProperty(c => c.Observer);
            connectionMock.SetupProperty(c => c.Info);
        }

        private void CloseConnection(int index)
        {
            var connection = this.connectionMocks[index].Object;
            this.connectionStates[index] = ConnectionState.Closing;
            connection.Observer.HandleStateChanged(connection);
            this.connectionStates[index] = ConnectionState.Closed;
            connection.Observer.HandleStateChanged(connection);
        }

        private void OpenConnection(int index)
        {
            var connection = this.connectionMocks[index].Object;
            this.connectionStates[index] = ConnectionState.Opening;
            connection.Observer.HandleStateChanged(connection);
            this.connectionStates[index] = ConnectionState.Open;
            connection.Observer.HandleStateChanged(connection);
        }
    }
}
