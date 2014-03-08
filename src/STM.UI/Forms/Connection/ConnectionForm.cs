using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using STM.Core.Data;
using STM.Core.Util;
using STM.UI.Annotations;
using STM.UI.Framework.Validation;
using STM.UI.Framework.Validation.Rules;
using STM.UI.Properties;

namespace STM.UI.Forms.Connection
{
    public partial class ConnectionForm : Form, IConnectionForm
    {
        private readonly ValidationProvider connectionValidator;
        private readonly ValidationProvider tunnelValidator;
        private bool modified;

        public ConnectionFormController Controller { get; private set; }

        public ConnectionForm(ConnectionFormController controller)
        {
            if (controller == null)
            {
                throw new ArgumentNullException("controller");
            }

            this.InitializeComponent();

            this.navigationTreeView.SelectedNode = this.navigationTreeView.Nodes[0];

            this.Controller = controller;
            this.Controller.Register(this);

            this.connectionValidator = new ValidationProvider();
            this.tunnelValidator = new ValidationProvider();

            this.connectionValidator.ErrorProvider = this.myErrorProvider;
            this.tunnelValidator.ErrorProvider = this.myErrorProvider;

            this.connectionValidator.SetValidationRule(this.hostNameTextBox, new HostNameValidationRule());
            this.connectionValidator.SetValidationRule(this.portTextBox, new PortNumberValidationRule());
            this.connectionValidator.SetValidationRule(this.userNameTextBox, new UserNameValidationRule());
            this.connectionValidator.SetValidationRule(this.passwordTextBox, new DelegatedValidationRule<string>(this.ValidatePassword));
            this.connectionValidator.SetValidationRule(this.privateKeyFileNameLabel, new DelegatedValidationRule<string>(this.ValidatePrivateKeyData));

            this.tunnelValidator.SetValidationRule(this.tunnelSrcPortTextBox, new DelegatedValidationRule<string>(this.ValidateTunnelSourcePort));
            this.tunnelValidator.SetValidationRule(this.tunnelDstHostTextBox, new DelegatedValidationRule<string>(this.ValidateTunnelDestinationHost));
            this.tunnelValidator.SetValidationRule(this.tunnelDstPortTextBox, new DelegatedValidationRule<string>(this.ValidateTunnelDestinationPort));
            this.tunnelValidator.SetValidationRule(this.tunnelNameTextBox, new DelegatedValidationRule<string>(this.ValidateTunnelName));
        }

        private ValidationResult ValidatePrivateKeyData(string text)
        {
            if (!this.usePrivateKeyRadioButton.Checked)
            {
                return ValidationResult.Success();
            }

            if (!RequiredValidationRule.Instance.Validate(this.Controller.LoadedPrivateKeyData))
            {
                return ValidationResult.Fail(RequiredValidationRule.Instance.ErrorText);
            }

            return ValidationResult.Success();
        }

        private ValidationResult ValidateTunnelName(string text)
        {
            if (!RequiredValidationRule.Instance.Validate(text))
            {
                return ValidationResult.Fail(RequiredValidationRule.Instance.ErrorText);
            }

            if (this.CollectTunnelInfoList().Select(t => t.Name).Contains(text))
            {
                return ValidationResult.Fail("The name is already used by another tunnel.");
            }

            return ValidationResult.Success();
        }

        private ValidationResult ValidateTunnelDestinationHost(string text)
        {
            if (!this.tunnelTypeDynamicRadioButton.Checked)
            {
                // local or remote
                var result = HostNameValidationRule.Instance.Validate(text);
                if (!result)
                {
                    return ValidationResult.Fail(HostNameValidationRule.Instance.ErrorText);
                }

                return ValidationResult.Success();
            }

            // dynamic
            if (!string.IsNullOrWhiteSpace(text))
            {
                return ValidationResult.Fail("The destination host name should not be set for dynamic tunnels.");
            }

            return ValidationResult.Success();
        }

        private ValidationResult ValidateTunnelDestinationPort(string text)
        {
            if (!this.tunnelTypeDynamicRadioButton.Checked)
            {
                // local or remote
                var result = PortNumberValidationRule.Instance.Validate(text);
                if (!result)
                {
                    return ValidationResult.Fail(PortNumberValidationRule.Instance.ErrorText);
                }

                return ValidationResult.Success();
            }

            // dynamic
            if (!string.IsNullOrWhiteSpace(text))
            {
                return ValidationResult.Fail("The destination port number should not be set for dynamic tunnels.");
            }

            return ValidationResult.Success();
        }

        private ValidationResult ValidateTunnelSourcePort(string text)
        {
            if (!PortNumberValidationRule.Instance.Validate(text))
            {
                return ValidationResult.Fail(PortNumberValidationRule.Instance.ErrorText);
            }

            var port = int.Parse(text);
            if (this.CollectTunnelInfoList().Any(t => t.LocalPort == port))
            {
                return ValidationResult.Fail("The port is already used by another local tunnel.");
            }

            return ValidationResult.Success();
        }

        private IEnumerable<TunnelInfo> CollectTunnelInfoList()
        {
            return this.tunnelsGridView.Rows.Cast<DataGridViewRow>().Select(r => (TunnelInfo)r.Tag);
        }

        private ValidationResult ValidatePassword(string text)
        {
            if (!usePasswordRadioButton.Checked)
            {
                return ValidationResult.Success();
            }

            if (this.usePasswordRadioButton.Checked && !PasswordValidationRule.Instance.Validate(text))
            {
                return ValidationResult.Fail(PasswordValidationRule.Instance.ErrorText);
            }

            return ValidationResult.Success();
        }

        private class ConnectionNameValidationRule : ValidationRule
        {
            private readonly IEnumerable<ConnectionInfo> connections;

            public ConnectionNameValidationRule(IEnumerable<ConnectionInfo> connections)
            {
                this.connections = connections;
            }

            public override bool Validate(object value)
            {
                var text = value as string;
                if (string.IsNullOrWhiteSpace(text))
                {
                    this.ErrorText = "The field is required";
                    return false;
                }

                var taken = this.connections.Any(c => c.Name.Equals(text));
                if (taken)
                {
                    this.ErrorText = "The name has already been assigned to another connection.";
                    return false;
                }

                return true;
            }
        }

        public void Render([NotNull] IEnumerable<ConnectionInfo> allConnections, ConnectionInfo connection)
        {
            if (allConnections == null)
            {
                throw new ArgumentNullException("allConnections");
            }

            this.SuspendLayout();

            var isNew = connection == null;
            if (isNew)
            {
                connection = new ConnectionInfo();
            }

            this.connectionNameTextBox.Text = connection.Name;
            this.hostNameTextBox.Text = connection.HostName;
            portTextBox.Text = connection.Port.ToString(CultureInfo.InvariantCulture);
            userNameTextBox.Text = connection.UserName;
            switch (connection.AuthType)
            {
            case AuthenticationType.Password:
                usePasswordRadioButton.Checked = true;
                passwordTextBox.Text = connection.Password;
                passphraseTextBox.Text = "";
                this.privateKeyFileNameLabel.Text = "<Not Loaded>";
                break;
            case AuthenticationType.PrivateKey:
                usePrivateKeyRadioButton.Checked = true;
                passphraseTextBox.Text = connection.Password;
                passwordTextBox.Text = "";
                this.privateKeyFileNameLabel.Text = "<Previously Loaded>";
                break;
            default:
                throw new ArgumentOutOfRangeException();
            }

            this.remoteCommandTextBox.Text = connection.RemoteCommand;

            this.parentConnectionComboBox.Items.Clear();
            // ReSharper disable once PossibleUnintendedReferenceComparison
            var allButThis = allConnections.Where(c => c != connection).ToArray();
            var possibleParents = allButThis.Where(c => !c.IsChildOf(connection)).OrderBy(c => c.Name);
            this.parentConnectionComboBox.Items.AddRange(new[] { "None" }.Concat<object>(possibleParents).ToArray());
            if (connection.Parent == null)
            {
                this.parentConnectionComboBox.SelectedIndex = 0;
            }
            else
            {
                this.parentConnectionComboBox.SelectedItem = connection.Parent;
            }

            this.tunnelsGridView.Rows.Clear();
            foreach (var tunnel in connection.Tunnels)
            {
                this.AddTunnelToList(tunnel);
            }

            this.removeTunnelButton.Enabled = this.tunnelsGridView.SelectedRows.Count > 0;

            this.connectionValidator.SetValidationRule(this.connectionNameTextBox, new ConnectionNameValidationRule(allButThis));
            this.connectionValidator.ResetErrors();
            this.ResetAddTunnelGroup();

            this.AcceptButton = isNew
                ? this.createButton
                : this.okButton;
            this.okButton.Visible = !isNew;
            this.applyButton.Visible = !isNew;
            this.createButton.Visible = isNew;
            this.Modified = false;

            this.ResumeLayout(true);
        }

        public void RenderPrivateKeyFileName(string fileName)
        {
            this.privateKeyFileNameLabel.Text = Path.GetFileName(fileName);
        }

        public void ResetAddTunnelGroup()
        {
            this.tunnelNameTextBox.Text = "";
            this.tunnelSrcPortTextBox.Text = "";
            this.tunnelDstHostTextBox.Text = "";
            this.tunnelDstPortTextBox.Text = "";
            this.tunnelTypeLocalRadioButton.Checked = true;
            this.tunnelValidator.ResetErrors();
        }

        private void AddTunnelToList(TunnelInfo tunnel)
        {
            string tunnelTypeAbbr;
            switch (tunnel.Type)
            {
            case TunnelType.Local:
                tunnelTypeAbbr = "L";
                break;
            case TunnelType.Remote:
                tunnelTypeAbbr = "R";
                break;
            case TunnelType.Dynamic:
                tunnelTypeAbbr = "D";
                break;
            default:
                throw new ArgumentOutOfRangeException();
            }

            var row = new DataGridViewRow();
            row.Cells.Add(new DataGridViewTextBoxCell { Value = tunnel.Name });
            row.Cells.Add(new DataGridViewTextBoxCell { Value = tunnelTypeAbbr });
            row.Cells.Add(new DataGridViewTextBoxCell { Value = tunnel.LocalPort });
            row.Cells.Add(new DataGridViewTextBoxCell { Value = tunnel.RemoteHostName });
            row.Cells.Add(new DataGridViewTextBoxCell { Value = tunnel.RemotePort });
            row.Tag = tunnel;
            this.tunnelsGridView.Rows.Add(row);
        }

        public new bool? ShowDialog()
        {
            var result = base.ShowDialog();
            switch (result)
            {
            case DialogResult.OK:
                return true;
            case DialogResult.Cancel:
                return false;
            default:
                return null;
            }
        }

        public void Close(bool result)
        {
            this.DialogResult = result
                ? DialogResult.OK
                : DialogResult.Cancel;
            this.Close();
        }

        /*

        public ConnectionForm(EMode mode, List<HostInfo> committedHosts)
        {
            // modified check
            this.textBoxName.TextChanged += delegate { this.Modified = true; };
            this.textBoxHostname.TextChanged += delegate { this.Modified = true; };
            this.textBoxPort.TextChanged += delegate { this.Modified = true; };
            this.textBoxLogin.TextChanged += delegate { this.Modified = true; };
            this.tbxPassword.TextChanged += delegate { this.Modified = true; };
            this.tbxPassphrase.TextChanged += delegate { this.Modified = true; };
            this.tbxRemoteCommand.TextChanged += delegate { this.Modified = true; };
            this.rbxPassword.CheckedChanged += delegate { this.Modified = true; };
            this.btnLoadPrivateKey.Click += delegate { this.Modified = true; };
            this.comboBoxDependsOn.SelectedIndexChanged += delegate { this.Modified = true; };
        }
*/

        public bool ValidateTunnel()
        {
            return this.tunnelValidator.Validate();
        }

        private void createButton_Click(object sender, EventArgs e)
        {
            this.Controller.Create();
        }

        public bool ValidateConnection()
        {
            return this.connectionValidator.Validate();
        }

        public void Collect(ConnectionInfo connection)
        {
            connection.Name = this.connectionNameTextBox.Text.Trim();
            connection.HostName = this.hostNameTextBox.Text.Trim();
            connection.Port = int.Parse(this.portTextBox.Text.Trim());
            connection.UserName = this.userNameTextBox.Text.Trim();
            connection.RemoteCommand = this.remoteCommandTextBox.Text.Trim();

            if (this.usePasswordRadioButton.Checked)
            {
                connection.AuthType = AuthenticationType.Password;
                connection.Password = this.passwordTextBox.Text.Trim();
            }
            else
            {
                connection.AuthType = AuthenticationType.PrivateKey;
                var passphrase = this.passphraseTextBox.Text.Trim();
                connection.Password = !string.IsNullOrEmpty(passphrase) ? passphrase : null;
            }

            connection.Parent = this.parentConnectionComboBox.SelectedItem as ConnectionInfo;
            connection.Tunnels.Clear();
            connection.Tunnels.AddRange(this.CollectTunnelInfoList());
        }

        private TunnelInfo GetSelectedTunnel()
        {
            var tunnel = this.tunnelsGridView.SelectedRows.Cast<DataGridViewRow>().Select(r => r.Tag as TunnelInfo).FirstOrDefault();
            return tunnel;
        }

        private void tunnelTypeDynamicRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (this.tunnelTypeDynamicRadioButton.Checked)
            {
                this.tunnelDstHostTextBox.Text = "";
                this.tunnelDstPortTextBox.Text = "";
                this.tunnelDstHostTextBox.Enabled = false;
                this.tunnelDstPortTextBox.Enabled = false;
            }
            else
            {
                this.tunnelDstHostTextBox.Enabled = true;
                this.tunnelDstPortTextBox.Enabled = true;
            }
        }

        private void addTunnelButton_Click(object sender, EventArgs e)
        {
            this.AddTunnel();
        }

        public void AddTunnel()
        {
            if (!this.tunnelValidator.Validate())
            {
                return;
            }

            var name = this.tunnelNameTextBox.Text;
            var srcPort = int.Parse(this.tunnelSrcPortTextBox.Text);
            var dstHost = this.tunnelDstHostTextBox.Text;
            var dstPort = int.Parse(this.tunnelDstPortTextBox.Text);
            var tunnelType = this.tunnelTypeLocalRadioButton.Checked
                ? TunnelType.Local
                : (this.tunnelTypeRemoteRadioButton.Checked
                    ? TunnelType.Remote
                    : TunnelType.Dynamic);

            var tunnel = new TunnelInfo
                {
                    Name = name,
                    LocalPort = srcPort,
                    RemoteHostName = dstHost,
                    RemotePort = dstPort,
                    Type = tunnelType
                };

            this.AddTunnelToList(tunnel);
            this.Modified = true;
            this.ResetAddTunnelGroup();
        }

        private void removeTunnelButton_Click(object sender, EventArgs e)
        {
            var row = this.tunnelsGridView.SelectedRows.Cast<DataGridViewRow>().FirstOrDefault();
            if (row == null)
            {
                return;
            }

            var tunnel = row.Tag as TunnelInfo;
            if (tunnel == null)
            {
                return;
            }

            this.tunnelsGridView.Rows.Remove(row);
            this.tunnelNameTextBox.Text = tunnel.Name;
            this.tunnelSrcPortTextBox.Text = tunnel.LocalPort.ToString(CultureInfo.InvariantCulture);
            this.tunnelDstHostTextBox.Text = tunnel.RemoteHostName;
            this.tunnelDstPortTextBox.Text = tunnel.RemotePort.ToString(CultureInfo.InvariantCulture);
            switch (tunnel.Type)
            {
            case TunnelType.Local:
                this.tunnelTypeLocalRadioButton.Checked = true;
                break;
            case TunnelType.Remote:
                this.tunnelTypeRemoteRadioButton.Checked = true;
                break;
            case TunnelType.Dynamic:
                this.tunnelTypeDynamicRadioButton.Checked = true;
                break;
            }

            this.Modified = true;
        }

        private void tunnelsGridView_SelectionChanged(object sender, EventArgs e)
        {
            this.removeTunnelButton.Enabled = this.GetSelectedTunnel() != null;
        }

        private void applyButton_Click(object sender, EventArgs e)
        {
            this.Controller.Apply();
            this.Modified = false;
        }

        private bool Modified
        {
            get
            {
                return this.modified;
            }
            set
            {
                this.modified = value;
                this.applyButton.Enabled = this.modified;
            }
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close(false);
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            this.Controller.Ok();
        }

        private void loadPrivateKeyButton_Click(object sender, EventArgs e)
        {
            this.Controller.LoadPrivateKey();
        }

        private void usePrivateKeyRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            var usePrivateKey = this.usePrivateKeyRadioButton.Checked;
            this.passwordTextBox.Enabled = !usePrivateKey;
            this.loadPrivateKeyButton.Enabled = usePrivateKey;
            this.passphraseTextBox.Enabled = usePrivateKey;
        }

        private void navigationTreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            this.SuspendLayout();
            const string mainSectionName = "main";
            const string tunnelsSectionName = "tunnels";
            var node = navigationTreeView.SelectedNode;
            switch (node.Name)
            {
            case mainSectionName:
                mainPanel.Visible = true;
                tunnelsPanel.Visible = false;
                break;
            case tunnelsSectionName:
                mainPanel.Visible = false;
                tunnelsPanel.Visible = true;
                break;
            }

            this.ResumeLayout(true);
        }
    }
}
