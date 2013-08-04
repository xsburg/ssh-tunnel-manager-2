using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
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

    public class EncryptedStorageParameters
    {
        public string FileName { get; private set; }
        public string Password { get; private set; }
    }

    public class EncryptedStorageContent
    {
        
    }

    public interface IEncryptedStorage
    {
        EncryptedStorageParameters Parameters { get; set; }
        EncryptedStorageContent Read();
        void Save(EncryptedStorageContent data);
    }

    public class EncryptedStorage : IEncryptedStorage
    {
        public EncryptedStorageParameters Parameters { get; set; }
        public EncryptedStorageContent Read()
        {
            throw new NotImplementedException();
        }

        public void Save(EncryptedStorageContent data)
        {
            throw new NotImplementedException();
        }
    }


    public class PuttyProfileManager
    {
        public SharedConnectionSettings GetOrCreate(string name)
        {
            throw new NotImplementedException();
        }

        public void Save(SharedConnectionSettings profile)
        {
            
        }
    }

    public interface IConnectionObserver
    {
        void HandleForwardingError(IConnection sender, TunnelInfo tunnel, string errorMessage);
        void HandleStateChanged(IConnection sender);
        void HandleMessage(IConnection sender, MessageSeverity severity, string message);
        void HandleFatalError(string errorMessage);
    }

    public enum MessageSeverity
    {
        Debug,
        Error,
        Warn,
        Info
    }

    public static class ArgumentsBuilder
    {
        public static string BuildPuttyArguments(object hostInfo)
        {
            return null;
        }

        public static string BuildPsftpArguments()
        {
            return null;
        }
    }

    public class ConsoleTools
    {
        public static void StartPutty()
        {
            /*var fileName = Path.Combine(Util.Helper.StartupPath, PuttyLocation);
            var args = PuttyArguments(host, profile, host.AuthType);
            Process.Start(fileName, args);*/
        }

        public static void StartPsftp()
        {/*
            var fileName = Path.Combine(Util.Helper.StartupPath, PsftpLocation);
            var args = psftpArguments(host);
            Process.Start(fileName, args);*/
        }

        public static void StartFileZilla()
        {/*
            var fileName = Path.Combine(Util.Helper.StartupPath, FileZillaLocation);
            var args = string.Format(@"sftp://{0}:{1}@{2}:{3}", host.Username, host.Password, host.Hostname, host.Port);
            Process.Start(fileName, args);*/
        }
    }
}
