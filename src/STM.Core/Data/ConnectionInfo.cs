// ***********************************************************************
// <author>Stephan Burguchev</author>
// <copyright company="Stephan Burguchev">
//   Copyright (c) Stephan Burguchev 2012-2013. All rights reserved.
// </copyright>
// <summary>
//   ConnectionInfo.cs
// </summary>
// ***********************************************************************

using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace STM.Core.Data
{
    [Serializable]
    public class ConnectionInfo
    {
        private string parentName;

        public ConnectionInfo()
        {
            this.Tunnels = new List<TunnelInfo>();
            this.AuthType = AuthenticationType.Password;
        }

        public AuthenticationType AuthType { get; set; }

        public string Hostname { get; set; }

        [XmlAttribute]
        public string Name { get; set; }

        [XmlIgnore]
        public ConnectionInfo Parent { get; set; }

        public string ParentName
        {
            get
            {
                if (this.Parent != null)
                {
                    return this.Parent.Name;
                }

                return this.parentName;
            }
            set
            {
                if (this.Parent != null)
                {
                    throw new InvalidOperationException("The property is writable for serialization purpose only.");
                }

                this.parentName = value;
            }
        }

        public string DisplayText
        {
            get
            {
                return string.Format(@"{0} ({1}:{2})", Name, Hostname, Port);
            }
        }

        public string Password { get; set; }
        public string Port { get; set; }
        public string PrivateKeyData { get; set; }
        public string RemoteCommand { get; set; }
        public SharedConnectionSettings SharedSettings { get; set; }
        public List<TunnelInfo> Tunnels { get; private set; }
        public string Username { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null || this.GetType() != obj.GetType())
            {
                return false;
            }

            var other = (ConnectionInfo)obj;
            return this.Name.Equals(other.Name);
        }

        public override int GetHashCode()
        {
            return this.Name.GetHashCode();
        }

        public bool IsChildOf(ConnectionInfo connection)
        {
            if (connection == null)
            {
                throw new ArgumentNullException("connection");
            }

            for (var parent = this.Parent; parent != null; parent = parent.Parent)
            {
                if (Equals(parent, connection))
                {
                    return true;
                }
            }

            return false;
        }

        public override string ToString()
        {
            return string.Format(@"{0}:{1}", this.Hostname, this.Port);
        }
    }
}
