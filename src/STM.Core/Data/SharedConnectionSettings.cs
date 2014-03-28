// ***********************************************************************
// <author>Stephan Burguchev</author>
// <copyright company="Stephan Burguchev">
//   Copyright (c) Stephan Burguchev 2012-2013. All rights reserved.
// </copyright>
// <summary>
//   SharedConnectionSettings.cs
// </summary>
// ***********************************************************************

using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using STM.Core.Util;

namespace STM.Core.Data
{
    [Serializable]
    public class SharedConnectionSettings : IXmlSerializable
    {
        private static readonly DataContractJsonSerializer Serializer =
            new DataContractJsonSerializer(typeof(Dictionary<string, object>));

        public SharedConnectionSettings()
        {
        }

        public SharedConnectionSettings(string name)
        {
            if (name == null)
            {
                throw new ArgumentNullException("name");
            }

            this.Name = name;
            this.Properties = new Dictionary<string, object>();
        }

        public SharedConnectionSettings(string name, Dictionary<string, object> properties)
        {
            if (name == null)
            {
                throw new ArgumentNullException("name");
            }

            if (properties == null)
            {
                throw new ArgumentNullException("properties");
            }

            this.Name = name;
            this.Properties = properties;
        }

        public bool LocalPortAcceptAll
        {
            get
            {
                return this.GetBool(Property.LocalPortAcceptAll);
            }
            set
            {
                this.SetBool(Property.LocalPortAcceptAll, value);
            }
        }

        public string Name { get; private set; }

        public string DisplayText
        {
            get
            {
                return Name;
            }
        }

        public Dictionary<string, object> Properties { get; private set; }

        public string ProxyExcludeList
        {
            get
            {
                return this.GetString(Property.ProxyExcludeList);
            }
            set
            {
                this.SetString(Property.ProxyExcludeList, value);
            }
        }

        public string ProxyHost
        {
            get
            {
                return this.GetString(Property.ProxyHost);
            }
            set
            {
                this.SetString(Property.ProxyHost, value);
            }
        }

        public bool ProxyLocalhost
        {
            get
            {
                return this.GetBool(Property.ProxyLocalhost);
            }
            set
            {
                this.SetBool(Property.ProxyLocalhost, value);
            }
        }

        public ProxyType ProxyMethod
        {
            get
            {
                return (ProxyType)this.GetInt(Property.ProxyMethod);
            }
            set
            {
                this.Properties[Property.ProxyMethod] = (int)value;
            }
        }

        public string ProxyPassword
        {
            get
            {
                return this.GetString(Property.ProxyPassword);
            }
            set
            {
                this.SetString(Property.ProxyPassword, value);
            }
        }

        public int ProxyPort
        {
            get
            {
                return this.GetInt(Property.ProxyPort);
            }
            set
            {
                this.SetInt(Property.ProxyPort, value);
            }
        }

        public string ProxyUsername
        {
            get
            {
                return this.GetString(Property.ProxyUsername);
            }
            set
            {
                this.SetString(Property.ProxyUsername, value);
            }
        }

        public bool RemotePortAcceptAll
        {
            get
            {
                return this.GetBool(Property.RemotePortAcceptAll);
            }
            set
            {
                this.SetBool(Property.RemotePortAcceptAll, value);
            }
        }

        XmlSchema IXmlSerializable.GetSchema()
        {
            return null;
        }

        void IXmlSerializable.ReadXml(XmlReader reader)
        {
            reader.ReadStartElement();
            this.Name = reader.ReadElementString("name");
            var xmlString = reader.ReadElementString("properties");
            reader.ReadEndElement();
            using (var ms = new MemoryStream(Encoding.UTF8.GetBytes(xmlString)))
            {
                this.Properties = (Dictionary<string, object>)Serializer.ReadObject(ms);
            }
        }

        void IXmlSerializable.WriteXml(XmlWriter writer)
        {
            string xmlString;
            using (var ms = new MemoryStream())
            {
                Serializer.WriteObject(ms, this.Properties);
                xmlString = Encoding.UTF8.GetString(ms.ToArray());
            }

            writer.WriteElementString("name", this.Name);
            writer.WriteElementString("properties", xmlString);
        }

        private bool GetBool(string name)
        {
            return this.Properties.GetValueOrDefault(name, 0) != 0;
        }

        private int GetInt(string name)
        {
            return this.Properties.GetValueOrDefault(name, 0);
        }

        private string GetString(string name)
        {
            return (string)this.Properties.GetValueOrDefault(name);
        }

        private void SetBool(string name, bool value)
        {
            this.Properties[name] = Convert.ToInt32(value);
        }

        private void SetInt(string name, int value)
        {
            this.Properties[name] = value;
        }

        private void SetString(string name, string value)
        {
            this.Properties[name] = value;
        }

        public static class Property
        {
            public const string LocalPortAcceptAll = "LocalPortAcceptAll";
            public const string ProxyExcludeList = "ProxyExcludeList";
            public const string ProxyHost = "ProxyHost";
            public const string ProxyLocalhost = "ProxyLocalhost";
            public const string ProxyMethod = "ProxyMethod";
            public const string ProxyPassword = "ProxyPassword";
            public const string ProxyPort = "ProxyPort";
            public const string ProxyUsername = "ProxyUsername";
            public const string RemotePortAcceptAll = "RemotePortAcceptAll";
        }
    }
}
