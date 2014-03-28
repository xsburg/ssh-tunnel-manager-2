// ***********************************************************************
// <author>Stephan Burguchev</author>
// <copyright company="Stephan Burguchev">
//   Copyright (c) Stephan Burguchev 2012-2014. All rights reserved.
// </copyright>
// <summary>
//   EncryptedStorageData.cs
// </summary>
// ***********************************************************************

using System;
using System.Collections.Generic;
using System.Linq;

namespace STM.Core.Data
{
    public class EncryptedStorageData
    {
        public EncryptedStorageData()
        {
            this.Connections = new List<ConnectionInfo>();
            this.SharedSettings = new List<SharedConnectionSettings>();
        }

        public EncryptedStorageData(
            IEnumerable<ConnectionInfo> connections,
            IEnumerable<SharedConnectionSettings> sharedSettings)
        {
            if (connections == null)
            {
                throw new ArgumentNullException("connections");
            }

            if (sharedSettings == null)
            {
                throw new ArgumentNullException("sharedSettings");
            }

            this.Connections = connections.ToList();
            this.SharedSettings = sharedSettings.ToList();
        }

        public List<ConnectionInfo> Connections { get; private set; }
        public List<SharedConnectionSettings> SharedSettings { get; private set; }

        public static EncryptedStorageData CreateDefaultContent()
        {
            var content = new EncryptedStorageData
                {
                    SharedSettings = new List<SharedConnectionSettings>
                        {
                            new SharedConnectionSettings("default")
                        }
                };
            return content;
        }

        public ConnectionInfo FindConnection(string connectionName)
        {
            return this.Connections.First(c => c.Name == connectionName);
        }
    }
}
