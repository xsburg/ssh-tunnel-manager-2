// ***********************************************************************
// <author>Stephan Burguchev</author>
// <copyright company="Stephan Burguchev">
//   Copyright (c) Stephan Burguchev 2012-2013. All rights reserved.
// </copyright>
// <summary>
//   PathHelper.cs
// </summary>
// ***********************************************************************

using System;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Text;

namespace STM.Core.Util
{
    /// <summary>Provides some useful implements missing in the FCL.</summary>
    public static class PathHelper
    {
        /// <summary>
        ///     Gets the path for the executable file that started the application, including the executable
        ///     name.
        /// </summary>
        /// <value>The path for the executable file that started the application.</value>
        public static string ExecutablePath
        {
            get
            {
                return GetExecutablePath();
            }
        }

        /// <summary>
        ///     Gets the path for the executable file that started the application, not including the
        ///     executable name.
        /// </summary>
        /// <value>
        ///     The path for the executable file that started the application.
        /// </value>
        public static string StartupPath
        {
            get
            {
                return Path.GetDirectoryName(GetExecutablePath());
            }
        }

        [DllImport(@"kernel32.dll", CharSet = CharSet.Auto)]
        private static extern int GetModuleFileName(IntPtr hModule, StringBuilder buffer, int length);

        private static string GetExecutablePath()
        {
            string executablePath;
            Assembly assembly1 = Assembly.GetEntryAssembly();
            if (assembly1 == null)
            {
                var builder1 = new StringBuilder(260);
                GetModuleFileName(IntPtr.Zero, builder1, builder1.Capacity);
                executablePath = Path.GetFullPath(builder1.ToString());
            }
            else
            {
                string text1 = assembly1.EscapedCodeBase;
                var uri1 = new Uri(text1);
                executablePath = uri1.Scheme == "file"
                    ? GetLocalPath(text1)
                    : uri1.ToString();
            }

            var uri2 = new Uri(executablePath);
            if (uri2.Scheme == "file")
            {
                new FileIOPermission(FileIOPermissionAccess.PathDiscovery, executablePath).Demand();
            }

            return executablePath;
        }

        private static string GetLocalPath(string fileName)
        {
            var uri1 = new Uri(fileName);
            return uri1.LocalPath + uri1.Fragment;
        }
    }
}
