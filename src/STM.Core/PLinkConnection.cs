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
        private Process process;
        private ConnectionState state;

        // ReSharper disable once NotAccessedField.Local
        private Timer timer;

        public PLinkConnection(ConnectionInfo connection)
        {
            if (connection == null)
            {
                throw new ArgumentNullException("connection");
            }

            this.Connection = connection;
        }

        public ConnectionInfo Connection { get; private set; }
        public bool HasForwardingFailures { get; private set; }
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

            try
            {
                this.process.Kill();
            }
                // ReSharper disable once EmptyGeneralCatchClause
            catch (Exception)
            {
            }
        }

        public void Open()
        {
            this.HasForwardingFailures = false;
            this.State = ConnectionState.Opening;
            this.multilineErrorText.Clear();

            string privateKeyFileName = null;
            if (Connection.AuthType == AuthenticationType.PrivateKey)
            {
                privateKeyFileName = PrivateKeyStorage.Create(Connection.PrivateKeyData).Filename;
            }

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
                    Arguments = ArgumentsBuilder.BuildPuttyArguments(this.Connection, false, privateKeyFileName)
                }
            };

            this.process.ErrorDataReceived += this.HandleErrorData;
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
                    var c = (char)this.process.StandardOutput.Read();
                    buffer.Append(c);
                }

                this.process.StandardOutput.DiscardBufferedData();
                string data = buffer.ToString().ToLower();

                buffer.Clear();

                if (data.Contains(@"login as:"))
                {
                    // invalid username provided
                    this.Close();
                    this.PublishFatalError("Invalid username");
                }
                else if (data.Contains(@"password:") && !passwordProvided)
                {
                    this.ProcessWriteLine(this.Connection.Password);
                    passwordProvided = true;
                }
                else if (data.Contains(@"passphrase for key") && !passphraseForKeyProvided)
                {
                    this.ProcessWriteLine(this.Connection.Password);
                    passphraseForKeyProvided = true;
                }
                else
                {
                    foreach (var line in data.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries))
                    {
                        this.PublishMessage(MessageSeverity.Debug, line);
                    }
                }
            }

            if (this.Connection.AuthType == AuthenticationType.PrivateKey)
            {
                PrivateKeyStorage.Delete(this.Connection.PrivateKeyData);
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
                // Доступ открыт, можно удалить ключ
                if (this.Connection.AuthType == AuthenticationType.PrivateKey)
                {
                    PrivateKeyStorage.Delete(this.Connection.PrivateKeyData);
                }

                this.PublishMessage(
                    MessageSeverity.Debug,
                    string.Format("Access granted called: {0}", this.Connection.DisplayText));

                // Make delay for a couple of seconds and set status to 'Started' if the shell is not supposed to be started
                if (string.IsNullOrWhiteSpace(this.Connection.RemoteCommand))
                {
                    this.timer = new Timer(
                        delegate
                        {
                            this.PublishMessage(
                                MessageSeverity.Debug,
                                string.Format("Delegate called: {0}", this.Connection.DisplayText));
                            this.State = ConnectionState.Opened;
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
                var tunnel = this.Connection.Tunnels.FirstOrDefault(
                    t => t.LocalPort == srcPort && t.Type == TunnelType.Dynamic);
                if (tunnel != null)
                {
                    this.PublishTunnelFailure(tunnel, errorString);
                    this.PublishMessage(
                        MessageSeverity.Warn,
                        string.Format("[{0}] [{1}] {2}", this.Connection.Name, tunnel.DisplayText, errorString));
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
            var tunnel = this.Connection.Tunnels.FirstOrDefault(
                t =>
                    t.LocalPort == srcPort && t.RemoteHostName == dstHost && t.RemotePort == dstPort && t.Type == TunnelType.Local);
            if (tunnel != null)
            {
                this.PublishTunnelFailure(tunnel, errorString);
                this.PublishMessage(
                    MessageSeverity.Warn,
                    string.Format("[{0}] [{1}] {2}", this.Connection.Name, tunnel.DisplayText, errorString));
            }
        }

        private void FindShellStartMessage(string text)
        {
            if (text.Contains(ShellStartedMessage))
            {
                this.State = ConnectionState.Opened;

                // Start a command to be executed after connection establishment
                if (!string.IsNullOrWhiteSpace(this.Connection.RemoteCommand))
                {
                    this.timer = new Timer(
                        delegate { this.ProcessWriteLine(this.Connection.RemoteCommand); },
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
                this.Observer.HandleFatalError(message);
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
