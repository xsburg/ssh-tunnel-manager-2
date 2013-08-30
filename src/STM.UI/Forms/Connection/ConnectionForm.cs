using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace STM.UI.Forms.Connection
{
    public partial class ConnectionForm : Form
    {
        private readonly Validator _hostValidator;
        private readonly Validator _tunnelValidator;

        private HostInfo _currentHost;
        private HostInfo _startupDependsOn;
        private readonly List<HostInfo> _createdHosts = new List<HostInfo>();
        private Label _labelRecentlyAdded;
        private bool _modified;
        private readonly List<HostInfo> _committedHosts;
        private string _loadedPrivateKey;

        public enum EMode
        {
            AddHost,
            EditHost
        }

        public ConnectionForm(EMode mode, List<HostInfo> committedHosts)
        {
            if (committedHosts == null) throw new ArgumentNullException("committedHosts");

            this.InitializeComponent();
            this._committedHosts = committedHosts;
            this.Mode = mode;

            // validators
            this._hostValidator = new Validator(this.theErrorProvider, this.theGoodProvider);
            this._hostValidator.AddControl(this.textBoxName, validateAlias);
            this._hostValidator.AddControl(this.textBoxHostname, this._hostValidator.ValidateHostname);
            this._hostValidator.AddControl(this.textBoxPort, this._hostValidator.ValidatePort);
            this._hostValidator.AddControl(this.textBoxLogin, this._hostValidator.ValidateUsername);
            this._hostValidator.AddControl(this.tbxPassword, validatePassword);
            this._hostValidator.AddControl(this.lblPrivateKeyFilename, control => control.Text, validatePrivateKeyData);

            this._tunnelValidator = new Validator(this.theErrorProvider, this.theGoodProvider);
            this._tunnelValidator.AddControl(this.textBoxSourcePort, validateTunnelSourcePort);
            this._tunnelValidator.AddControl(this.textBoxDestHost, validateTunnelDestinationHost);
            this._tunnelValidator.AddControl(this.textBoxDestPort, validateTunnelDestinationPort);
            this._tunnelValidator.AddControl(this.textBoxTunnelName, validateTunnelAlias);
            // style for selected mode
            if (mode == EMode.AddHost)
            {
                this.flowLayoutPanelAddHost.Visible = true;
                this.flowLayoutPanelEditHost.Visible = false;
                this.AcceptButton = this.buttonAddHost;
                this.CancelButton = this.buttonClose;
            }
            else
            {
                this.flowLayoutPanelAddHost.Visible = false;
                this.flowLayoutPanelEditHost.Visible = true;
                this.AcceptButton = this.buttonOk;
                this.CancelButton = this.buttonCancel;
            }
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

        private bool validatePassword(Control control, string text)
        {
            if (!this.rbxPassword.Checked)
            {
                this._tunnelValidator.SetError(control, null);
                return true;
            }

            return this._hostValidator.ValidateNotNullOrWhitespaces(control, text);
        }

        #region Validators

        private bool validatePrivateKeyData(Control control, string text)
        {
            if (!this.rbxPrivateKey.Checked)
            {
                this._tunnelValidator.SetError(control, null);
                return true;
            }

            return this._hostValidator.ValidateNotNullOrWhitespaces(control, this._loadedPrivateKey);
        }

        private bool validateAlias(Control control, string text)
        {
            if (this._tunnelValidator.ValidateNotNullOrWhitespaces(control, text))
            {
                var alias = text;

                var aliases = this._committedHosts.Where(h => h != this._currentHost).Select(h => h.Name).ToList();

                if (aliases.Contains(alias))
                {
                    this._tunnelValidator.SetError(control, Resources.ValidatorError_HostAliasBusy);
                    return false;
                }
                return true;
            }
            return false;
        }

        private bool validateTunnelAlias(Control control, string text)
        {
            if (this._tunnelValidator.ValidateNotNullOrWhitespaces(control, text))
            {
                var alias = text;

                //var aliases = _committedHosts.Where(h => h != _currentHost).SelectMany(h => h.Tunnels).Select(t => t.Name).Concat(
                //    tunnels().Select(t => t.Name)).ToList();

                var aliases = this.tunnels().Select(t => t.Name).ToList();

                if (aliases.Contains(alias))
                {
                    this._tunnelValidator.SetError(control, Resources.ValidatorError_TunnelAliasBusy);
                    return false;
                }
                return true;
            }
            return false;
        }

        private bool validateTunnelDestinationHost(Control control, string text)
        {
            if (this.radioButtonLocal.Checked || this.radioButtonRemote.Checked)
            {
                // local or remote
                return this._tunnelValidator.ValidateHostname(control, text);
            }
            // dynamic
            if (!string.IsNullOrWhiteSpace(text))
            {
                this.theErrorProvider.SetError(control, Resources.ValidatorError_TunnelDstHostnameNotEmpty);
                this.theGoodProvider.SetError(control, "");
                return false;
            }
            return true;
        }

        private bool validateTunnelDestinationPort(Control control, string text)
        {
            if (this.radioButtonLocal.Checked || this.radioButtonRemote.Checked)
            {
                // local or remote
                return this._tunnelValidator.ValidatePort(control, text);
            }
            // dynamic
            if (!string.IsNullOrWhiteSpace(text))
            {
                this._tunnelValidator.SetError(control, Resources.ValidatorError_TunnelHostnameNotEmpty);
                return false;
            }
            return true;
        }

        private bool validateTunnelSourcePort(Control control, string text)
        {
            if (this._tunnelValidator.ValidatePort(control, text))
            {
                var port = text;

                /*var otherHost = _committedHosts.Where(h => h != _currentHost).FirstOrDefault(h => h.Tunnels.Exists(t => t.LocalPort == port));
                if (otherHost != null)
                {
                    _tunnelValidator.SetError(control, string.Format(Resources.ValidatorError_LocalPortBusyAnotherHost, otherHost.Name));
                    return false;
                }*/
                if (this.tunnels().Exists(t => t.LocalPort == port))
                {
                    this._tunnelValidator.SetError(control, Resources.ValidatorError_LocalPortBusyThisHost);
                    return false;
                }
                return true;
            }
            return false;
        }

        #endregion

        #region Properties

        public EMode Mode { get; private set; }

        public HostInfo StartupDependsOn
        {
            get { return this._startupDependsOn; }
            set
            {
                if (this.Mode != EMode.AddHost)
                    throw new InvalidOperationException();
                this._startupDependsOn = value;
            }
        }

        public HostInfo Host
        {
            get { return this._currentHost; }
            set
            {
                if (this.Mode != EMode.EditHost)
                    throw new InvalidOperationException();
                this._currentHost = value;
            }
        }

        public HostInfo[] CreatedHosts
        {
            get { return this._createdHosts.ToArray(); }
        }

        #endregion

        private bool Modified
        {
            get { return this._modified; }
            set
            {
                this._modified = value;
                this.buttonApply.Enabled = this._modified;
            }
        }

        private void HostDialog_Load(object sender, EventArgs e)
        {
            this.reset();
            this._createdHosts.Clear();
            this.Modified = false;
        }

        private void reset()
        {
            if (this.Mode == EMode.AddHost)
            {
                this._currentHost = new HostInfo();

                this.textBoxName.Text = "";
                this.textBoxHostname.Text = "";
                this.textBoxPort.Text = "";
                this.textBoxLogin.Text = "";
                this.tbxPassword.Text = "";
                this.tbxRemoteCommand.Text = "";

                this.comboBoxDependsOn.Items.Clear();
                var hosts = this._committedHosts.Where(h => h != this._currentHost && !h.DeepDependsOn(this._currentHost)).OrderBy(h => h.Name);
                this.comboBoxDependsOn.Items.AddRange(new[] { Resources.HostDialog_DependsOnNone }.Concat<object>(hosts).ToArray());
                if (this.StartupDependsOn != null)
                    this.comboBoxDependsOn.SelectedItem = this.StartupDependsOn;
                else
                    this.comboBoxDependsOn.SelectedIndex = 0;

                this.tunnelsGridView.Rows.Clear();
                this.buttonRemoveTunnel.Enabled = this.tunnelsGridView.SelectedRows.Count > 0;

                this._hostValidator.Reset();
                this.resetAddTunnelGroup();
                this.Text = Resources.HostDialog_AddNewHostTitle;
            }
            else // Edit mode
            {
                this.hostToForm();
                this.Text = Resources.HostDialog_EditHostTitle;
            }
        }

        private void hostToForm()
        {
            if (this._currentHost == null)
                throw new FormatException(Resources.HostDialog_HostPropertyIsNull);

            this.textBoxName.Text = this._currentHost.Name;
            this.textBoxHostname.Text = this._currentHost.Hostname;
            this.textBoxPort.Text = this._currentHost.Port;
            this.textBoxLogin.Text = this._currentHost.Username;

            if (this._currentHost.AuthType == AuthenticationType.Password)
            {
                this.rbxPassword.Checked = true;
                this.tbxPassword.Text = this._currentHost.Password;
            }
            else
            {
                this.rbxPrivateKey.Checked = true;
                this.tbxPassphrase.Text = this._currentHost.Password;
                this.lblPrivateKeyFilename.Text = "<Previously Loaded>";
                this._loadedPrivateKey = this._currentHost.PrivateKeyData;
            }

            this.tbxPassword.Text = this._currentHost.Password;
            this.tbxRemoteCommand.Text = this._currentHost.RemoteCommand;

            this.comboBoxDependsOn.Items.Clear();
            var hosts = this._committedHosts.Where(h => h != this._currentHost && !h.DeepDependsOn(this._currentHost)).OrderBy(h => h.Name);
            this.comboBoxDependsOn.Items.AddRange(new[] { Resources.HostDialog_DependsOnNone }.Concat<object>(hosts).ToArray());
            if (this._currentHost.DependsOn == null)
            {
                this.comboBoxDependsOn.SelectedIndex = 0;
            }
            else
            {
                this.comboBoxDependsOn.SelectedItem = this._currentHost.DependsOn;
            }

            this.tunnelsGridView.Rows.Clear();
            foreach (var tunnel in this._currentHost.Tunnels)
            {
                this.addTunnel(tunnel);
            }

            this.buttonRemoveTunnel.Enabled = this.tunnelsGridView.SelectedRows.Count > 0;

            this._hostValidator.Reset();
            this.resetAddTunnelGroup();
        }

        private void formToHost()
        {
            this._currentHost.Name = this.textBoxName.Text.Trim();
            this._currentHost.Hostname = this.textBoxHostname.Text.Trim();
            this._currentHost.Port = this.textBoxPort.Text.Trim();
            this._currentHost.Username = this.textBoxLogin.Text.Trim();
            this._currentHost.RemoteCommand = this.tbxRemoteCommand.Text.Trim();

            if (this.rbxPassword.Checked)
            {
                this._currentHost.AuthType = AuthenticationType.Password;
                this._currentHost.Password = this.tbxPassword.Text.Trim();
                this._currentHost.PrivateKeyData = null;
            }
            else
            {
                this._currentHost.AuthType = AuthenticationType.PrivateKey;
                var passphrase = this.tbxPassphrase.Text.Trim();
                this._currentHost.Password = !string.IsNullOrEmpty(passphrase) ? passphrase : null;
                this._currentHost.PrivateKeyData = this._loadedPrivateKey;
            }

            var dependsOnHost = this.comboBoxDependsOn.SelectedItem as HostInfo;
            this._currentHost.DependsOn = dependsOnHost;

            this._currentHost.Tunnels.Clear();
            //_currentHost.Tunnels.AddRange(listBoxTunnels.Items.Cast<TunnelInfo>());
            this._currentHost.Tunnels.AddRange(this.tunnels());
        }

        private bool buttonAddTunnelReminder()
        {
            if (this._tunnelValidator.ValidateControls(false))
            {
                switch (MessageBox.Show(this, Resources.HostDialog_AddTunnelButtonReminder,
                                        Util.AssemblyTitle, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question))
                {
                case DialogResult.Yes:
                    // Adding last tunnel
                    this.buttonAddTunnel_Click(null, EventArgs.Empty);
                    return true;
                case DialogResult.No:
                    this.resetAddTunnelGroup();
                    return true;
                case DialogResult.Cancel:
                    // Go back and change something
                    return false;
                }
            }
            return true;
        }

        private void resetAddTunnelGroup()
        {
            this.textBoxTunnelName.Text = "";
            this.textBoxSourcePort.Text = "";
            this.textBoxDestHost.Text = "";
            this.textBoxDestPort.Text = "";
            this.radioButtonLocal.Checked = true;
            this._tunnelValidator.Reset();
        }

        #region Add host

        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void buttonAddHost_Click(object sender, EventArgs e)
        {
            if (!this._hostValidator.ValidateControls()) return;

            if (!this.buttonAddTunnelReminder())
                return;

            // Adding host
            this.formToHost();

            /*EncryptedSettings.Instance.Hosts.Add(_currentHost);
            EncryptedSettings.Instance.Save();*/
            this._createdHosts.Add(this._currentHost);

            // Update gui and reset
            if (this._labelRecentlyAdded == null)
            {
                this._labelRecentlyAdded = new Label {ForeColor = Color.FromArgb(20,146,20), Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Bottom, Margin = new Padding(3,3,3,0)};
                this.flowLayoutPanelMain.Controls.Add(this._labelRecentlyAdded);
            }
            this._labelRecentlyAdded.Text = string.Format(Resources.HostDialog_RecentlyAdded, this._currentHost); // string.Join(", ", _recentlyAddedHosts.Select(h => h.ToString()));
            var rowCount = (int)Math.Ceiling((double)this._labelRecentlyAdded.PreferredWidth / this._labelRecentlyAdded.Size.Width);
            this._labelRecentlyAdded.Size = new Size(this._labelRecentlyAdded.Size.Width, rowCount * this._labelRecentlyAdded.PreferredHeight);

            this.buttonClose.Text = Resources.ButtonText_Close;
            this.reset();
        }

        #endregion

        #region Tunnels

        private void addTunnel(TunnelInfo tunnel)
        {
            var row = new DataGridViewRow();
            row.Cells.Add(new DataGridViewTextBoxCell {Value = tunnel.Name});
            row.Cells.Add(new DataGridViewTextBoxCell {Value = tunnel.Type.ToString()[0]});
            row.Cells.Add(new DataGridViewTextBoxCell {Value = tunnel.LocalPort});
            row.Cells.Add(new DataGridViewTextBoxCell {Value = tunnel.RemoteHostname});
            row.Cells.Add(new DataGridViewTextBoxCell {Value = tunnel.RemotePort});
            row.Tag = tunnel;
            this.tunnelsGridView.Rows.Add(row);
        }

        private TunnelInfo selectedTunnel()
        {
            var tunnel = this.tunnelsGridView.SelectedRows.Cast<DataGridViewRow>().Select(r => r.Tag as TunnelInfo).FirstOrDefault();
            return tunnel;
        }

        private List<TunnelInfo> tunnels()
        {
            return this.tunnelsGridView.Rows.Cast<DataGridViewRow>().Select(r => (TunnelInfo) r.Tag).ToList();
        }

        private void radioButtonDynamic_CheckedChanged(object sender, EventArgs e)
        {
            if (this.radioButtonDynamic.Checked)
            {
                this.textBoxDestHost.Text = "";
                this.textBoxDestPort.Text = "";
                this.textBoxDestHost.Enabled = false;
                this.textBoxDestPort.Enabled = false;
            } else
            {
                this.textBoxDestHost.Enabled = true;
                this.textBoxDestPort.Enabled = true;
            }
        }

        private void buttonAddTunnel_Click(object sender, EventArgs e)
        {
            if (!this._tunnelValidator.ValidateControls()) return;

            var name = this.textBoxTunnelName.Text;
            var srcPort = this.textBoxSourcePort.Text;
            var dstHost = this.textBoxDestHost.Text;
            var dstPort = this.textBoxDestPort.Text;
            var tunnelType = this.radioButtonLocal.Checked
                                 ? TunnelType.Local
                                 : (this.radioButtonRemote.Checked ? TunnelType.Remote : TunnelType.Dynamic);

            var tunnel = new TunnelInfo {Name = name, LocalPort = srcPort, RemoteHostname = dstHost, RemotePort = dstPort, Type = tunnelType};

            this.addTunnel(tunnel);
            this.Modified = true;
            this.resetAddTunnelGroup();
        }

        private void buttonRemoveTunnel_Click(object sender, EventArgs e)
        {
            var row = this.tunnelsGridView.SelectedRows.Cast<DataGridViewRow>().FirstOrDefault();
            if (row == null) return;
            var tunnel = row.Tag as TunnelInfo;
            if (tunnel == null) return;

            //listBoxTunnels.Items.Remove(tunnel);
            this.tunnelsGridView.Rows.Remove(row);
            this.textBoxTunnelName.Text = tunnel.Name;
            this.textBoxSourcePort.Text = tunnel.LocalPort;
            this.textBoxDestHost.Text = tunnel.RemoteHostname;
            this.textBoxDestPort.Text = tunnel.RemotePort;
            switch (tunnel.Type)
            {
            case TunnelType.Local:
                this.radioButtonLocal.Checked = true;
                break;
            case TunnelType.Remote:
                this.radioButtonRemote.Checked = true;
                break;
            case TunnelType.Dynamic:
                this.radioButtonDynamic.Checked = true;
                break;
            }
            this.Modified = true;
        }

        private void tunnelsGridView_SelectionChanged(object sender, EventArgs e)
        {
            this.buttonRemoveTunnel.Enabled = this.selectedTunnel() != null;
        }

        #endregion

        #region Edit host

        private void buttonApply_Click(object sender, EventArgs e)
        {
            this.formToHost();
            this.Modified = false;
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            if (!this.buttonAddTunnelReminder())
                return;
            this.formToHost();
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        #endregion

        private void btnLoadPrivateKey_Click(object sender, EventArgs e)
        {
            if (this.openPrivateKeyFileDialog.ShowDialog(this) != DialogResult.OK)
            {
                return;
            }

            var filename = this.openPrivateKeyFileDialog.FileName;
            this._loadedPrivateKey = File.ReadAllText(filename, Encoding.ASCII);
            this.lblPrivateKeyFilename.Text = Path.GetFileName(filename);
        }

        private void rbxPrivateKey_CheckedChanged(object sender, EventArgs e)
        {
            var privateKeysSelected = this.rbxPrivateKey.Checked;
            this.tbxPassword.Enabled = !privateKeysSelected;
            this.btnLoadPrivateKey.Enabled = privateKeysSelected;
            this.tbxPassphrase.Enabled = privateKeysSelected;
        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }
    }
}
