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
using STM.Core.Util;

namespace STM.Core.Data
{
    public class SharedConnectionSettings
    {
        public SharedConnectionSettings(string name)
        {
            this.Name = name;
            this.Properties = new Dictionary<string, object>();
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
