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
            throw new NotImplementedException();
        }

        void IConnectionObserver.HandleMessage(IConnection sender, MessageSeverity severity, string message)
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
    }

    public enum MessageSeverity
    {
        Debug,
        Error,
        Warn,
        Info
    }

    public static class PuttyArgumentsBuilder
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
