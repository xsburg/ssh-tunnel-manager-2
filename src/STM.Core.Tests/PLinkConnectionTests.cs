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
            var connection = new ConnectionInfo
                {
                    Name = "SDF.org",
                    HostName = "sdf.org",
                    Port = 22,
                    UserName = "stmut",
                    Password = "test",
                    //SharedSettings = new SharedConnectionSettings("default")
                };
            var appliedStated = new List<ConnectionState>();
            var connector = new PLinkConnection(connection);
            connector.Observer = new ConnectionObserver(
                c =>
                {
                    Console.WriteLine("[CONNECTION {0}]", c.State.ToString().ToUpper());
                    appliedStated.Add(c.State);
                    if (c.State == ConnectionState.Opened)
                    {
                        c.Close();
                    }
                });
            connector.Open();
            appliedStated.Should().Contain(ConnectionState.Opening);
            appliedStated.Should().Contain(ConnectionState.Opened);
            appliedStated.Should().Contain(ConnectionState.Closing);
            appliedStated.Should().Contain(ConnectionState.Closed);
        }

        private class ConnectionObserver : IConnectionObserver
        {
            private readonly Action<IConnection> stateChanged;

            public ConnectionObserver(Action<IConnection> stateChanged)
            {
                this.stateChanged = stateChanged;
            }

            public void HandleFatalError(string errorMessage)
            {
                Console.WriteLine("[Fatal error] {0}", errorMessage);
            }

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
                if (this.stateChanged != null)
                {
                    this.stateChanged(sender);
                }
            }
        }
    }
}
