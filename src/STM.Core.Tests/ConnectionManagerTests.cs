// ***********************************************************************
// <author>Stephan Burguchev</author>
// <copyright company="Stephan Burguchev">
//   Copyright (c) Stephan Burguchev 2012-2013. All rights reserved.
// </copyright>
// <summary>
//   ConnectionManagerTests.cs
// </summary>
// ***********************************************************************

using System.Collections.Generic;
using Moq;
using Ninject;
using NUnit.Framework;
using STM.Core.Data;

namespace STM.Core.Tests
{
    [TestFixture]
    public class ConnectionManagerTests
    {
        private StandardKernel kernel;

        [SetUp]
        public void SetUp()
        {
            this.kernel = new StandardKernel();
        }

        [Test]
        public void It_should_open_connection()
        {
            int index = 0;
            var connectionsToReturn = new List<IConnection>();
            var connectionFactoryMock = new Mock<IConnectionFactory>();
            connectionFactoryMock.Setup(cf => cf.CreateConnection()).Returns(() => connectionsToReturn[index++]);
            this.kernel.Bind<IConnectionFactory>().ToConstant(connectionFactoryMock.Object);

            var connectionMock = new Mock<IConnection>();
            connectionMock.Setup(c => c.Open());
            connectionMock.SetupProperty(c => c.State, ConnectionState.Closed);
            connectionMock.SetupProperty(c => c.Observer);
            connectionMock.SetupProperty(c => c.Info);
            var connection = connectionMock.Object;
            
            var cm = kernel.Get<ConnectionManager>();

            cm.Open(
                new ConnectionInfo
                    {
                        Name = "connection 1"
                    });
            cm.Open(
                new ConnectionInfo
                    {
                        Name = "connection 2"
                    });
        }
    }
}