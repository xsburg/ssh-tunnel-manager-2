﻿// ***********************************************************************
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
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using STM.Core.Data;

namespace STM.Core
{
    public class PLinkConnection : IConnection
    {
        private const string PLinkLocation = @"Tools\plink.exe";
        private const string ShellStartedMessage = "Started a shell/command";

        private readonly StringBuilder multilineErrorText = new StringBuilder();
        private ConnectionInfo info;
        private bool passphraseForKeyProvided;
        private bool passwordProvided;
        private Process process;
        private ConnectionState state = ConnectionState.Closed;

        // ReSharper disable once NotAccessedField.Local
        private Timer timer;

        public PLinkConnection()
        {
        }

        public PLinkConnection(ConnectionInfo info)
        {
            if (info == null)
            {
                throw new ArgumentNullException("info");
            }

            this.Info = info;
        }

        public bool HasForwardingFailures { get; private set; }

        public ConnectionInfo Info
        {
            get
            {
                return this.info;
            }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException();
                }

                if (this.State != ConnectionState.Closed)
                {
                    throw new InvalidOperationException("The connection must be closed to perform this operation.");
                }

                this.info = value;
            }
        }

        public IConnectionObserver Observer { get; set; }

        public ConnectionState State
        {
            get
            {
                return this.state;
            }
            private set
            {
                if (this.state == value)
                {
                    return;
                }

                this.state = value;
                this.PublishStateChanged();
            }
        }

        public void Close()
        {
            if (this.State == ConnectionState.Closed)
            {
                return;
            }

            this.CheckConsistency();

            if (this.Info.AuthType == AuthenticationType.PrivateKey)
            {
                PrivateKeyStorage.Delete(this.Info.PrivateKeyData);
            }
            
            try
            {
                if (!this.process.HasExited)
                {
                    this.State = ConnectionState.Closing;
                    this.process.Kill();
                }
            }
            // ReSharper disable once EmptyGeneralCatchClause
            catch (Exception)
            {
            }

            this.State = ConnectionState.Closed;
        }

        public void Open()
        {
            try
            {
                this.CheckConsistency();

                this.passwordProvided = false;
                this.passphraseForKeyProvided = false;
                this.HasForwardingFailures = false;
                this.State = ConnectionState.Opening;
                this.multilineErrorText.Clear();

                string privateKeyFileName = null;
                if (this.Info.AuthType == AuthenticationType.PrivateKey)
                {
                    privateKeyFileName = PrivateKeyStorage.Create(this.Info.PrivateKeyData).Filename;
                }

                var puttyArguments = ArgumentsBuilder.BuildPuttyArguments(this.Info, false, privateKeyFileName);
                this.process = new Process
                    {
                        StartInfo =
                            {
                                FileName = PLinkLocation,
                                CreateNoWindow = true,
                                UseShellExecute = false,
                                RedirectStandardError = true,
                                RedirectStandardOutput = true,
                                RedirectStandardInput = true,
                                Arguments = puttyArguments
                            }
                    };

                this.process.Exited += (s, a) => this.Close();
                this.process.ErrorDataReceived += this.HandleErrorData;
                this.process.OutputDataReceived += this.HandleOutputData;
                this.process.Start();
                this.process.BeginErrorReadLine();
                this.process.BeginOutputReadLine();

                this.process.StandardInput.AutoFlush = true;
            }
            catch (Exception ex)
            {
                this.PublishFatalError(ex.Message);
                this.Close();
            }
        }

        private void CheckConsistency()
        {
            if (this.Info == null)
            {
                throw new InvalidOperationException("The connection info property must be set to perform this operation.");
            }
        }

        private void FindAccessDeniedError(string text)
        {
            if (text.Contains("Access denied"))
            {
                this.PublishFatalError("Access Denied");
                this.Close();
            }
        }

        private void FindAccessGrantedMessage(string text)
        {
            if (text.Contains(@"Access granted"))
            {
                if (this.Info.AuthType == AuthenticationType.PrivateKey)
                {
                    PrivateKeyStorage.Delete(this.Info.PrivateKeyData);
                }

                this.PublishMessage(
                    MessageSeverity.Debug,
                    string.Format("Access granted called: {0}", this.Info.DisplayText));

                var shellWontBeStarted = string.IsNullOrWhiteSpace(this.Info.RemoteCommand);
                if (shellWontBeStarted)
                {
                    this.timer = new Timer(
                        delegate
                        {
                            this.PublishMessage(
                                MessageSeverity.Debug,
                                string.Format("Delegate called: {0}", this.Info.DisplayText));
                            this.State = ConnectionState.Open;
                        },
                        null,
                        1500,
                        Timeout.Infinite);
                }
            }
        }

        private void FindCentificateAcceptanceMessage(string text)
        {
            if (text.Contains(@"The server's host key is not cached in the registry."))
            {
                this.ProcessWrite(@"y");
            }
        }

        private bool FindConnectionError(string text)
        {
            if (this.multilineErrorText.Length > 0)
            {
                this.multilineErrorText.Append(text);
                this.PublishFatalError(this.multilineErrorText.ToString());
                this.multilineErrorText.Clear();
                this.Close();
                return true;
            }

            if (text.Contains(@"Unable to open connection:"))
            {
                this.multilineErrorText.Append(@"Unable to open connection: ");
                return true;
            }

            return false;
        }

        private void FindDynamicTunnelFailures(string text)
        {
            var m = Regex.Match(
                text,
                @"Local port (?<srcPort>\d+) SOCKS dynamic forwarding failed: (?<errorString>.*)",
                RegexOptions.IgnoreCase);
            if (m.Success)
            {
                var srcPort = int.Parse(m.Groups["srcPort"].Value);
                var errorString = m.Groups["errorString"].Value;
                var tunnel = this.Info.Tunnels.FirstOrDefault(
                    t => t.LocalPort == srcPort && t.Type == TunnelType.Dynamic);
                if (tunnel != null)
                {
                    this.PublishTunnelFailure(tunnel, errorString);
                    this.PublishMessage(
                        MessageSeverity.Warn,
                        string.Format("[{0}] [{1}] {2}", this.Info.Name, tunnel.DisplayText, errorString));
                }
            }
        }

        private void FindFatalError(string text)
        {
            var m = Regex.Match(text, @"^FATAL ERROR:\s*(?<msg>.*)$");
            if (m.Success)
            {
                this.PublishFatalError(m.Groups["msg"].Value);
            }
        }

        private void FindLocalTunnelFailures(string text)
        {
            var m = Regex.Match(
                text,
                @"Local port (?<srcPort>\d+) forwarding to (?<dstHost>[^:]+):(?<dstPort>\d+) failed: (?<errorString>.*)",
                RegexOptions.IgnoreCase);
            if (!m.Success)
            {
                return;
            }

            var srcPort = int.Parse(m.Groups["srcPort"].Value);
            var dstHost = m.Groups["dstHost"].Value;
            var dstPort = int.Parse(m.Groups["dstPort"].Value);
            var errorString = m.Groups["errorString"].Value;
            var tunnel = this.Info.Tunnels.FirstOrDefault(
                t =>
                    t.LocalPort == srcPort && t.RemoteHostName == dstHost && t.RemotePort == dstPort && t.Type == TunnelType.Local);
            if (tunnel != null)
            {
                this.PublishTunnelFailure(tunnel, errorString);
                this.PublishMessage(
                    MessageSeverity.Warn,
                    string.Format("[{0}] [{1}] {2}", this.Info.Name, tunnel.DisplayText, errorString));
            }
        }

        private void FindShellStartMessage(string text)
        {
            if (text.Contains(ShellStartedMessage))
            {
                this.State = ConnectionState.Open;

                // Start a command to be executed after connection establishment
                if (!string.IsNullOrWhiteSpace(this.Info.RemoteCommand))
                {
                    this.timer = new Timer(
                        delegate { this.ProcessWriteLine(this.Info.RemoteCommand); },
                        null,
                        1000,
                        Timeout.Infinite);
                }
            }
        }

        private void HandleErrorData(object o, DataReceivedEventArgs args)
        {
            var text = args.Data;
            if (text == null)
            {
                return;
            }

            this.PublishMessage(MessageSeverity.Debug, text);

            this.FindLocalTunnelFailures(text);
            this.FindDynamicTunnelFailures(text);
            this.FindCentificateAcceptanceMessage(text);
            if (this.FindConnectionError(text))
            {
                return;
            }

            this.FindAccessDeniedError(text);
            this.FindAccessGrantedMessage(text);
            this.FindFatalError(text);
            this.FindShellStartMessage(text);
        }

        private void HandleOutputData(object sender, DataReceivedEventArgs e)
        {
            if (e.Data == null)
            {
                return;
            }

            var data = e.Data.ToLower();

            if (data.Contains(@"login as:"))
            {
                // invalid username provided
                this.PublishFatalError("Invalid username");
                this.Close();
            }
            else if (data.Contains(@"password:") && !this.passwordProvided)
            {
                this.ProcessWriteLine(this.Info.Password);
                this.passwordProvided = true;
            }
            else if (data.Contains(@"passphrase for key") && !this.passphraseForKeyProvided)
            {
                this.ProcessWriteLine(this.Info.Password);
                this.passphraseForKeyProvided = true;
            }
            else
            {
                foreach (var line in data.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    this.PublishMessage(MessageSeverity.Debug, line);
                }
            }
        }

        private void ProcessWrite(string text)
        {
            lock (this.process.StandardInput)
            {
                this.process.StandardInput.Write(text);
            }
        }

        private void ProcessWriteLine(string text)
        {
            lock (this.process.StandardInput)
            {
                this.process.StandardInput.WriteLine(text);
            }
        }

        private void PublishFatalError(string message)
        {
            if (this.Observer != null)
            {
                this.Observer.HandleFatalError(this, message);
            }
        }

        private void PublishMessage(MessageSeverity severity, string message)
        {
            if (this.Observer != null)
            {
                this.Observer.HandleMessage(this, severity, message);
            }
        }

        private void PublishStateChanged()
        {
            if (this.Observer != null)
            {
                this.Observer.HandleStateChanged(this);
            }
        }

        private void PublishTunnelFailure(TunnelInfo tunnel, string errorMessage)
        {
            this.HasForwardingFailures = true;

            if (this.Observer != null)
            {
                this.Observer.HandleForwardingError(this, tunnel, errorMessage);
            }
        }
    }
}
