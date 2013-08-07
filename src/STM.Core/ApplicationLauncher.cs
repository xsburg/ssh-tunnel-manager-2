// ***********************************************************************
// <author>Stephan Burguchev</author>
// <copyright company="Stephan Burguchev">
//   Copyright (c) Stephan Burguchev 2012-2013. All rights reserved.
// </copyright>
// <summary>
//   ApplicationLauncher.cs
// </summary>
// ***********************************************************************

using System.Diagnostics;
using System.IO;
using STM.Core.Data;
using STM.Core.Util;

namespace STM.Core
{
    public class ApplicationLauncher
    {
        private const string FileZillaLocation = @"Tools\FileZilla\filezilla.exe";
        private const string PsftpLocation = @"Tools\psftp.exe";
        private const string PuttyLocation = @"Tools\putty.exe";

        public void StartFileZilla(ConnectionInfo connectionInfo)
        {
            var fileName = Path.Combine(PathHelper.StartupPath, FileZillaLocation);
            var args = ArgumentsBuilder.BuildFileZillaArguments(connectionInfo);
            Process.Start(fileName, args);
        }

        public void StartPsftp(ConnectionInfo connectionInfo)
        {
            var fileName = Path.Combine(PathHelper.StartupPath, PsftpLocation);
            var args = ArgumentsBuilder.BuildPsftpArguments(
                connectionInfo,
                PrivateKeyStorage.Create(connectionInfo.PrivateKeyData).Filename);
            Process.Start(fileName, args);
        }

        public void StartPutty(ConnectionInfo connectionInfo)
        {
            var fileName = Path.Combine(PathHelper.StartupPath, PuttyLocation);
            var args = ArgumentsBuilder.BuildPuttyArguments(
                connectionInfo,
                true,
                PrivateKeyStorage.Create(connectionInfo.PrivateKeyData).Filename);
            Process.Start(fileName, args);
        }
    }
}
