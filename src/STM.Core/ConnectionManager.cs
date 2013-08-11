// ***********************************************************************
// <author>Stephan Burguchev</author>
// <copyright company="Stephan Burguchev">
//   Copyright (c) Stephan Burguchev 2012-2013. All rights reserved.
// </copyright>
// <summary>
//   ConnectionManager.cs
// </summary>
// ***********************************************************************

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using STM.Core.Data;

namespace STM.Core
{
    public class ConnectionManager : IConnectionObserver
    {
        private readonly List<ConnectionInternal> activeConnections = new List<ConnectionInternal>();
        private readonly IConnectionFactory connectionFactory;
        private readonly List<ConnectionInternal> pendingConnections = new List<ConnectionInternal>();
        private readonly UserSettingsManager userSettings;

        public ConnectionManager(IConnectionFactory connectionFactory, UserSettingsManager userSettings)
        {
            if (connectionFactory == null)
            {
                throw new ArgumentNullException("connectionFactory");
            }

            if (userSettings == null)
            {
                throw new ArgumentNullException("userSettings");
            }

            this.connectionFactory = connectionFactory;
            this.userSettings = userSettings;
        }

        public void Close(ConnectionInfo connectionInfo)
        {
            if (connectionInfo == null)
            {
                throw new ArgumentNullException("connectionInfo");
            }

            var connection = this.activeConnections.FirstOrDefault(c => c.Connection.Info.Equals(connectionInfo)) ??
                             this.pendingConnections.FirstOrDefault(c => c.Connection.Info.Equals(connectionInfo));
            if (connection == null)
            {
                return;
            }

            this.activeConnections.Remove(connection);
            this.pendingConnections.Remove(connection);
            connection.Connection.Close();
        }

        public void HandleFatalError(IConnection sender, string errorMessage)
        {
            var connection = this.FindConnection(sender);
            if (connection != null)
            {
                connection.SequencedFailsCount++;
            }
        }

        public void Open(ConnectionInfo connectionInfo)
        {
            if (connectionInfo.Parent != null)
            {
                this.Open(connectionInfo.Parent);
            }

            var connection = this.connectionFactory.CreateConnection();
            connection.Info = connectionInfo;
            connection.Observer = this;
            this.pendingConnections.Add(new ConnectionInternal(connection));
            connection.Open();
        }

        void IConnectionObserver.HandleForwardingError(IConnection sender, TunnelInfo tunnel, string errorMessage)
        {
        }

        void IConnectionObserver.HandleMessage(IConnection sender, MessageSeverity severity, string message)
        {
        }

        void IConnectionObserver.HandleStateChanged(IConnection sender)
        {
            ConnectionInternal connection;
            switch (sender.State)
            {
            case ConnectionState.Opened:
                connection = this.pendingConnections.FirstOrDefault(c => c.Connection == sender);
                if (connection != null && this.activeConnections.All(c => c != connection))
                {
                    this.pendingConnections.Remove(connection);
                    this.activeConnections.Add(connection);
                    connection.SequencedFailsCount = 0;
                }

                break;
            case ConnectionState.Closed:
                connection = this.activeConnections.FirstOrDefault(c => c.Connection == sender);
                if (connection != null)
                {
                    this.activeConnections.Remove(connection);
                    this.TryToRestartLostConnection(connection);
                }

                break;
            }
        }

        private ConnectionInternal FindConnection(IConnection sender)
        {
            return this.activeConnections.FirstOrDefault(c => c.Connection.Info.Equals(sender.Info)) ??
                   this.pendingConnections.FirstOrDefault(c => c.Connection.Info.Equals(sender.Info));
        }

        private void TryToRestartLostConnection(ConnectionInternal connection)
        {
            if (!this.userSettings.Settings.RestartLostConnections ||
                connection.SequencedFailsCount > this.userSettings.Settings.AttemptsToRestartLostConnection)
            {
                return;
            }

            if (this.userSettings.Settings.LostConnectionRestartInterval == TimeSpan.Zero)
            {
                connection.Connection.Open();
            }
            else
            {
                // ReSharper disable once NotAccessedVariable
                Timer timer;
                timer = new Timer(
                    s =>
                    {
                        timer = null;
                        connection.Connection.Open();
                    },
                    null,
                    this.userSettings.Settings.LostConnectionRestartInterval,
                    TimeSpan.FromSeconds(-1));
            }
        }

        private class ConnectionInternal
        {
            public ConnectionInternal(IConnection connection)
            {
                this.Connection = connection;
            }

            public IConnection Connection { get; private set; }
            public int SequencedFailsCount { get; set; }
        }
    }
}
