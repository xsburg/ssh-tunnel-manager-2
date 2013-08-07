﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using STM.Core.Data;

namespace STM.Core
{
    public class ConnectionManager : IConnectionObserver
    {
        public TimeSpan AcceptableStartDelay { get; set; }
        public TimeSpan AcceptableStopDelay { get; set; }

        private List<ConnectionInfo> activeConnections = new List<ConnectionInfo>();
        private List<ConnectionInfo> pendingConnections = new List<ConnectionInfo>();

        public void Open(ConnectionInfo connection)
        {
            if (connection.Parent != null)
            {
                this.Open(connection.Parent);
            }

            if (this.activeConnections.Contains(connection))
            {
                return;
            }

            if (this.pendingConnections.Contains(connection))
            {
                return;
            }

            this.pendingConnections.Add(connection);
        }

        public void Close(ConnectionInfo connection)
        {
            this.activeConnections.Remove(connection);
        }

        void IConnectionObserver.HandleForwardingError(IConnection sender, TunnelInfo tunnel, string errorMessage)
        {
            throw new NotImplementedException();
        }

        void IConnectionObserver.HandleStateChanged(IConnection sender)
        {
            /*string logMessage;
            switch (value)
            {
            case ConnectionState.Opening:
                logMessage = "Starting...";
                break;
            case ConnectionState.Opened:
                logMessage = this.HasForwardingFailures
                    ? "Started with warnings"
                    : "Started";
                break;
            case ConnectionState.Closed:
                logMessage = "Stopped";
                break;
            case ConnectionState.Closing:
                if (_config.RestartDelay > 0)
                    Logger.Log.InfoFormat("[{0}] {1}", Host.Name, string.Format("Waiting {0} seconds before restart...", _config.RestartDelay));
                else
                    Logger.Log.InfoFormat("[{0}] {1}", Host.Name, "Restarting after connection loss...");
                break;
            }

            this.PublishMessage(MessageSeverity.Info, string.Format("[{0}] {1}", Connection.Name, logMessage));
            this.state = value;
            this.PublishStateChanged();

            if (_status == ELinkStatus.Stopped)
            {
                _eventStopped.Set();
            }
            else
            {
                _eventStopped.Reset();
            }
            if (_status == ELinkStatus.Started ||
                _status == ELinkStatus.StartedWithWarnings)
            {
                _eventStarted.Set();
            }
            else
            {
                _eventStarted.Reset();
            }*/
        }

        void IConnectionObserver.HandleMessage(IConnection sender, MessageSeverity severity, string message)
        {
            throw new NotImplementedException();
        }

        public void HandleFatalError(string errorMessage)
        {
            throw new NotImplementedException();
        }
    }
}
