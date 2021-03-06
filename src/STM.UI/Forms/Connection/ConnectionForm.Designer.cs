﻿namespace STM.UI.Forms.Connection
{
    partial class ConnectionForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.Label connectionNameLabel;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConnectionForm));
            System.Windows.Forms.Label addressLabel;
            System.Windows.Forms.Label userNameLabel;
            System.Windows.Forms.Label parentConnectionLabel;
            System.Windows.Forms.Label tunnelSrcAddressLabel;
            System.Windows.Forms.Label tunnelDstAddressLabel;
            System.Windows.Forms.Label tunnelNameLabel;
            System.Windows.Forms.Label tunnelSrcHostLabel;
            System.Windows.Forms.Label remoteCommandLabel;
            System.Windows.Forms.Label proxyLabel;
            this.tunnelsLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.newTunnelGroupBox = new System.Windows.Forms.GroupBox();
            this.newTunnelPanel = new System.Windows.Forms.TableLayoutPanel();
            this.tunnelTypeLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.tunnelTypeFlowLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.tunnelTypeLocalRadioButton = new System.Windows.Forms.RadioButton();
            this.tunnelTypeRemoteRadioButton = new System.Windows.Forms.RadioButton();
            this.tunnelTypeDynamicRadioButton = new System.Windows.Forms.RadioButton();
            this.buttonAddTunnel = new System.Windows.Forms.Button();
            this.tunnelEditPanel = new System.Windows.Forms.TableLayoutPanel();
            this.tunnelNameTextBox = new System.Windows.Forms.TextBox();
            this.tunnelDstAddressSeparatorLabel = new System.Windows.Forms.Label();
            this.tunnelSrcAddressSeparatorLabel = new System.Windows.Forms.Label();
            this.tunnelDstHostTextBox = new System.Windows.Forms.TextBox();
            this.tunnelDstPortTextBox = new System.Windows.Forms.TextBox();
            this.tunnelSrcPortTextBox = new System.Windows.Forms.TextBox();
            this.tunnelsGridView = new System.Windows.Forms.DataGridView();
            this.tunnelNameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tunnelTypeColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tunnelSrcPortColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tunnelDstHostColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tunnelDstPortColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.removeTunnelButton = new System.Windows.Forms.Button();
            this.connectionNameTextBox = new System.Windows.Forms.TextBox();
            this.hostNameTextBox = new System.Windows.Forms.TextBox();
            this.addressSeparatorLabel = new System.Windows.Forms.Label();
            this.userNameTextBox = new System.Windows.Forms.TextBox();
            this.mainLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.proxyComboBox = new System.Windows.Forms.ComboBox();
            this.authenticationGroupBox = new System.Windows.Forms.GroupBox();
            this.privateKeyFileNameLabel = new System.Windows.Forms.Label();
            this.passphraseLabel = new System.Windows.Forms.Label();
            this.loadPrivateKeyButton = new System.Windows.Forms.Button();
            this.passphraseTextBox = new System.Windows.Forms.TextBox();
            this.usePrivateKeyRadioButton = new System.Windows.Forms.RadioButton();
            this.usePasswordRadioButton = new System.Windows.Forms.RadioButton();
            this.passwordTextBox = new System.Windows.Forms.TextBox();
            this.addressPanel = new System.Windows.Forms.Panel();
            this.portTextBox = new System.Windows.Forms.TextBox();
            this.parentConnectionComboBox = new System.Windows.Forms.ComboBox();
            this.remoteCommandTextBox = new System.Windows.Forms.TextBox();
            this.createButton = new System.Windows.Forms.Button();
            this.dialogButtonsPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.applyButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.okButton = new System.Windows.Forms.Button();
            this.myErrorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.rootTableLayout = new System.Windows.Forms.TableLayoutPanel();
            this.navigationList = new System.Windows.Forms.ListBox();
            this.tunnelsPanel = new System.Windows.Forms.Panel();
            this.mainPanel = new System.Windows.Forms.Panel();
            connectionNameLabel = new System.Windows.Forms.Label();
            addressLabel = new System.Windows.Forms.Label();
            userNameLabel = new System.Windows.Forms.Label();
            parentConnectionLabel = new System.Windows.Forms.Label();
            tunnelSrcAddressLabel = new System.Windows.Forms.Label();
            tunnelDstAddressLabel = new System.Windows.Forms.Label();
            tunnelNameLabel = new System.Windows.Forms.Label();
            tunnelSrcHostLabel = new System.Windows.Forms.Label();
            remoteCommandLabel = new System.Windows.Forms.Label();
            proxyLabel = new System.Windows.Forms.Label();
            this.tunnelsLayoutPanel.SuspendLayout();
            this.newTunnelGroupBox.SuspendLayout();
            this.newTunnelPanel.SuspendLayout();
            this.tunnelTypeLayoutPanel.SuspendLayout();
            this.tunnelTypeFlowLayoutPanel.SuspendLayout();
            this.tunnelEditPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tunnelsGridView)).BeginInit();
            this.mainLayoutPanel.SuspendLayout();
            this.authenticationGroupBox.SuspendLayout();
            this.addressPanel.SuspendLayout();
            this.dialogButtonsPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.myErrorProvider)).BeginInit();
            this.rootTableLayout.SuspendLayout();
            this.tunnelsPanel.SuspendLayout();
            this.mainPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // connectionNameLabel
            // 
            resources.ApplyResources(connectionNameLabel, "connectionNameLabel");
            connectionNameLabel.Name = "connectionNameLabel";
            // 
            // addressLabel
            // 
            resources.ApplyResources(addressLabel, "addressLabel");
            addressLabel.Name = "addressLabel";
            // 
            // userNameLabel
            // 
            resources.ApplyResources(userNameLabel, "userNameLabel");
            userNameLabel.Name = "userNameLabel";
            // 
            // parentConnectionLabel
            // 
            resources.ApplyResources(parentConnectionLabel, "parentConnectionLabel");
            parentConnectionLabel.Name = "parentConnectionLabel";
            // 
            // tunnelSrcAddressLabel
            // 
            resources.ApplyResources(tunnelSrcAddressLabel, "tunnelSrcAddressLabel");
            tunnelSrcAddressLabel.Name = "tunnelSrcAddressLabel";
            // 
            // tunnelDstAddressLabel
            // 
            resources.ApplyResources(tunnelDstAddressLabel, "tunnelDstAddressLabel");
            tunnelDstAddressLabel.Name = "tunnelDstAddressLabel";
            // 
            // tunnelNameLabel
            // 
            resources.ApplyResources(tunnelNameLabel, "tunnelNameLabel");
            tunnelNameLabel.Name = "tunnelNameLabel";
            // 
            // tunnelSrcHostLabel
            // 
            resources.ApplyResources(tunnelSrcHostLabel, "tunnelSrcHostLabel");
            tunnelSrcHostLabel.Name = "tunnelSrcHostLabel";
            // 
            // remoteCommandLabel
            // 
            resources.ApplyResources(remoteCommandLabel, "remoteCommandLabel");
            remoteCommandLabel.Name = "remoteCommandLabel";
            // 
            // proxyLabel
            // 
            resources.ApplyResources(proxyLabel, "proxyLabel");
            proxyLabel.Name = "proxyLabel";
            // 
            // tunnelsLayoutPanel
            // 
            resources.ApplyResources(this.tunnelsLayoutPanel, "tunnelsLayoutPanel");
            this.tunnelsLayoutPanel.Controls.Add(this.newTunnelGroupBox, 0, 2);
            this.tunnelsLayoutPanel.Controls.Add(this.tunnelsGridView, 0, 1);
            this.tunnelsLayoutPanel.Controls.Add(this.removeTunnelButton, 0, 0);
            this.tunnelsLayoutPanel.Name = "tunnelsLayoutPanel";
            // 
            // newTunnelGroupBox
            // 
            resources.ApplyResources(this.newTunnelGroupBox, "newTunnelGroupBox");
            this.newTunnelGroupBox.Controls.Add(this.newTunnelPanel);
            this.newTunnelGroupBox.Name = "newTunnelGroupBox";
            this.newTunnelGroupBox.TabStop = false;
            // 
            // newTunnelPanel
            // 
            resources.ApplyResources(this.newTunnelPanel, "newTunnelPanel");
            this.newTunnelPanel.Controls.Add(this.tunnelTypeLayoutPanel, 0, 1);
            this.newTunnelPanel.Controls.Add(this.tunnelEditPanel, 0, 0);
            this.newTunnelPanel.Name = "newTunnelPanel";
            // 
            // tunnelTypeLayoutPanel
            // 
            resources.ApplyResources(this.tunnelTypeLayoutPanel, "tunnelTypeLayoutPanel");
            this.tunnelTypeLayoutPanel.Controls.Add(this.tunnelTypeFlowLayoutPanel, 0, 0);
            this.tunnelTypeLayoutPanel.Controls.Add(this.buttonAddTunnel, 2, 0);
            this.tunnelTypeLayoutPanel.Name = "tunnelTypeLayoutPanel";
            // 
            // tunnelTypeFlowLayoutPanel
            // 
            resources.ApplyResources(this.tunnelTypeFlowLayoutPanel, "tunnelTypeFlowLayoutPanel");
            this.tunnelTypeFlowLayoutPanel.Controls.Add(this.tunnelTypeLocalRadioButton);
            this.tunnelTypeFlowLayoutPanel.Controls.Add(this.tunnelTypeRemoteRadioButton);
            this.tunnelTypeFlowLayoutPanel.Controls.Add(this.tunnelTypeDynamicRadioButton);
            this.tunnelTypeFlowLayoutPanel.Name = "tunnelTypeFlowLayoutPanel";
            // 
            // tunnelTypeLocalRadioButton
            // 
            resources.ApplyResources(this.tunnelTypeLocalRadioButton, "tunnelTypeLocalRadioButton");
            this.tunnelTypeLocalRadioButton.Checked = true;
            this.tunnelTypeLocalRadioButton.Name = "tunnelTypeLocalRadioButton";
            this.tunnelTypeLocalRadioButton.TabStop = true;
            this.tunnelTypeLocalRadioButton.UseVisualStyleBackColor = true;
            // 
            // tunnelTypeRemoteRadioButton
            // 
            resources.ApplyResources(this.tunnelTypeRemoteRadioButton, "tunnelTypeRemoteRadioButton");
            this.tunnelTypeRemoteRadioButton.Name = "tunnelTypeRemoteRadioButton";
            this.tunnelTypeRemoteRadioButton.UseVisualStyleBackColor = true;
            // 
            // tunnelTypeDynamicRadioButton
            // 
            resources.ApplyResources(this.tunnelTypeDynamicRadioButton, "tunnelTypeDynamicRadioButton");
            this.tunnelTypeDynamicRadioButton.Name = "tunnelTypeDynamicRadioButton";
            this.tunnelTypeDynamicRadioButton.UseVisualStyleBackColor = true;
            this.tunnelTypeDynamicRadioButton.CheckedChanged += new System.EventHandler(this.tunnelTypeDynamicRadioButton_CheckedChanged);
            // 
            // buttonAddTunnel
            // 
            resources.ApplyResources(this.buttonAddTunnel, "buttonAddTunnel");
            this.buttonAddTunnel.Name = "buttonAddTunnel";
            this.buttonAddTunnel.UseVisualStyleBackColor = true;
            this.buttonAddTunnel.Click += new System.EventHandler(this.addTunnelButton_Click);
            // 
            // tunnelEditPanel
            // 
            resources.ApplyResources(this.tunnelEditPanel, "tunnelEditPanel");
            this.tunnelEditPanel.Controls.Add(tunnelNameLabel, 0, 0);
            this.tunnelEditPanel.Controls.Add(this.tunnelNameTextBox, 1, 0);
            this.tunnelEditPanel.Controls.Add(this.tunnelDstAddressSeparatorLabel, 2, 2);
            this.tunnelEditPanel.Controls.Add(this.tunnelSrcAddressSeparatorLabel, 2, 1);
            this.tunnelEditPanel.Controls.Add(this.tunnelDstHostTextBox, 1, 2);
            this.tunnelEditPanel.Controls.Add(tunnelSrcAddressLabel, 0, 1);
            this.tunnelEditPanel.Controls.Add(tunnelDstAddressLabel, 0, 2);
            this.tunnelEditPanel.Controls.Add(this.tunnelDstPortTextBox, 3, 2);
            this.tunnelEditPanel.Controls.Add(tunnelSrcHostLabel, 1, 1);
            this.tunnelEditPanel.Controls.Add(this.tunnelSrcPortTextBox, 3, 1);
            this.tunnelEditPanel.Name = "tunnelEditPanel";
            // 
            // tunnelNameTextBox
            // 
            resources.ApplyResources(this.tunnelNameTextBox, "tunnelNameTextBox");
            this.myErrorProvider.SetIconPadding(this.tunnelNameTextBox, ((int)(resources.GetObject("tunnelNameTextBox.IconPadding"))));
            this.tunnelNameTextBox.Name = "tunnelNameTextBox";
            // 
            // tunnelDstAddressSeparatorLabel
            // 
            resources.ApplyResources(this.tunnelDstAddressSeparatorLabel, "tunnelDstAddressSeparatorLabel");
            this.tunnelDstAddressSeparatorLabel.Name = "tunnelDstAddressSeparatorLabel";
            // 
            // tunnelSrcAddressSeparatorLabel
            // 
            resources.ApplyResources(this.tunnelSrcAddressSeparatorLabel, "tunnelSrcAddressSeparatorLabel");
            this.tunnelSrcAddressSeparatorLabel.Name = "tunnelSrcAddressSeparatorLabel";
            // 
            // tunnelDstHostTextBox
            // 
            resources.ApplyResources(this.tunnelDstHostTextBox, "tunnelDstHostTextBox");
            this.myErrorProvider.SetIconAlignment(this.tunnelDstHostTextBox, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("tunnelDstHostTextBox.IconAlignment"))));
            this.myErrorProvider.SetIconPadding(this.tunnelDstHostTextBox, ((int)(resources.GetObject("tunnelDstHostTextBox.IconPadding"))));
            this.tunnelDstHostTextBox.Name = "tunnelDstHostTextBox";
            // 
            // tunnelDstPortTextBox
            // 
            resources.ApplyResources(this.tunnelDstPortTextBox, "tunnelDstPortTextBox");
            this.myErrorProvider.SetIconPadding(this.tunnelDstPortTextBox, ((int)(resources.GetObject("tunnelDstPortTextBox.IconPadding"))));
            this.tunnelDstPortTextBox.Name = "tunnelDstPortTextBox";
            // 
            // tunnelSrcPortTextBox
            // 
            resources.ApplyResources(this.tunnelSrcPortTextBox, "tunnelSrcPortTextBox");
            this.myErrorProvider.SetIconPadding(this.tunnelSrcPortTextBox, ((int)(resources.GetObject("tunnelSrcPortTextBox.IconPadding"))));
            this.tunnelSrcPortTextBox.Name = "tunnelSrcPortTextBox";
            // 
            // tunnelsGridView
            // 
            this.tunnelsGridView.AllowUserToAddRows = false;
            this.tunnelsGridView.AllowUserToDeleteRows = false;
            this.tunnelsGridView.AllowUserToResizeRows = false;
            resources.ApplyResources(this.tunnelsGridView, "tunnelsGridView");
            this.tunnelsGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.tunnelsGridView.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.tunnelsGridView.BackgroundColor = System.Drawing.SystemColors.Window;
            this.tunnelsGridView.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.tunnelsGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.tunnelsGridView.ColumnHeadersVisible = false;
            this.tunnelsGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.tunnelNameColumn,
            this.tunnelTypeColumn,
            this.tunnelSrcPortColumn,
            this.tunnelDstHostColumn,
            this.tunnelDstPortColumn});
            this.tunnelsGridView.MultiSelect = false;
            this.tunnelsGridView.Name = "tunnelsGridView";
            this.tunnelsGridView.ReadOnly = true;
            this.tunnelsGridView.RowHeadersVisible = false;
            this.tunnelsGridView.RowTemplate.Height = 18;
            this.tunnelsGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.tunnelsGridView.SelectionChanged += new System.EventHandler(this.tunnelsGridView_SelectionChanged);
            // 
            // tunnelNameColumn
            // 
            this.tunnelNameColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            resources.ApplyResources(this.tunnelNameColumn, "tunnelNameColumn");
            this.tunnelNameColumn.Name = "tunnelNameColumn";
            this.tunnelNameColumn.ReadOnly = true;
            // 
            // tunnelTypeColumn
            // 
            this.tunnelTypeColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCellsExceptHeader;
            resources.ApplyResources(this.tunnelTypeColumn, "tunnelTypeColumn");
            this.tunnelTypeColumn.Name = "tunnelTypeColumn";
            this.tunnelTypeColumn.ReadOnly = true;
            // 
            // tunnelSrcPortColumn
            // 
            this.tunnelSrcPortColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCellsExceptHeader;
            resources.ApplyResources(this.tunnelSrcPortColumn, "tunnelSrcPortColumn");
            this.tunnelSrcPortColumn.Name = "tunnelSrcPortColumn";
            this.tunnelSrcPortColumn.ReadOnly = true;
            // 
            // tunnelDstHostColumn
            // 
            this.tunnelDstHostColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCellsExceptHeader;
            resources.ApplyResources(this.tunnelDstHostColumn, "tunnelDstHostColumn");
            this.tunnelDstHostColumn.Name = "tunnelDstHostColumn";
            this.tunnelDstHostColumn.ReadOnly = true;
            // 
            // tunnelDstPortColumn
            // 
            this.tunnelDstPortColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCellsExceptHeader;
            resources.ApplyResources(this.tunnelDstPortColumn, "tunnelDstPortColumn");
            this.tunnelDstPortColumn.Name = "tunnelDstPortColumn";
            this.tunnelDstPortColumn.ReadOnly = true;
            // 
            // removeTunnelButton
            // 
            resources.ApplyResources(this.removeTunnelButton, "removeTunnelButton");
            this.removeTunnelButton.Name = "removeTunnelButton";
            this.removeTunnelButton.UseVisualStyleBackColor = true;
            this.removeTunnelButton.Click += new System.EventHandler(this.removeTunnelButton_Click);
            // 
            // connectionNameTextBox
            // 
            resources.ApplyResources(this.connectionNameTextBox, "connectionNameTextBox");
            this.myErrorProvider.SetIconPadding(this.connectionNameTextBox, ((int)(resources.GetObject("connectionNameTextBox.IconPadding"))));
            this.connectionNameTextBox.Name = "connectionNameTextBox";
            // 
            // hostNameTextBox
            // 
            resources.ApplyResources(this.hostNameTextBox, "hostNameTextBox");
            this.myErrorProvider.SetIconPadding(this.hostNameTextBox, ((int)(resources.GetObject("hostNameTextBox.IconPadding"))));
            this.hostNameTextBox.Name = "hostNameTextBox";
            // 
            // addressSeparatorLabel
            // 
            resources.ApplyResources(this.addressSeparatorLabel, "addressSeparatorLabel");
            this.addressSeparatorLabel.Name = "addressSeparatorLabel";
            // 
            // userNameTextBox
            // 
            resources.ApplyResources(this.userNameTextBox, "userNameTextBox");
            this.myErrorProvider.SetIconPadding(this.userNameTextBox, ((int)(resources.GetObject("userNameTextBox.IconPadding"))));
            this.userNameTextBox.Name = "userNameTextBox";
            // 
            // mainLayoutPanel
            // 
            resources.ApplyResources(this.mainLayoutPanel, "mainLayoutPanel");
            this.mainLayoutPanel.Controls.Add(this.proxyComboBox, 1, 6);
            this.mainLayoutPanel.Controls.Add(this.authenticationGroupBox, 0, 3);
            this.mainLayoutPanel.Controls.Add(this.userNameTextBox, 1, 2);
            this.mainLayoutPanel.Controls.Add(this.connectionNameTextBox, 1, 0);
            this.mainLayoutPanel.Controls.Add(this.addressPanel, 1, 1);
            this.mainLayoutPanel.Controls.Add(connectionNameLabel, 0, 0);
            this.mainLayoutPanel.Controls.Add(userNameLabel, 0, 2);
            this.mainLayoutPanel.Controls.Add(addressLabel, 0, 1);
            this.mainLayoutPanel.Controls.Add(proxyLabel, 0, 6);
            this.mainLayoutPanel.Controls.Add(parentConnectionLabel, 0, 5);
            this.mainLayoutPanel.Controls.Add(remoteCommandLabel, 0, 4);
            this.mainLayoutPanel.Controls.Add(this.parentConnectionComboBox, 1, 5);
            this.mainLayoutPanel.Controls.Add(this.remoteCommandTextBox, 1, 4);
            this.mainLayoutPanel.Name = "mainLayoutPanel";
            // 
            // proxyComboBox
            // 
            resources.ApplyResources(this.proxyComboBox, "proxyComboBox");
            this.proxyComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.proxyComboBox.FormattingEnabled = true;
            this.myErrorProvider.SetIconAlignment(this.proxyComboBox, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("proxyComboBox.IconAlignment"))));
            this.proxyComboBox.Name = "proxyComboBox";
            // 
            // authenticationGroupBox
            // 
            this.mainLayoutPanel.SetColumnSpan(this.authenticationGroupBox, 2);
            this.authenticationGroupBox.Controls.Add(this.privateKeyFileNameLabel);
            this.authenticationGroupBox.Controls.Add(this.passphraseLabel);
            this.authenticationGroupBox.Controls.Add(this.loadPrivateKeyButton);
            this.authenticationGroupBox.Controls.Add(this.passphraseTextBox);
            this.authenticationGroupBox.Controls.Add(this.usePrivateKeyRadioButton);
            this.authenticationGroupBox.Controls.Add(this.usePasswordRadioButton);
            this.authenticationGroupBox.Controls.Add(this.passwordTextBox);
            resources.ApplyResources(this.authenticationGroupBox, "authenticationGroupBox");
            this.authenticationGroupBox.Name = "authenticationGroupBox";
            this.authenticationGroupBox.TabStop = false;
            // 
            // privateKeyFileNameLabel
            // 
            resources.ApplyResources(this.privateKeyFileNameLabel, "privateKeyFileNameLabel");
            this.myErrorProvider.SetIconPadding(this.privateKeyFileNameLabel, ((int)(resources.GetObject("privateKeyFileNameLabel.IconPadding"))));
            this.privateKeyFileNameLabel.Name = "privateKeyFileNameLabel";
            // 
            // passphraseLabel
            // 
            resources.ApplyResources(this.passphraseLabel, "passphraseLabel");
            this.passphraseLabel.Name = "passphraseLabel";
            // 
            // loadPrivateKeyButton
            // 
            resources.ApplyResources(this.loadPrivateKeyButton, "loadPrivateKeyButton");
            this.loadPrivateKeyButton.Name = "loadPrivateKeyButton";
            this.loadPrivateKeyButton.UseVisualStyleBackColor = true;
            this.loadPrivateKeyButton.Click += new System.EventHandler(this.loadPrivateKeyButton_Click);
            // 
            // passphraseTextBox
            // 
            resources.ApplyResources(this.passphraseTextBox, "passphraseTextBox");
            this.passphraseTextBox.Name = "passphraseTextBox";
            this.passphraseTextBox.UseSystemPasswordChar = true;
            // 
            // usePrivateKeyRadioButton
            // 
            resources.ApplyResources(this.usePrivateKeyRadioButton, "usePrivateKeyRadioButton");
            this.usePrivateKeyRadioButton.Name = "usePrivateKeyRadioButton";
            this.usePrivateKeyRadioButton.UseVisualStyleBackColor = true;
            this.usePrivateKeyRadioButton.CheckedChanged += new System.EventHandler(this.usePrivateKeyRadioButton_CheckedChanged);
            // 
            // usePasswordRadioButton
            // 
            resources.ApplyResources(this.usePasswordRadioButton, "usePasswordRadioButton");
            this.usePasswordRadioButton.Checked = true;
            this.usePasswordRadioButton.Name = "usePasswordRadioButton";
            this.usePasswordRadioButton.TabStop = true;
            this.usePasswordRadioButton.UseVisualStyleBackColor = true;
            // 
            // passwordTextBox
            // 
            resources.ApplyResources(this.passwordTextBox, "passwordTextBox");
            this.myErrorProvider.SetIconPadding(this.passwordTextBox, ((int)(resources.GetObject("passwordTextBox.IconPadding"))));
            this.passwordTextBox.Name = "passwordTextBox";
            this.passwordTextBox.UseSystemPasswordChar = true;
            // 
            // addressPanel
            // 
            resources.ApplyResources(this.addressPanel, "addressPanel");
            this.addressPanel.Controls.Add(this.hostNameTextBox);
            this.addressPanel.Controls.Add(this.portTextBox);
            this.addressPanel.Controls.Add(this.addressSeparatorLabel);
            this.addressPanel.Name = "addressPanel";
            // 
            // portTextBox
            // 
            resources.ApplyResources(this.portTextBox, "portTextBox");
            this.myErrorProvider.SetIconPadding(this.portTextBox, ((int)(resources.GetObject("portTextBox.IconPadding"))));
            this.portTextBox.Name = "portTextBox";
            // 
            // parentConnectionComboBox
            // 
            resources.ApplyResources(this.parentConnectionComboBox, "parentConnectionComboBox");
            this.parentConnectionComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.parentConnectionComboBox.FormattingEnabled = true;
            this.myErrorProvider.SetIconAlignment(this.parentConnectionComboBox, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("parentConnectionComboBox.IconAlignment"))));
            this.parentConnectionComboBox.Name = "parentConnectionComboBox";
            // 
            // remoteCommandTextBox
            // 
            resources.ApplyResources(this.remoteCommandTextBox, "remoteCommandTextBox");
            this.remoteCommandTextBox.Name = "remoteCommandTextBox";
            // 
            // createButton
            // 
            resources.ApplyResources(this.createButton, "createButton");
            this.createButton.Name = "createButton";
            this.createButton.UseVisualStyleBackColor = true;
            this.createButton.Click += new System.EventHandler(this.createButton_Click);
            // 
            // dialogButtonsPanel
            // 
            resources.ApplyResources(this.dialogButtonsPanel, "dialogButtonsPanel");
            this.rootTableLayout.SetColumnSpan(this.dialogButtonsPanel, 3);
            this.dialogButtonsPanel.Controls.Add(this.applyButton);
            this.dialogButtonsPanel.Controls.Add(this.cancelButton);
            this.dialogButtonsPanel.Controls.Add(this.okButton);
            this.dialogButtonsPanel.Controls.Add(this.createButton);
            this.dialogButtonsPanel.Name = "dialogButtonsPanel";
            // 
            // applyButton
            // 
            resources.ApplyResources(this.applyButton, "applyButton");
            this.applyButton.Name = "applyButton";
            this.applyButton.UseVisualStyleBackColor = true;
            this.applyButton.Click += new System.EventHandler(this.applyButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            resources.ApplyResources(this.cancelButton, "cancelButton");
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // okButton
            // 
            resources.ApplyResources(this.okButton, "okButton");
            this.okButton.Name = "okButton";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // myErrorProvider
            // 
            this.myErrorProvider.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
            this.myErrorProvider.ContainerControl = this;
            resources.ApplyResources(this.myErrorProvider, "myErrorProvider");
            // 
            // rootTableLayout
            // 
            resources.ApplyResources(this.rootTableLayout, "rootTableLayout");
            this.rootTableLayout.Controls.Add(this.navigationList, 0, 0);
            this.rootTableLayout.Controls.Add(this.tunnelsPanel, 2, 0);
            this.rootTableLayout.Controls.Add(this.mainPanel, 1, 0);
            this.rootTableLayout.Controls.Add(this.dialogButtonsPanel, 1, 1);
            this.rootTableLayout.Name = "rootTableLayout";
            // 
            // navigationList
            // 
            resources.ApplyResources(this.navigationList, "navigationList");
            this.navigationList.FormattingEnabled = true;
            this.navigationList.Items.AddRange(new object[] {
            resources.GetString("navigationList.Items"),
            resources.GetString("navigationList.Items1")});
            this.navigationList.Name = "navigationList";
            this.navigationList.SelectedIndexChanged += new System.EventHandler(this.navigationList_SelectedIndexChanged);
            // 
            // tunnelsPanel
            // 
            resources.ApplyResources(this.tunnelsPanel, "tunnelsPanel");
            this.tunnelsPanel.Controls.Add(this.tunnelsLayoutPanel);
            this.tunnelsPanel.Name = "tunnelsPanel";
            // 
            // mainPanel
            // 
            resources.ApplyResources(this.mainPanel, "mainPanel");
            this.mainPanel.Controls.Add(this.mainLayoutPanel);
            this.mainPanel.Name = "mainPanel";
            // 
            // ConnectionForm
            // 
            this.AcceptButton = this.createButton;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelButton;
            this.Controls.Add(this.rootTableLayout);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ConnectionForm";
            this.tunnelsLayoutPanel.ResumeLayout(false);
            this.newTunnelGroupBox.ResumeLayout(false);
            this.newTunnelGroupBox.PerformLayout();
            this.newTunnelPanel.ResumeLayout(false);
            this.newTunnelPanel.PerformLayout();
            this.tunnelTypeLayoutPanel.ResumeLayout(false);
            this.tunnelTypeLayoutPanel.PerformLayout();
            this.tunnelTypeFlowLayoutPanel.ResumeLayout(false);
            this.tunnelTypeFlowLayoutPanel.PerformLayout();
            this.tunnelEditPanel.ResumeLayout(false);
            this.tunnelEditPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tunnelsGridView)).EndInit();
            this.mainLayoutPanel.ResumeLayout(false);
            this.mainLayoutPanel.PerformLayout();
            this.authenticationGroupBox.ResumeLayout(false);
            this.authenticationGroupBox.PerformLayout();
            this.addressPanel.ResumeLayout(false);
            this.addressPanel.PerformLayout();
            this.dialogButtonsPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.myErrorProvider)).EndInit();
            this.rootTableLayout.ResumeLayout(false);
            this.rootTableLayout.PerformLayout();
            this.tunnelsPanel.ResumeLayout(false);
            this.mainPanel.ResumeLayout(false);
            this.mainPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox connectionNameTextBox;
        private System.Windows.Forms.TextBox hostNameTextBox;
        private System.Windows.Forms.Label addressSeparatorLabel;
        private System.Windows.Forms.TextBox userNameTextBox;
        private System.Windows.Forms.TableLayoutPanel mainLayoutPanel;
        private System.Windows.Forms.Panel addressPanel;
        private System.Windows.Forms.ComboBox parentConnectionComboBox;
        private System.Windows.Forms.Button removeTunnelButton;
        private System.Windows.Forms.TextBox tunnelSrcPortTextBox;
        private System.Windows.Forms.Button buttonAddTunnel;
        private System.Windows.Forms.RadioButton tunnelTypeDynamicRadioButton;
        private System.Windows.Forms.RadioButton tunnelTypeRemoteRadioButton;
        private System.Windows.Forms.RadioButton tunnelTypeLocalRadioButton;
        private System.Windows.Forms.Button createButton;
        private System.Windows.Forms.TextBox portTextBox;
        private System.Windows.Forms.ErrorProvider myErrorProvider;
        private System.Windows.Forms.TextBox tunnelDstHostTextBox;
        private System.Windows.Forms.Label tunnelSrcAddressSeparatorLabel;
        private System.Windows.Forms.Label tunnelDstAddressSeparatorLabel;
        private System.Windows.Forms.TextBox tunnelNameTextBox;
        private System.Windows.Forms.TextBox tunnelDstPortTextBox;
        private System.Windows.Forms.FlowLayoutPanel dialogButtonsPanel;
        private System.Windows.Forms.Button applyButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.DataGridView tunnelsGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn tunnelNameColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn tunnelTypeColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn tunnelSrcPortColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn tunnelDstHostColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn tunnelDstPortColumn;
        private System.Windows.Forms.GroupBox authenticationGroupBox;
        private System.Windows.Forms.Label privateKeyFileNameLabel;
        private System.Windows.Forms.Label passphraseLabel;
        private System.Windows.Forms.Button loadPrivateKeyButton;
        private System.Windows.Forms.TextBox passphraseTextBox;
        private System.Windows.Forms.RadioButton usePrivateKeyRadioButton;
        private System.Windows.Forms.RadioButton usePasswordRadioButton;
        private System.Windows.Forms.TextBox passwordTextBox;
        private System.Windows.Forms.TextBox remoteCommandTextBox;
        private System.Windows.Forms.TableLayoutPanel tunnelEditPanel;
        private System.Windows.Forms.TableLayoutPanel tunnelTypeLayoutPanel;
        private System.Windows.Forms.FlowLayoutPanel tunnelTypeFlowLayoutPanel;
        private System.Windows.Forms.TableLayoutPanel tunnelsLayoutPanel;
        private System.Windows.Forms.TableLayoutPanel rootTableLayout;
        private System.Windows.Forms.Panel tunnelsPanel;
        private System.Windows.Forms.Panel mainPanel;
        private System.Windows.Forms.ListBox navigationList;
        private System.Windows.Forms.ComboBox proxyComboBox;
        private System.Windows.Forms.GroupBox newTunnelGroupBox;
        private System.Windows.Forms.TableLayoutPanel newTunnelPanel;
    }
}