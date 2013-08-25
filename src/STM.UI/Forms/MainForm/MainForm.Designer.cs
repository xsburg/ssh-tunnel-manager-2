namespace STM.UI.Forms.MainForm
{
    partial class MainForm
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
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("Stopped");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("Starting");
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("Waiting");
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("Started");
            System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("All hosts", new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2,
            treeNode3,
            treeNode4});
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.mainMenuStrip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.changeStorageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.changePasswordToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.keepConnectionsExitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hostToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addHostToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editHostToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.removeHostToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.startToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stopToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.startPuTTYToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.startPsftpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.startFileZillaSFTPToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.theToolStrip = new System.Windows.Forms.ToolStrip();
            this.toolStripButtonSave = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonAddHost = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonRemoveHost = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonEditHost = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonStart = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonStop = new System.Windows.Forms.ToolStripButton();
            this.theStatusStrip = new System.Windows.Forms.StatusStrip();
            this.splitContainerH1 = new System.Windows.Forms.SplitContainer();
            this.treeViewFilter = new System.Windows.Forms.TreeView();
            this.splitContainerV1 = new System.Windows.Forms.SplitContainer();
            this.hostsGridView = new System.Windows.Forms.DataGridView();
            this.hgwStatusIconColumn = new System.Windows.Forms.DataGridViewImageColumn();
            this.hgwNameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.hgwUsernameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.hgwHostnameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.hgwStatusColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.hgwDependsOnColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.mainMenuStrip.SuspendLayout();
            this.theToolStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerH1)).BeginInit();
            this.splitContainerH1.Panel1.SuspendLayout();
            this.splitContainerH1.Panel2.SuspendLayout();
            this.splitContainerH1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerV1)).BeginInit();
            this.splitContainerV1.Panel1.SuspendLayout();
            this.splitContainerV1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.hostsGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // mainMenuStrip
            // 
            this.mainMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.hostToolStripMenuItem,
            this.toolsToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.mainMenuStrip.Location = new System.Drawing.Point(0, 0);
            this.mainMenuStrip.Name = "mainMenuStrip";
            this.mainMenuStrip.Size = new System.Drawing.Size(826, 24);
            this.mainMenuStrip.TabIndex = 2;
            this.mainMenuStrip.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveToolStripMenuItem,
            this.toolStripSeparator1,
            this.changeStorageToolStripMenuItem,
            this.changePasswordToolStripMenuItem,
            this.toolStripSeparator4,
            this.keepConnectionsExitToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Image = global::STM.UI.Properties.Resources.disk;
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(209, 22);
            this.saveToolStripMenuItem.Text = "Sa&ve";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(206, 6);
            // 
            // changeStorageToolStripMenuItem
            // 
            this.changeStorageToolStripMenuItem.Image = global::STM.UI.Properties.Resources.databases__arrow;
            this.changeStorageToolStripMenuItem.Name = "changeStorageToolStripMenuItem";
            this.changeStorageToolStripMenuItem.Size = new System.Drawing.Size(209, 22);
            this.changeStorageToolStripMenuItem.Text = "&Change Storage...";
            // 
            // changePasswordToolStripMenuItem
            // 
            this.changePasswordToolStripMenuItem.Image = global::STM.UI.Properties.Resources.key__arrow;
            this.changePasswordToolStripMenuItem.Name = "changePasswordToolStripMenuItem";
            this.changePasswordToolStripMenuItem.Size = new System.Drawing.Size(209, 22);
            this.changePasswordToolStripMenuItem.Text = "C&hange Password...";
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(206, 6);
            // 
            // keepConnectionsExitToolStripMenuItem
            // 
            this.keepConnectionsExitToolStripMenuItem.Name = "keepConnectionsExitToolStripMenuItem";
            this.keepConnectionsExitToolStripMenuItem.Size = new System.Drawing.Size(209, 22);
            this.keepConnectionsExitToolStripMenuItem.Text = "E&xit but keep connections";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(209, 22);
            this.exitToolStripMenuItem.Text = "Ex&it";
            // 
            // hostToolStripMenuItem
            // 
            this.hostToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addHostToolStripMenuItem,
            this.editHostToolStripMenuItem,
            this.removeHostToolStripMenuItem,
            this.toolStripSeparator2,
            this.startToolStripMenuItem,
            this.stopToolStripMenuItem,
            this.toolStripSeparator7,
            this.startPuTTYToolStripMenuItem,
            this.startPsftpToolStripMenuItem,
            this.startFileZillaSFTPToolStripMenuItem});
            this.hostToolStripMenuItem.Name = "hostToolStripMenuItem";
            this.hostToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.hostToolStripMenuItem.Text = "&Host";
            // 
            // addHostToolStripMenuItem
            // 
            this.addHostToolStripMenuItem.Image = global::STM.UI.Properties.Resources.server__plus;
            this.addHostToolStripMenuItem.Name = "addHostToolStripMenuItem";
            this.addHostToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.addHostToolStripMenuItem.Text = "&Add host...";
            // 
            // editHostToolStripMenuItem
            // 
            this.editHostToolStripMenuItem.Image = global::STM.UI.Properties.Resources.server__pencil;
            this.editHostToolStripMenuItem.Name = "editHostToolStripMenuItem";
            this.editHostToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.editHostToolStripMenuItem.Text = "&Edit host...";
            // 
            // removeHostToolStripMenuItem
            // 
            this.removeHostToolStripMenuItem.Image = global::STM.UI.Properties.Resources.server__minus;
            this.removeHostToolStripMenuItem.Name = "removeHostToolStripMenuItem";
            this.removeHostToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.Delete;
            this.removeHostToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.removeHostToolStripMenuItem.Text = "&Remove host";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(167, 6);
            // 
            // startToolStripMenuItem
            // 
            this.startToolStripMenuItem.Image = global::STM.UI.Properties.Resources.control;
            this.startToolStripMenuItem.Name = "startToolStripMenuItem";
            this.startToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F5;
            this.startToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.startToolStripMenuItem.Text = "&Start";
            // 
            // stopToolStripMenuItem
            // 
            this.stopToolStripMenuItem.Image = global::STM.UI.Properties.Resources.control_stop_square;
            this.stopToolStripMenuItem.Name = "stopToolStripMenuItem";
            this.stopToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.F5)));
            this.stopToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.stopToolStripMenuItem.Text = "S&top";
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(167, 6);
            // 
            // startPuTTYToolStripMenuItem
            // 
            this.startPuTTYToolStripMenuItem.Image = global::STM.UI.Properties.Resources.icon_16x16_putty;
            this.startPuTTYToolStripMenuItem.Name = "startPuTTYToolStripMenuItem";
            this.startPuTTYToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.startPuTTYToolStripMenuItem.Text = "Start &PuTTY";
            // 
            // startPsftpToolStripMenuItem
            // 
            this.startPsftpToolStripMenuItem.Image = global::STM.UI.Properties.Resources.psftp;
            this.startPsftpToolStripMenuItem.Name = "startPsftpToolStripMenuItem";
            this.startPsftpToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.startPsftpToolStripMenuItem.Text = "Start ps&ftp";
            // 
            // startFileZillaSFTPToolStripMenuItem
            // 
            this.startFileZillaSFTPToolStripMenuItem.Image = global::STM.UI.Properties.Resources.filezilla;
            this.startFileZillaSFTPToolStripMenuItem.Name = "startFileZillaSFTPToolStripMenuItem";
            this.startFileZillaSFTPToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.startFileZillaSFTPToolStripMenuItem.Text = "Start F&ileZilla SFTP";
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.optionsToolStripMenuItem});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.toolsToolStripMenuItem.Text = "&Tools";
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.Image = global::STM.UI.Properties.Resources.Gear;
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(125, 22);
            this.optionsToolStripMenuItem.Text = "&Options...";
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "H&elp";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(221, 22);
            this.aboutToolStripMenuItem.Text = "&About SSH Tunnel Manager";
            // 
            // theToolStrip
            // 
            this.theToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonSave,
            this.toolStripSeparator5,
            this.toolStripButtonAddHost,
            this.toolStripButtonRemoveHost,
            this.toolStripButtonEditHost,
            this.toolStripSeparator3,
            this.toolStripButtonStart,
            this.toolStripButtonStop});
            this.theToolStrip.Location = new System.Drawing.Point(0, 24);
            this.theToolStrip.Name = "theToolStrip";
            this.theToolStrip.Size = new System.Drawing.Size(826, 25);
            this.theToolStrip.TabIndex = 3;
            this.theToolStrip.Text = "toolStrip1";
            // 
            // toolStripButtonSave
            // 
            this.toolStripButtonSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonSave.Image = global::STM.UI.Properties.Resources.disk;
            this.toolStripButtonSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonSave.Name = "toolStripButtonSave";
            this.toolStripButtonSave.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonSave.Text = "Save";
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButtonAddHost
            // 
            this.toolStripButtonAddHost.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonAddHost.Image = global::STM.UI.Properties.Resources.server__plus;
            this.toolStripButtonAddHost.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonAddHost.Name = "toolStripButtonAddHost";
            this.toolStripButtonAddHost.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonAddHost.Text = "New host...";
            // 
            // toolStripButtonRemoveHost
            // 
            this.toolStripButtonRemoveHost.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonRemoveHost.Image = global::STM.UI.Properties.Resources.server__minus;
            this.toolStripButtonRemoveHost.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonRemoveHost.Name = "toolStripButtonRemoveHost";
            this.toolStripButtonRemoveHost.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonRemoveHost.Text = "Remove Host";
            // 
            // toolStripButtonEditHost
            // 
            this.toolStripButtonEditHost.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonEditHost.Image = global::STM.UI.Properties.Resources.server__pencil;
            this.toolStripButtonEditHost.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonEditHost.Name = "toolStripButtonEditHost";
            this.toolStripButtonEditHost.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonEditHost.Text = "Edit Host...";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButtonStart
            // 
            this.toolStripButtonStart.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonStart.Image = global::STM.UI.Properties.Resources.control;
            this.toolStripButtonStart.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonStart.Name = "toolStripButtonStart";
            this.toolStripButtonStart.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonStart.Text = "Start";
            // 
            // toolStripButtonStop
            // 
            this.toolStripButtonStop.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonStop.Image = global::STM.UI.Properties.Resources.control_stop_square;
            this.toolStripButtonStop.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonStop.Name = "toolStripButtonStop";
            this.toolStripButtonStop.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonStop.Text = "Stop";
            // 
            // theStatusStrip
            // 
            this.theStatusStrip.Location = new System.Drawing.Point(0, 535);
            this.theStatusStrip.Name = "theStatusStrip";
            this.theStatusStrip.Size = new System.Drawing.Size(826, 22);
            this.theStatusStrip.TabIndex = 4;
            this.theStatusStrip.Text = "statusStrip1";
            // 
            // splitContainerH1
            // 
            this.splitContainerH1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerH1.Location = new System.Drawing.Point(0, 49);
            this.splitContainerH1.Name = "splitContainerH1";
            // 
            // splitContainerH1.Panel1
            // 
            this.splitContainerH1.Panel1.Controls.Add(this.treeViewFilter);
            // 
            // splitContainerH1.Panel2
            // 
            this.splitContainerH1.Panel2.Controls.Add(this.splitContainerV1);
            this.splitContainerH1.Size = new System.Drawing.Size(826, 486);
            this.splitContainerH1.SplitterDistance = 143;
            this.splitContainerH1.TabIndex = 5;
            // 
            // treeViewFilter
            // 
            this.treeViewFilter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeViewFilter.Location = new System.Drawing.Point(0, 0);
            this.treeViewFilter.Name = "treeViewFilter";
            treeNode1.Name = "filterStoppedHostsNode";
            treeNode1.Tag = "1";
            treeNode1.Text = "Stopped";
            treeNode2.Name = "filterUnknownHostsNode";
            treeNode2.Tag = "2";
            treeNode2.Text = "Starting";
            treeNode3.Name = "filterWaitingHostsNode";
            treeNode3.Tag = "3";
            treeNode3.Text = "Waiting";
            treeNode4.Name = "filterStartedHostsNode";
            treeNode4.Tag = "4";
            treeNode4.Text = "Started";
            treeNode5.Name = "filterAllHostsNode";
            treeNode5.Tag = "";
            treeNode5.Text = "All hosts";
            this.treeViewFilter.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode5});
            this.treeViewFilter.Size = new System.Drawing.Size(143, 486);
            this.treeViewFilter.TabIndex = 0;
            // 
            // splitContainerV1
            // 
            this.splitContainerV1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerV1.Location = new System.Drawing.Point(0, 0);
            this.splitContainerV1.Name = "splitContainerV1";
            this.splitContainerV1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainerV1.Panel1
            // 
            this.splitContainerV1.Panel1.Controls.Add(this.hostsGridView);
            this.splitContainerV1.Size = new System.Drawing.Size(679, 486);
            this.splitContainerV1.SplitterDistance = 260;
            this.splitContainerV1.TabIndex = 1;
            // 
            // hostsGridView
            // 
            this.hostsGridView.AllowUserToAddRows = false;
            this.hostsGridView.AllowUserToDeleteRows = false;
            this.hostsGridView.AllowUserToResizeRows = false;
            this.hostsGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.hostsGridView.BackgroundColor = System.Drawing.SystemColors.Window;
            this.hostsGridView.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.hostsGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.hostsGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.hgwStatusIconColumn,
            this.hgwNameColumn,
            this.hgwUsernameColumn,
            this.hgwHostnameColumn,
            this.hgwStatusColumn,
            this.hgwDependsOnColumn});
            this.hostsGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.hostsGridView.Location = new System.Drawing.Point(0, 0);
            this.hostsGridView.MultiSelect = false;
            this.hostsGridView.Name = "hostsGridView";
            this.hostsGridView.RowHeadersVisible = false;
            this.hostsGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.hostsGridView.Size = new System.Drawing.Size(679, 260);
            this.hostsGridView.TabIndex = 0;
            // 
            // hgwStatusIconColumn
            // 
            this.hgwStatusIconColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.hgwStatusIconColumn.FillWeight = 27.62246F;
            this.hgwStatusIconColumn.HeaderText = "*";
            this.hgwStatusIconColumn.MinimumWidth = 20;
            this.hgwStatusIconColumn.Name = "hgwStatusIconColumn";
            this.hgwStatusIconColumn.ReadOnly = true;
            this.hgwStatusIconColumn.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.hgwStatusIconColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.hgwStatusIconColumn.Width = 25;
            // 
            // hgwNameColumn
            // 
            this.hgwNameColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.hgwNameColumn.FillWeight = 20F;
            this.hgwNameColumn.HeaderText = "Unique Name";
            this.hgwNameColumn.MinimumWidth = 20;
            this.hgwNameColumn.Name = "hgwNameColumn";
            this.hgwNameColumn.ReadOnly = true;
            this.hgwNameColumn.Width = 97;
            // 
            // hgwUsernameColumn
            // 
            this.hgwUsernameColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.hgwUsernameColumn.HeaderText = "Username";
            this.hgwUsernameColumn.Name = "hgwUsernameColumn";
            this.hgwUsernameColumn.ReadOnly = true;
            this.hgwUsernameColumn.Width = 80;
            // 
            // hgwHostnameColumn
            // 
            this.hgwHostnameColumn.FillWeight = 95.02126F;
            this.hgwHostnameColumn.HeaderText = "Host";
            this.hgwHostnameColumn.Name = "hgwHostnameColumn";
            this.hgwHostnameColumn.ReadOnly = true;
            // 
            // hgwStatusColumn
            // 
            this.hgwStatusColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.hgwStatusColumn.FillWeight = 95.02126F;
            this.hgwStatusColumn.HeaderText = "Status";
            this.hgwStatusColumn.Name = "hgwStatusColumn";
            this.hgwStatusColumn.ReadOnly = true;
            this.hgwStatusColumn.Width = 62;
            // 
            // hgwDependsOnColumn
            // 
            this.hgwDependsOnColumn.HeaderText = "Depends On";
            this.hgwDependsOnColumn.Name = "hgwDependsOnColumn";
            this.hgwDependsOnColumn.ReadOnly = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(826, 557);
            this.Controls.Add(this.splitContainerH1);
            this.Controls.Add(this.theStatusStrip);
            this.Controls.Add(this.theToolStrip);
            this.Controls.Add(this.mainMenuStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.Text = "SSH Tunnel Manager";
            this.mainMenuStrip.ResumeLayout(false);
            this.mainMenuStrip.PerformLayout();
            this.theToolStrip.ResumeLayout(false);
            this.theToolStrip.PerformLayout();
            this.splitContainerH1.Panel1.ResumeLayout(false);
            this.splitContainerH1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerH1)).EndInit();
            this.splitContainerH1.ResumeLayout(false);
            this.splitContainerV1.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerV1)).EndInit();
            this.splitContainerV1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.hostsGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip mainMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem changeStorageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem changePasswordToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem keepConnectionsExitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem hostToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addHostToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editHostToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem removeHostToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem startToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem stopToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.ToolStripMenuItem startPuTTYToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem startPsftpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem startFileZillaSFTPToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStrip theToolStrip;
        private System.Windows.Forms.ToolStripButton toolStripButtonSave;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripButton toolStripButtonAddHost;
        private System.Windows.Forms.ToolStripButton toolStripButtonRemoveHost;
        private System.Windows.Forms.ToolStripButton toolStripButtonEditHost;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton toolStripButtonStart;
        private System.Windows.Forms.ToolStripButton toolStripButtonStop;
        private System.Windows.Forms.StatusStrip theStatusStrip;
        private System.Windows.Forms.SplitContainer splitContainerH1;
        private System.Windows.Forms.TreeView treeViewFilter;
        private System.Windows.Forms.SplitContainer splitContainerV1;
        private System.Windows.Forms.DataGridView hostsGridView;
        private System.Windows.Forms.DataGridViewImageColumn hgwStatusIconColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn hgwNameColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn hgwUsernameColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn hgwHostnameColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn hgwStatusColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn hgwDependsOnColumn;
    }
}

