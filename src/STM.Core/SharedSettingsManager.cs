// ***********************************************************************
// <author>Stephan Burguchev</author>
// <copyright company="Stephan Burguchev">
//   Copyright (c) Stephan Burguchev 2012-2013. All rights reserved.
// </copyright>
// <summary>
//   SharedSettingsManager.cs
// </summary>
// ***********************************************************************

using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security;
using System.Text.RegularExpressions;
using Microsoft.Win32;
using STM.Core.Data;

namespace STM.Core
{
    public class SharedSettingsManager
    {
        private const string PuttySessionsRoot = @"Software\SimonTatham\PuTTY\Sessions\";
        private const string STMPrefix = "___SSHTunnelManager___";
        private static readonly SharedConnectionSettings DefaultProfile;

        static SharedSettingsManager()
        {
            var properties = MyResources.DefaultPuttyProfile
                .Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)
                .Skip(3)
                .Select(ParseRegExportLine)
                .Where(t => t != null)
                .ToDictionary(p => p.Item1, p => p.Item2);
            DefaultProfile = new SharedConnectionSettings("default", properties);
        }

        public void Delete(string name)
        {
            try
            {
                var profileKeyName = GetRegistryKeyName(name);
                Registry.CurrentUser.DeleteSubKeyTree(profileKeyName, false);
            }
            catch (Exception ex)
            {
                throw new ClientException(
                    string.Format("Security error has occured while deleting the PuTTY profile {0}: {1}", name, ex.Message),
                    ex);
            }
        }

        public SharedConnectionSettings GetOrCreate(string name)
        {
            if (name == null)
            {
                throw new ArgumentNullException("name");
            }

            return Read(name) ?? this.Create(name);
        }

        public void Save(SharedConnectionSettings profile)
        {
            if (profile == null)
            {
                throw new ArgumentNullException("profile");
            }

            try
            {
                var profileKeyName = GetRegistryKeyName(profile.Name);
                using (var profileKey = Registry.CurrentUser
                    .CreateSubKey(profileKeyName, RegistryKeyPermissionCheck.ReadWriteSubTree))
                {
                    if (profileKey == null)
                    {
                        throw new ClientException(string.Format("Failed to create the profile subkey {0}", profile.Name));
                    }

                    foreach (var pair in profile.Properties)
                    {
                        RegistryValueKind kind;
                        if (pair.Value is string)
                        {
                            kind = RegistryValueKind.String;
                        }
                        else if (pair.Value is int)
                        {
                            kind = RegistryValueKind.DWord;
                        }
                        else
                        {
                            throw new FormatException("Invalid shared settings property type.");
                        }

                        profileKey.SetValue(pair.Key, pair.Value, kind);
                    }
                }
            }
            catch (SecurityException e)
            {
                throw new ClientException(
                    string.Format("Security error has occured while updating the PuTTY profile {0}: {1}", profile.Name, e.Message),
                    e);
            }
            catch (UnauthorizedAccessException e)
            {
                throw new ClientException(
                    string.Format(
                        "Unauthorized access error has occured while updating PuTTY profile {0}: {1}",
                        profile.Name,
                        e.Message),
                    e);
            }
            catch (IOException e)
            {
                throw new ClientException(
                    string.Format("I/O error has occured while updating PuTTY profile {0}: {1}", profile.Name, e.Message),
                    e);
            }
        }

        private static string GetRegistryKeyName(string name)
        {
            return PuttySessionsRoot + STMPrefix + name;
        }

        private static Tuple<string, object> ParseRegExportLine(string line)
        {
            var m = Regex.Match(
                line,
                @"((?<name>[^""=]+)|""(?<name>([^""]|(?<=\\)"")+)"")" +
                @"=" +
                @"(((?<type>\w+):)?(?<value>[^""=]*)|""((?<type>\w+:)?(?<value>([^""]|(?<=\\)"")*))"")");
            if (!m.Success)
            {
                return null;
            }

            var name = m.Groups["name"].Value.Replace(@"\""", @"""");
            var textValue = m.Groups["value"].Value.Replace(@"\""", @"""");
            var type = m.Groups["type"].Value;
            object value;

            if (string.IsNullOrEmpty(type))
            {
                value = textValue;
            }
            else if (type == @"dword")
            {
                value = int.Parse(textValue, NumberStyles.AllowHexSpecifier);
            }
            else
            {
                throw new NotSupportedException();
            }

            return Tuple.Create(name, value);
        }

        private static SharedConnectionSettings Read(string name)
        {
            if (name == null)
            {
                throw new ArgumentNullException("name");
            }

            try
            {
                var profileKeyName = GetRegistryKeyName(name);
                using (var profileKey = Registry.CurrentUser.OpenSubKey(profileKeyName, false))
                {
                    if (profileKey == null)
                    {
                        return null;
                    }

                    if (DefaultProfile.Properties.Keys.Except(profileKey.GetValueNames()).Any())
                    {
                        throw new ClientException("The default profile has some properties that are missing in the read one.");
                    }

                    var profile = new SharedConnectionSettings(name);
                    foreach (var propertyName in profileKey.GetValueNames())
                    {
                        var value = profileKey.GetValue(propertyName);
                        profile.Properties[propertyName] = value;
                    }

                    return profile;
                }
            }
            catch (SecurityException e)
            {
                throw new ClientException(
                    string.Format("Security error has occured while updating the PuTTY profile {0}: {1}", name, e.Message),
                    e);
            }
        }

        private SharedConnectionSettings Create(string name)
        {
            var profile = new SharedConnectionSettings(name, new Dictionary<string, object>(DefaultProfile.Properties));
            this.Save(profile);
            return Read(profile.Name);
        }
    }
}
