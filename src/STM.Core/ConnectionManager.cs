// ***********************************************************************
// <author>Stephan Burguchev</author>
// <copyright company="Stephan Burguchev">
//   Copyright (c) Stephan Burguchev 2012-2014. All rights reserved.
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
using STM.Core.Util;

namespace STM.Core
{
    public class ConnectionManager : IConnectionObserver
    {
        private readonly List<ConnectionInternal> activeConnections = new List<ConnectionInternal>();
        private readonly IConnectionFactory connectionFactory;
        private readonly List<IConnectionManagerObserver> observers = new List<IConnectionManagerObserver>();
        private readonly List<ConnectionInternal> pendingConnections = new List<ConnectionInternal>();
        private readonly ISharedSettingsManager sharedSettingsManager;
        private readonly object syncObject = new object();
        private readonly IUserSettingsManager userSettings;

        public ConnectionManager(
            IConnectionFactory connectionFactory,
            IUserSettingsManager userSettings,
            ISharedSettingsManager sharedSettingsManager)
        {
            if (connectionFactory == null)
            {
                throw new ArgumentNullException("connectionFactory");
            }
            if (userSettings == null)
            {
                throw new ArgumentNullException("userSettings");
            }
            if (sharedSettingsManager == null)
            {
                throw new ArgumentNullException("sharedSettingsManager");
            }

            this.connectionFactory = connectionFactory;
            this.userSettings = userSettings;
            this.sharedSettingsManager = sharedSettingsManager;

            this.SyncContext = SynchronizationContext.Current;
        }

        public ConnectionInfo[] ActiveConnections
        {
            get
            {
                lock (this.syncObject)
                {
                    return this.activeConnections.Select(c => c.Connection.Info).ToArray();
                }
            }
        }

        public SynchronizationContext SyncContext { get; private set; }

        public void AddObserver(IConnectionManagerObserver observer)
        {
            if (this.observers.Contains(observer))
            {
                return;
            }

            this.observers.Add(observer);
        }

        public void Close(ConnectionInfo connectionInfo)
        {
            if (connectionInfo == null)
            {
                throw new ArgumentNullException("connectionInfo");
            }

            lock (this.syncObject)
            {
                var connection = this.FindConnection(connectionInfo);
                if (connection == null)
                {
                    return;
                }

                this.activeConnections.Remove(connection);
                this.pendingConnections.Remove(connection);
                connection.Connection.Close();
            }
        }

        public void CloseAll()
        {
            lock (this.syncObject)
            {
                this.activeConnections.Union(this.pendingConnections).ToArray().Apply(c => this.Close(c.Connection.Info));
                this.activeConnections.Clear();
                this.pendingConnections.Clear();
            }
        }

        public ConnectionState GetState(ConnectionInfo info)
        {
            lock (this.syncObject)
            {
                var connection = this.FindConnection(info);
                return connection == null
                    ? ConnectionState.Closed
                    : connection.State;
            }
        }

        public void Open(ConnectionInfo connectionInfo)
        {
            if (this.SyncContext == null)
            {
                this.SyncContext = SynchronizationContext.Current;
            }

            if (connectionInfo.Parent != null)
            {
                this.Open(connectionInfo.Parent);
            }

            lock (this.syncObject)
            {
                if (connectionInfo.SharedSettings != null)
                {
                    this.sharedSettingsManager.Save(connectionInfo.SharedSettings);
                }

                var connection = this.connectionFactory.CreateConnection();
                connection.Info = connectionInfo;
                connection.Observer = this;
                this.pendingConnections.Add(new ConnectionInternal(connection));
                connection.Open();
            }
        }

        public void RemoveObserver(IConnectionManagerObserver observer)
        {
            this.observers.Remove(observer);
        }

        void IConnectionObserver.HandleFatalError(IConnection sender, string errorMessage)
        {
            var connection = this.FindConnection(sender.Info);
            if (connection != null)
            {
                connection.SequencedFailsCount++;
            }

            this.Fire(o => o.HandleFatalError(sender.Info, errorMessage));
        }

        void IConnectionObserver.HandleForwardingError(IConnection sender, TunnelInfo tunnel, string errorMessage)
        {
            this.Fire(o => o.HandleForwardingError(sender.Info, tunnel, errorMessage));
        }

        void IConnectionObserver.HandleMessage(IConnection sender, MessageSeverity severity, string message)
        {
            this.BroadcastMessage(sender.Info, severity, message);
        }

        void IConnectionObserver.HandleStateChanged(IConnection sender)
        {
            lock (this.syncObject)
            {
                ConnectionInternal connection;
                switch (sender.State)
                {
                case ConnectionState.Open:
                    connection = this.pendingConnections.FirstOrDefault(c => c.Connection.Info.Equals(sender.Info));
                    if (connection != null && this.activeConnections.All(c => !c.Connection.Info.Equals(sender.Info)))
                    {
                        this.pendingConnections.Remove(connection);
                        this.activeConnections.Add(connection);
                        connection.SequencedFailsCount = 0;
                        connection.State = sender.State;
                    }

                    break;
                case ConnectionState.Closed:
                    connection = this.activeConnections.FirstOrDefault(c => c.Connection.Info.Equals(sender.Info));
                    if (connection != null)
                    {
                        this.BroadcastMessage(
                            connection.Connection.Info,
                            MessageSeverity.Error,
                            string.Format("The connection has been lost."));
                        this.activeConnections.Remove(connection);
                        this.TryToRestartLostConnection(connection);
                        connection.State = sender.State;
                    }

                    break;
                default:
                    connection = this.FindConnection(sender.Info);
                    if (connection != null)
                    {
                        connection.State = sender.State;
                    }

                    break;
                }
            }

            this.Fire(o => o.HandleStateChanged(sender.Info, sender.State));
        }

        private void BroadcastMessage(ConnectionInfo connectionInfo, MessageSeverity severity, string message)
        {
            this.Fire(o => o.HandleMessage(connectionInfo, severity, message));
        }

        private ConnectionInternal FindConnection(ConnectionInfo connectionInfo)
        {
            return this.activeConnections.FirstOrDefault(c => c.Connection.Info.Equals(connectionInfo)) ??
                   this.pendingConnections.FirstOrDefault(c => c.Connection.Info.Equals(connectionInfo));
        }

        private void Fire(Action<IConnectionManagerObserver> action)
        {
            if (this.SyncContext != null)
            {
                this.SyncContext.Send(state => this.observers.Apply(action), null);
            }
            else
            {
                this.observers.Apply(action);
            }
        }

        private void TryToRestartLostConnection(ConnectionInternal connection)
        {
            if (!this.userSettings.Settings.RestartLostConnections ||
                connection.SequencedFailsCount > this.userSettings.Settings.AttemptsToRestartLostConnection)
            {
                return;
            }

            var restartInterval = this.userSettings.Settings.LostConnectionRestartInterval;
            if (restartInterval == TimeSpan.Zero)
            {
                this.BroadcastMessage(connection.Connection.Info, MessageSeverity.Info, "Attempting to reconnect...");
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
                        lock (this.syncObject)
                        {
                            this.BroadcastMessage(connection.Connection.Info, MessageSeverity.Info, "Attempting to reconnect...");
                            this.pendingConnections.Add(connection);
                            connection.Connection.Open();
                        }
                    },
                    null,
                    (long)restartInterval.TotalMilliseconds,
                    -1);

                var seconds = restartInterval.TotalSeconds;
                var timePart = seconds / 60 > 0
                    ? string.Format("{0} minute(s)", (int)Math.Ceiling(seconds / 60.0))
                    : string.Format("{0} second(s)", seconds);
                this.BroadcastMessage(
                    connection.Connection.Info,
                    MessageSeverity.Info,
                    string.Format("A reconnection attempt will be made in {0}...", timePart));
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
            public ConnectionState State { get; set; }
        }
    }
}
