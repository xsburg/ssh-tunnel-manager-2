// ***********************************************************************
// <author>Stephan Burguchev</author>
// <copyright company="Stephan Burguchev">
//   Copyright (c) Stephan Burguchev 2012-2013. All rights reserved.
// </copyright>
// <summary>
//   EncryptedStorageContent.cs
// </summary>
// ***********************************************************************

using System;
using System.Collections.Generic;
using System.Linq;

namespace STM.Core.Data
{
    public class EncryptedStorageContent
    {
        public EncryptedStorageContent()
        {
            this.Connections = new List<ConnectionInfo>();
            this.SharedSettings = new List<SharedConnectionSettings>();
        }

        public EncryptedStorageContent(
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

        public static EncryptedStorageContent CreateDefaultContent()
        {
            var content = new EncryptedStorageContent
                {
                    SharedSettings = new List<SharedConnectionSettings>
                        {
                            new SharedConnectionSettings("default")
                        }
                };
            return content;
        }
    }
}
