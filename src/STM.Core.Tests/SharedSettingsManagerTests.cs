// ***********************************************************************
// <author>Stephan Burguchev</author>
// <copyright company="Stephan Burguchev">
//   Copyright (c) Stephan Burguchev 2012-2013. All rights reserved.
// </copyright>
// <summary>
//   SharedSettingsManagerTests.cs
// </summary>
// ***********************************************************************

using System;
using NUnit.Framework;
using SharpTestsEx;

namespace STM.Core.Tests
{
    [TestFixture]
    public class SharedSettingsManagerTests
    {
        private string name;

        [Test]
        public void It_should_delete_settings()
        {
            var ssm = new SharedSettingsManager();
            this.name = Guid.NewGuid().ToString("N");
            var settings = ssm.GetOrCreate(this.name);
            settings.ProxyHost = "foo";
            ssm.Save(settings);
            ssm.Delete(settings.Name);
            var settings2 = ssm.GetOrCreate(settings.Name);
            settings2.ProxyHost.Should().Not.Be.EqualTo(settings.ProxyHost);
        }

        [Test]
        public void It_should_save_changes()
        {
            var ssm = new SharedSettingsManager();
            this.name = Guid.NewGuid().ToString("N");
            var settings = ssm.GetOrCreate(this.name);
            settings.ProxyHost = "foo";
            ssm.Save(settings);
            var settings2 = ssm.GetOrCreate(settings.Name);
            settings2.ProxyHost.Should().Be.EqualTo(settings.ProxyHost);
        }

        [TearDown]
        public void TearDown()
        {
            if (this.name != null)
            {
                new SharedSettingsManager().Delete(this.name);
            }
        }
    }
}
