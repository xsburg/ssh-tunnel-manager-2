// ***********************************************************************
// <author>Stephan Burguchev</author>
// <copyright company="Stephan Burguchev">
//   Copyright (c) Stephan Burguchev 2012-2013. All rights reserved.
// </copyright>
// <summary>
//   PLinkConnectionTests.cs
// </summary>
// ***********************************************************************

using System;
using System.Collections.Generic;
using System.Threading;
using NUnit.Framework;
using SharpTestsEx;
using STM.Core.Data;

namespace STM.Core.Tests
{
    [TestFixture]
    public class PLinkConnectionTests
    {
        [Test]
        public void It_should_open_connection()
        {
            var connection = CreateValidConnection();
            var connector = new PLinkConnection(connection);
            var observer = new ConnectionObserver();
            connector.Observer = observer;
            connector.Open();
            observer.AppliedStates.Should().Contain(ConnectionState.Opening);
            observer.AppliedStates.Should().Contain(ConnectionState.Opened);
            observer.AppliedStates.Should().Contain(ConnectionState.Closing);
            observer.AppliedStates.Should().Contain(ConnectionState.Closed);
        }

        [Test]
        public void It_should_handle_invalid_password()
        {
            var connection = CreateValidConnection();
            connection.Password = "foo";
            var connector = new PLinkConnection(connection);
            var observer = new ConnectionObserver();
            connector.Observer = observer;
            connector.Open();
            observer.FatalError.Should().Be.EqualTo("Access Denied");
            observer.AppliedStates.Should().Contain(ConnectionState.Opening);
            observer.AppliedStates.Should().Contain(ConnectionState.Closing);
            observer.AppliedStates.Should().Contain(ConnectionState.Closed);
        }

        [Test]
        public void It_should_handle_invalid_hostName()
        {
            var connection = CreateValidConnection();
            connection.HostName = "foo";
            var connector = new PLinkConnection(connection);
            var observer = new ConnectionObserver();
            connector.Observer = observer;
            connector.Open();
            WaitForAsyncHandlers();
            observer.FatalError.Should().Be.EqualTo("Unable to open connection: Host does not exist");
            observer.AppliedStates.Should().Contain(ConnectionState.Opening);
            observer.AppliedStates.Should().Contain(ConnectionState.Closed);
        }

        private static void WaitForAsyncHandlers()
        {
            Thread.Sleep(1000);
        }

        private static ConnectionInfo CreateValidConnection()
        {
            var connection = new ConnectionInfo
                {
                    Name = "SDF.org",
                    HostName = "sdf.org",
                    Port = 22,
                    UserName = "stmut",
                    Password = "test"
                };
            return connection;
        }

        private class ConnectionObserver : IConnectionObserver
        {
            private readonly Action<IConnection> stateChanged;
            private readonly List<ConnectionState> appliedStates;

            public ConnectionObserver(Action<IConnection> stateChanged = null)
            {
                this.stateChanged = stateChanged;
                this.appliedStates = new List<ConnectionState>();
            }

            public ConnectionState[] AppliedStates
            {
                get
                {
                    return this.appliedStates.ToArray();
                }
            }

            public void HandleFatalError(string errorMessage)
            {
                FatalError = errorMessage;
                Console.WriteLine("[Fatal error] {0}", errorMessage);
            }

            public string FatalError { get; private set; }

            public void HandleForwardingError(IConnection sender, TunnelInfo tunnel, string errorMessage)
            {
                Console.WriteLine("Forwarding error for the tunnel {0}: {1}", tunnel.DisplayText, errorMessage);
            }

            public void HandleMessage(IConnection sender, MessageSeverity severity, string message)
            {
                Console.WriteLine("[{0}] {1}", severity, message);
            }

            public void HandleStateChanged(IConnection sender)
            {
                Console.WriteLine("[CONNECTION {0}]", sender.State.ToString().ToUpper());
                this.appliedStates.Add(sender.State);
                if (sender.State == ConnectionState.Opened)
                {
                    sender.Close();
                }

                if (this.stateChanged != null)
                {
                    this.stateChanged(sender);
                }
            }
        }
    }
}
