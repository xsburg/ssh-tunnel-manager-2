// ***********************************************************************
// <author>Stephan Burguchev</author>
// <copyright company="Stephan Burguchev">
//   Copyright (c) Stephan Burguchev 2012-2013. All rights reserved.
// </copyright>
// <summary>
//   EncryptedStorageTests.cs
// </summary>
// ***********************************************************************

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Xml;
using NUnit.Framework;
using SharpTestsEx;
using STM.Core.Data;

namespace STM.Core.Tests
{
    [TestFixture]
    public class EncryptedStorageTests
    {
        private string fileName;
        private SharedConnectionSettings settings1;
        private SharedConnectionSettings settings2;

        [SetUp]
        public void SetUp()
        {
            this.fileName = Guid.NewGuid().ToString("N");
            var ssm = new SharedSettingsManager();
            this.settings1 = ssm.GetOrCreate(Guid.NewGuid().ToString("N"));
            this.settings2 = ssm.GetOrCreate(Guid.NewGuid().ToString("N"));
        }

        [TearDown]
        public void TearDown()
        {
            File.Delete(this.fileName);
            var ssm = new SharedSettingsManager();
            ssm.Delete(this.settings1.Name);
            ssm.Delete(this.settings2.Name);
        }

        [Test]
        public void It_should_save_connections()
        {
            /*var marketplace = new Dictionary<string, object>()
                {
                    { "key1", 1 },
                    { "key2", "value2" },
                };
            string xmlString;
            using (var sw = new StringWriter())
            {
                using (var writer = new XmlTextWriter(sw))
                {
                    writer.Formatting = Formatting.Indented; // indent the Xml so it's human readable
                    serializer.WriteObject(writer, marketplace);
                    writer.Flush();
                    xmlString = sw.ToString();
                }
            }

            var ms = new MemoryStream(Encoding.Default.GetBytes(xmlString));

            var object12 = serializer.ReadObject(ms);*/

            var expectedData = CreateContent();
            var storage = new EncryptedStorage
                {
                    Parameters =
                        {
                            FileName = this.fileName,
                            Password = "password"
                        }
                };
            storage.Save();
            storage.Read();
            storage.Content.Connections.Should().Have.SameSequenceAs(expectedData.Connections);
        }

        [Test]
        public void It_should_save_and_restore_settings_relations()
        {
            var expectedData = CreateContent();
            var storage = new EncryptedStorage
                {
                    Parameters =
                        {
                            FileName = this.fileName,
                            Password = "password"
                        }
                };
            storage.Save();
            storage.Read();
            var actualData = storage.Content;

            actualData.SharedSettings.Should().Have.Count.EqualTo(2);
            actualData.Connections.Select(c => c.SharedSettingsName)
                .Should()
                .Have.SameSequenceAs(expectedData.Connections.Select(c => c.SharedSettingsName));
            actualData.Connections[2].SharedSettings.Should().Be.SameInstanceAs(actualData.Connections[3].SharedSettings);
        }

        private EncryptedStorageContent CreateContent()
        {
            var connections = new[]
                {
                    CreateConnectionInfo(1),
                    CreateConnectionInfo(2),
                    CreateConnectionInfo(3),
                    CreateConnectionInfo(4)
                };

            connections[1].SharedSettings = settings1;
            connections[2].SharedSettings = settings2;
            connections[3].SharedSettings = settings2;

            return new EncryptedStorageContent(connections.ToList(), new [] { settings1, settings2 });
        }

        private static ConnectionInfo CreateConnectionInfo(int index)
        {
            return new ConnectionInfo
                {
                    Name = "connection_" + index,
                    HostName = "foo-" + index + ".com",
                    Port = 22,
                    UserName = "test-" + index,
                    Password = "test-" + index
                };
        }
    }
}
