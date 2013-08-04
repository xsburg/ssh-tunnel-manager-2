// ***********************************************************************
// <author>Stephan Burguchev</author>
// <copyright company="Stephan Burguchev">
//   Copyright (c) Stephan Burguchev 2012-2013. All rights reserved.
// </copyright>
// <summary>
//   ArgumentsBuilder.cs
// </summary>
// ***********************************************************************

using System;
using System.Linq;
using System.Text;
using STM.Core.Data;

namespace STM.Core
{
    public static class ArgumentsBuilder
    {
        public static string BuildFileZillaArguments(ConnectionInfo connection)
        {
            var arguments = string.Format(
                @"sftp://{0}:{1}@{2}:{3}",
                connection.UserName,
                connection.Password,
                connection.HostName,
                connection.Port);
            return arguments;
        }

        public static string BuildPsftpArguments(ConnectionInfo connection, string privateKeyFileName)
        {
            var arguments = string.Format("{0}@{1} -P {2} -batch", connection.UserName, connection.Password, connection.Port);
            switch (connection.AuthType)
            {
            case AuthenticationType.Password:
                arguments += string.Format("-pw {0}", connection.Password);
                break;
            case AuthenticationType.PrivateKey:
                arguments += string.Format("-i {0}", privateKeyFileName);
                break;
            default:
                throw new ArgumentOutOfRangeException();
            }

            return arguments;
        }

        /// <summary>
        ///     Build something like this:
        ///     "-ssh -load _stm_preset_ username@domainName -P 22 -pw password -D 5000 -L 44333:username.dyndns.org:44333"
        /// </summary>
        public static string BuildPuttyArguments(
            ConnectionInfo connection,
            bool forceShellStart,
            string privateKeyFileName = null)
        {
            const string sshFlag = "-ssh";
            const string verboseFlag = "-v";
            var target = string.Format("{0}@{1}", connection.UserName, connection.HostName);
            var port = string.Format("-P {0}", connection.Port);

            var profile = connection.SharedSettings != null
                ? string.Format("-load {0}", connection.SharedSettings.Name)
                : "";

            var dontStartShellFlag = string.IsNullOrEmpty(connection.RemoteCommand)
                ? "-N"
                : "";

            var credentials = "";
            switch (connection.AuthType)
            {
            case AuthenticationType.None:
                break;
            case AuthenticationType.Password:
                credentials = string.Format("-pw {0}", connection.Password);
                break;
            case AuthenticationType.PrivateKey:
                credentials = string.Format("-i {0}", privateKeyFileName);
                break;
            default:
                throw new ArgumentOutOfRangeException();
            }

            var connectionArguments = string.Join(
                " ",
                sshFlag,
                target,
                port,
                credentials,
                profile,
                verboseFlag,
                dontStartShellFlag);

            var sb = new StringBuilder(connectionArguments);
            foreach (var tunnelArguments in connection.Tunnels.Select(BuildPuttyTunnelArguments))
            {
                sb.Append(tunnelArguments);
            }

            return sb.ToString();
        }

        private static string BuildPuttyTunnelArguments(TunnelInfo tunnel)
        {
            if (tunnel == null)
            {
                throw new ArgumentNullException("tunnel");
            }

            switch (tunnel.Type)
            {
            case TunnelType.Local:
                return String.Format(@" -L {0}:{1}:{2}", tunnel.LocalPort, tunnel.RemoteHostname, tunnel.RemotePort);
            case TunnelType.Remote:
                return String.Format(@" -R {0}:{1}:{2}", tunnel.LocalPort, tunnel.RemoteHostname, tunnel.RemotePort);
            case TunnelType.Dynamic:
                return String.Format(@" -D {0}", tunnel.LocalPort);
            default:
                throw new ArgumentOutOfRangeException("tunnel");
            }
        }
    }
}
