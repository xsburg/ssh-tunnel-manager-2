// ***********************************************************************
// <author>Stephan Burguchev</author>
// <copyright company="Stephan Burguchev">
//   Copyright (c) Stephan Burguchev 2012-2013. All rights reserved.
// </copyright>
// <summary>
//   EncryptedStorage.cs
// </summary>
// ***********************************************************************

using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using STM.Core.Data;
using STM.Core.Util;

namespace STM.Core
{
    public class EncryptedStorage : IEncryptedStorage
    {
        private static readonly byte[] MagicString = Encoding.ASCII.GetBytes(@"MAGIC");
        private static readonly XmlSerializer Serializer = new XmlSerializer(typeof(EncryptedStorageContent));

        public EncryptedStorage()
        {
            this.Parameters = new EncryptedStorageParameters();
        }

        public EncryptedStorageParameters Parameters { get; set; }

        public EncryptedStorageContent Read()
        {
            if (this.Parameters == null)
            {
                throw new InvalidOperationException("The parameters property is null.");
            }

            if (string.IsNullOrWhiteSpace(this.Parameters.Password))
            {
                throw new EncryptedStorageException("The password is empty.");
            }

            using (var inputXml = new MemoryStream())
            {
                try
                {
                    using (var file = File.OpenRead(this.Parameters.FileName))
                    {
                        CryptoHelper.DecryptAes(file, inputXml, this.Parameters.Password);
                    }
                }
                catch (CryptographicException e)
                {
                    if (e.Message == @"Padding is invalid and cannot be removed.")
                    {
                        // In most cases this means what password is invalid, but also can mean what storage is broken.
                        throw new EncryptedStorageException("Invalid password.");
                    }

                    throw;
                }

                inputXml.Seek(0, SeekOrigin.Begin);
                var buffer = new byte[MagicString.Length];
                var readed = inputXml.Read(buffer, 0, buffer.Length);
                if (readed != buffer.Length || !MagicString.SequenceEqual(buffer))
                {
                    throw new EncryptedStorageException("Invalid password.");
                }

                var data = (EncryptedStorageContent)Serializer.Deserialize(inputXml);
                InitializeRelations(data);
                return data;
            }
        }

        public void Save(EncryptedStorageContent data)
        {
            if (data == null)
            {
                throw new ArgumentNullException("data");
            }

            if (this.Parameters == null)
            {
                throw new InvalidOperationException("The parameters property is null.");
            }

            using (var stream = new MemoryStream())
            {
                stream.Write(MagicString, 0, MagicString.Length);
                using (var writer = XmlWriter.Create(
                    stream,
                    new XmlWriterSettings
                        {
                            Encoding = Encoding.UTF8,
                            Indent = true
                        }))
                {
                    Serializer.Serialize(writer, data);
                }

                stream.Seek(0, SeekOrigin.Begin);
                using (var encryptedStream = File.Open(this.Parameters.FileName, FileMode.Create, FileAccess.Write))
                {
                    CryptoHelper.EncryptAes(stream, encryptedStream, this.Parameters.Password);
                }
            }
        }

        public bool Test(out string errorText)
        {
            try
            {
                this.Read();
                errorText = "";
                return true;
            }
            catch (Exception ex)
            {
                errorText = ex.Message;
                return false;
            }
        }

        private static void InitializeRelations(EncryptedStorageContent data)
        {
            var connectionsByName = data.Connections.ToDictionary(c => c.Name);
            var sharedSettingsByName = data.SharedSettings.ToDictionary(cs => cs.Name);

            foreach (var connectionInfo in data.Connections)
            {
                // Parent
                var parentName = connectionInfo.ParentName;
                if (parentName != null)
                {
                    var parentConnectionInfo = connectionsByName.GetValueOrDefault(parentName);
                    if (parentConnectionInfo != null)
                    {
                        connectionInfo.Parent = parentConnectionInfo;
                    }
                    else
                    {
                        connectionInfo.ParentName = null;
                    }
                }

                // SharedSettings
                var sharedSettingsName = connectionInfo.SharedSettingsName;
                if (sharedSettingsName != null)
                {
                    var sharedSettings = sharedSettingsByName.GetValueOrDefault(sharedSettingsName);
                    if (sharedSettings != null)
                    {
                        connectionInfo.SharedSettings = sharedSettings;
                    }
                    else
                    {
                        connectionInfo.SharedSettingsName = null;
                    }
                }
            }
        }
    }
}
