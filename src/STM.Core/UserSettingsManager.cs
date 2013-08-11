// ***********************************************************************
// <author>Stephan Burguchev</author>
// <copyright company="Stephan Burguchev">
//   Copyright (c) Stephan Burguchev 2012-2013. All rights reserved.
// </copyright>
// <summary>
//   UserSettingsManager.cs
// </summary>
// ***********************************************************************

using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using STM.Core.Properties;

namespace STM.Core
{
    public class UserSettingsManager
    {
        private static readonly IEnumerable<PropertyDescriptor> PropertyDescriptors;

        static UserSettingsManager()
        {
            PropertyDescriptors = TypeDescriptor.GetProperties(typeof(Settings))
                .Cast<PropertyDescriptor>()
                .Where(p => p.Name.StartsWith("Settings_"));
        }

        public UserSettingsManager()
        {
            this.Settings = new UserSettings();
            foreach (var property in PropertyDescriptors)
            {
                var value = property.GetValue(Properties.Settings.Default);
                typeof(UserSettings).GetProperty(property.Name).SetValue(this.Settings, value);
            }

            this.FileName = Properties.Settings.Default.FileName;
            this.Password = Properties.Settings.Default.Password;
        }

        public string FileName { get; set; }

        public string Password { get; set; }

        public UserSettings Settings { get; private set; }

        public void Save()
        {
            foreach (var property in PropertyDescriptors)
            {
                var value = typeof(UserSettings).GetProperty(property.Name).GetValue(this.Settings);
                property.SetValue(Properties.Settings.Default, value);
            }

            Properties.Settings.Default.FileName = this.FileName;
            Properties.Settings.Default.Password = this.Password;
            Properties.Settings.Default.Save();
        }
    }
}
