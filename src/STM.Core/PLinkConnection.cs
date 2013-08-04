// ***********************************************************************
// <author>Stephan Burguchev</author>
// <copyright company="Stephan Burguchev">
//   Copyright (c) Stephan Burguchev 2012-2013. All rights reserved.
// </copyright>
// <summary>
//   PLinkConnection.cs
// </summary>
// ***********************************************************************

using System;
using System.Diagnostics;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using STM.Core.Data;

namespace STM.Core
{
    public class PLinkConnection : IConnection
    {
        private Process process;
        private const string PLinkLocation = @"Tools\plink.exe";

        public ConnectionInfo Info { get; private set; }
        public IConnectionObserver Observer { get; set; }
        public ConnectionState State { get; private set; }

        public void Close()
        {
            throw new NotImplementedException();
        }

        public void Open()
        {
            // fill results dic
            lock (_forwardingResultsLock)
            {
                _forwardingResults = Host.Tunnels.ToDictionary(t => t, t => ForwardingResult.CreateSuccess());
            }

            this.process = new Process
            {
                StartInfo =
                {
                    FileName = ConsoleTools.PLinkLocation,
                    CreateNoWindow = true,
                    UseShellExecute = false,
                    RedirectStandardError = true,
                    RedirectStandardOutput = true,
                    RedirectStandardInput = true,
                    Arguments = ConsoleTools.PuttyArguments(Host, _profile, Host.AuthType)
                }
            };

            this.process.ErrorDataReceived += errorDataHandler;
            this.process.Start();
            this.process.BeginErrorReadLine();

            this.process.StandardInput.AutoFlush = true;

            var buffer = new StringBuilder();
            bool passwordProvided = false;
            bool passphraseForKeyProvided = false;
            while (!this.process.HasExited)
            {
                while (this.process.StandardOutput.Peek() >= 0)
                {
                    char c = (char)this.process.StandardOutput.Read();
                    buffer.Append(c);
                }

                this.process.StandardOutput.DiscardBufferedData();
                string data = buffer.ToString().ToLower();

                buffer.Clear();

                if (data.Contains(@"login as:"))
                {
                    // invalid username provided
                    stop();
                    // _process.StandardInput.WriteLine(username);
                    LastStartError = Resources.PuttyLink_Error_InvalidUsername;
                }
                else if (data.Contains(@"password:") && !passwordProvided)
                {
                    writeLineStdIn(Host.Password);
                    passwordProvided = true;
                }
                else if (data.Contains(@"passphrase for key") && !passphraseForKeyProvided)
                {
                    writeLineStdIn(Host.Password);
                    passphraseForKeyProvided = true;
                }
                else
                {
                    foreach (var line in data.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries))
                    {
                        Logger.Log.Debug(line);
                    }
                }
            }

            PrivateKeysStorage.RemovePrivateKey(Host);
            return Status == ELinkStatus.Started || Status == ELinkStatus.StartedWithWarnings;
        }
        private void writeStdIn(string text)
        {
            lock (_process.StandardInput)
            {
                _process.StandardInput.Write(text);
            }
        }

        private void errorDataHandler(object o, DataReceivedEventArgs args)
        {
            if (args.Data == null)
                return;
            ThreadContext.Properties[@"Host"] = Host; // Set up context for working thread
            Logger.Log.Debug(args.Data);
            // LOCAL tunnels error
            var m = Regex.Match(args.Data, @"Local port (?<srcPort>\d+) forwarding to (?<dstHost>[^:]+):(?<dstPort>\d+) failed: (?<errorString>.*)", RegexOptions.IgnoreCase);
            if (m.Success)
            {
                var srcPort = m.Groups["srcPort"].Value;
                var dstHost = m.Groups["dstHost"].Value;
                var dstPort = m.Groups["dstPort"].Value;
                var errorString = m.Groups["errorString"].Value;
                var tunnel = Host.Tunnels.FirstOrDefault(
                    t => t.LocalPort == srcPort && t.RemoteHostname == dstHost && t.RemotePort == dstPort && t.Type == TunnelType.Local);
                if (tunnel != null)
                {
                    lock (_forwardingResultsLock)
                    {
                        _forwardingResults[tunnel] = ForwardingResult.CreateFailed(errorString);
                    }

                    Logger.Log.WarnFormat("[{0}] [{1}] {2}", Host.Name, tunnel.SimpleString, errorString);
                }
            }
            // DYNAMIC tunnels error
            m = Regex.Match(args.Data, @"Local port (?<srcPort>\d+) SOCKS dynamic forwarding failed: (?<errorString>.*)", RegexOptions.IgnoreCase);
            if (m.Success)
            {
                var srcPort = m.Groups["srcPort"].Value;
                var errorString = m.Groups["errorString"].Value;
                var tunnel = Host.Tunnels.FirstOrDefault(
                    t => t.LocalPort == srcPort && t.Type == TunnelType.Dynamic);
                if (tunnel != null)
                {
                    lock (_forwardingResultsLock)
                    {
                        _forwardingResults[tunnel] = ForwardingResult.CreateFailed(errorString);
                    }
                    Logger.Log.WarnFormat("[{0}] [{1}] {2}", Host.Name, tunnel.SimpleString, errorString);
                }
            }
            // Accept certificate
            if (args.Data.Contains(@"The server's host key is not cached in the registry."))
            {
                writeStdIn(@"y");
            }
            // Unable to open connection:
            if (args.Data.Contains(@"Unable to open connection:"))
            {
                _multilineError.Append(@"Unable to open connection: ");
                return;
            }
            // Access denied error
            if (args.Data.Contains(@"Access denied"))
            {
                // Неверный пароль (Доступ запрещен)
                LastStartError = @"Access Denied";
                stop();
            }
            // Access granted
            if (args.Data.Contains(@"Access granted"))
            {
                // Доступ открыт, можно удалить ключ
                PrivateKeysStorage.RemovePrivateKey(Host);
                Logger.Log.Debug(string.Format("Access granted called: {0}", Host));

                // Make delay for a couple of seconds and set status to 'Started' if a shell is not supposed to be started
                if (string.IsNullOrWhiteSpace(Host.RemoteCommand))
                {
                    _deferredCallTimer = new Timer(
                        delegate
                        {
                            bool forwardingFailed;
                            lock (_forwardingResultsLock)
                            {
                                forwardingFailed = _forwardingResults.Any(p => !p.Value.Success);
                            }

                            ThreadContext.Properties[@"Host"] = Host;
                            Logger.Log.Debug(string.Format("Delegate called: {0}", Host));
                            Status = forwardingFailed
                                         ? ELinkStatus.StartedWithWarnings
                                         : ELinkStatus.Started;
                            ThreadContext.Properties[@"Host"] = null;
                        },
                        null,
                        1500,
                        Timeout.Infinite);
                }
            }

            // Fatal errors
            m = Regex.Match(args.Data, @"^FATAL ERROR:\s*(?<msg>.*)$");
            if (m.Success)
            {
                LastStartError = m.Groups["msg"].Value;
            }

            // connection establishing
            if (args.Data.Contains(ShellStartedMessage))
            {
                bool forwardingFails;
                lock (_forwardingResultsLock)
                {
                    forwardingFails = _forwardingResults.Any(p => !p.Value.Success);
                }

                Status = forwardingFails ? ELinkStatus.StartedWithWarnings : ELinkStatus.Started;

                // Start a command to be executed after connection establishment
                if (!string.IsNullOrWhiteSpace(Host.RemoteCommand))
                {
                    _deferredCallTimer = new Timer(
                        delegate
                        {
                            ThreadContext.Properties[@"Host"] = Host;
                            writeLineStdIn(Host.RemoteCommand);
                            ThreadContext.Properties[@"Host"] = null;
                        },
                        null,
                        1000,
                        Timeout.Infinite);
                }
            }

            // multiline error?
            if (_multilineError.Length > 0)
            {
                _multilineError.Append(args.Data);
                LastStartError = _multilineError.ToString();
                _multilineError.Clear();
                stop();
            }

            ThreadContext.Properties[@"Host"] = null;
        }
    }
}
