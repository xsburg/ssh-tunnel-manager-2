// ***********************************************************************
// <author>Stephan Burguchev</author>
// <copyright company="Stephan Burguchev">
//   Copyright (c) Stephan Burguchev 2012-2013. All rights reserved.
// </copyright>
// <summary>
//   Program.cs
// </summary>
// ***********************************************************************

using System;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using STM.Core.Util;

namespace STM.UI
{
    internal static class Program
    {
        // ReSharper disable once InconsistentNaming
        private const int WS_SHOWNORMAL = 1;

        public static void ActivateRunningInstance(Process instance)
        {
            ShowWindowAsync(instance.MainWindowHandle, WS_SHOWNORMAL);
            SetForegroundWindow(instance.MainWindowHandle);
        }

        public static Process RunningInstance()
        {
            var current = Process.GetCurrentProcess();
            var processes = Process.GetProcessesByName(current.ProcessName);
            return processes.Where(process => process.Id != current.Id)
                .FirstOrDefault(
                    process =>
                        string.Equals(
                            PathHelper.ExecutablePath,
                            process.MainModule.FileName,
                            StringComparison.OrdinalIgnoreCase));
        }

        [STAThread]
        private static void Main()
        {
            var process = RunningInstance();
            if (process != null)
            {
                ActivateRunningInstance(process);
            }
            else
            {
                new Bootstrapper().Run();
            }
        }

        [DllImport(@"User32.dll")]
        private static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport(@"User32.dll")]
        private static extern bool ShowWindowAsync(IntPtr hWnd, int cmdShow);
    }
}
