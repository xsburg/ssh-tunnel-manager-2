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
            System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
            System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
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
            this.myToolStrip = new System.Windows.Forms.ToolStrip();
            this.saveButton = new System.Windows.Forms.ToolStripButton();
            this.addConnectionButton = new System.Windows.Forms.ToolStripButton();
            this.removeConnectionButton = new System.Windows.Forms.ToolStripButton();
            this.editConnectionButton = new System.Windows.Forms.ToolStripButton();
            this.startButton = new System.Windows.Forms.ToolStripButton();
            this.stopButton = new System.Windows.Forms.ToolStripButton();
            this.myStatusStrip = new System.Windows.Forms.StatusStrip();
            this.myHorizontalSplitContainer = new System.Windows.Forms.SplitContainer();
            this.filterTreeView = new System.Windows.Forms.TreeView();
            this.myVerticalSplitContainer = new System.Windows.Forms.SplitContainer();
            this.connectionsGridView = new System.Windows.Forms.DataGridView();
            this.connectionStateIconColumn = new System.Windows.Forms.DataGridViewImageColumn();
            this.connectionNameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.connectionUserNameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.connectionAddressColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.connectionStateColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.connectionParentColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.mainMenuStrip.SuspendLayout();
            this.myToolStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.myHorizontalSplitContainer)).BeginInit();
            this.myHorizontalSplitContainer.Panel1.SuspendLayout();
            this.myHorizontalSplitContainer.Panel2.SuspendLayout();
            this.myHorizontalSplitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.myVerticalSplitContainer)).BeginInit();
            this.myVerticalSplitContainer.Panel1.SuspendLayout();
            this.myVerticalSplitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.connectionsGridView)).BeginInit();
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
            // myToolStrip
            // 
            this.myToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveButton,
            toolStripSeparator5,
            this.addConnectionButton,
            this.removeConnectionButton,
            this.editConnectionButton,
            toolStripSeparator3,
            this.startButton,
            this.stopButton});
            this.myToolStrip.Location = new System.Drawing.Point(0, 24);
            this.myToolStrip.Name = "myToolStrip";
            this.myToolStrip.Size = new System.Drawing.Size(826, 25);
            this.myToolStrip.TabIndex = 3;
            this.myToolStrip.Text = "toolStrip1";
            // 
            // saveButton
            // 
            this.saveButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.saveButton.Image = global::STM.UI.Properties.Resources.disk;
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(23, 22);
            this.saveButton.Text = "Save";
            // 
            // toolStripSeparator5
            // 
            toolStripSeparator5.Name = "toolStripSeparator5";
            toolStripSeparator5.Size = new System.Drawing.Size(6, 25);
            // 
            // addConnectionButton
            // 
            this.addConnectionButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.addConnectionButton.Image = global::STM.UI.Properties.Resources.server__plus;
            this.addConnectionButton.Name = "addConnectionButton";
            this.addConnectionButton.Size = new System.Drawing.Size(23, 22);
            this.addConnectionButton.Text = "New host...";
            // 
            // removeConnectionButton
            // 
            this.removeConnectionButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.removeConnectionButton.Image = global::STM.UI.Properties.Resources.server__minus;
            this.removeConnectionButton.Name = "removeConnectionButton";
            this.removeConnectionButton.Size = new System.Drawing.Size(23, 22);
            this.removeConnectionButton.Text = "Remove Host";
            // 
            // editConnectionButton
            // 
            this.editConnectionButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.editConnectionButton.Image = global::STM.UI.Properties.Resources.server__pencil;
            this.editConnectionButton.Name = "editConnectionButton";
            this.editConnectionButton.Size = new System.Drawing.Size(23, 22);
            this.editConnectionButton.Text = "Edit Host...";
            // 
            // toolStripSeparator3
            // 
            toolStripSeparator3.Name = "toolStripSeparator3";
            toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // startButton
            // 
            this.startButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.startButton.Image = global::STM.UI.Properties.Resources.control;
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(23, 22);
            this.startButton.Text = "Start";
            this.startButton.Click += new System.EventHandler(this.startButton_Click);
            // 
            // stopButton
            // 
            this.stopButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.stopButton.Image = global::STM.UI.Properties.Resources.control_stop_square;
            this.stopButton.Name = "stopButton";
            this.stopButton.Size = new System.Drawing.Size(23, 22);
            this.stopButton.Text = "Stop";
            // 
            // myStatusStrip
            // 
            this.myStatusStrip.Location = new System.Drawing.Point(0, 535);
            this.myStatusStrip.Name = "myStatusStrip";
            this.myStatusStrip.Size = new System.Drawing.Size(826, 22);
            this.myStatusStrip.TabIndex = 4;
            this.myStatusStrip.Text = "statusStrip1";
            // 
            // myHorizontalSplitContainer
            // 
            this.myHorizontalSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.myHorizontalSplitContainer.Location = new System.Drawing.Point(0, 49);
            this.myHorizontalSplitContainer.Name = "myHorizontalSplitContainer";
            // 
            // myHorizontalSplitContainer.Panel1
            // 
            this.myHorizontalSplitContainer.Panel1.Controls.Add(this.filterTreeView);
            // 
            // myHorizontalSplitContainer.Panel2
            // 
            this.myHorizontalSplitContainer.Panel2.Controls.Add(this.myVerticalSplitContainer);
            this.myHorizontalSplitContainer.Size = new System.Drawing.Size(826, 486);
            this.myHorizontalSplitContainer.SplitterDistance = 143;
            this.myHorizontalSplitContainer.TabIndex = 5;
            // 
            // filterTreeView
            // 
            this.filterTreeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.filterTreeView.Location = new System.Drawing.Point(0, 0);
            this.filterTreeView.Name = "filterTreeView";
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
            this.filterTreeView.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode5});
            this.filterTreeView.Size = new System.Drawing.Size(143, 486);
            this.filterTreeView.TabIndex = 0;
            // 
            // myVerticalSplitContainer
            // 
            this.myVerticalSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.myVerticalSplitContainer.Location = new System.Drawing.Point(0, 0);
            this.myVerticalSplitContainer.Name = "myVerticalSplitContainer";
            this.myVerticalSplitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // myVerticalSplitContainer.Panel1
            // 
            this.myVerticalSplitContainer.Panel1.Controls.Add(this.connectionsGridView);
            this.myVerticalSplitContainer.Size = new System.Drawing.Size(679, 486);
            this.myVerticalSplitContainer.SplitterDistance = 260;
            this.myVerticalSplitContainer.TabIndex = 1;
            // 
            // connectionsGridView
            // 
            this.connectionsGridView.AllowUserToAddRows = false;
            this.connectionsGridView.AllowUserToDeleteRows = false;
            this.connectionsGridView.AllowUserToResizeRows = false;
            this.connectionsGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.connectionsGridView.BackgroundColor = System.Drawing.SystemColors.Window;
            this.connectionsGridView.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.connectionsGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.connectionsGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.connectionStateIconColumn,
            this.connectionNameColumn,
            this.connectionUserNameColumn,
            this.connectionAddressColumn,
            this.connectionStateColumn,
            this.connectionParentColumn});
            this.connectionsGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.connectionsGridView.Location = new System.Drawing.Point(0, 0);
            this.connectionsGridView.MultiSelect = false;
            this.connectionsGridView.Name = "connectionsGridView";
            this.connectionsGridView.RowHeadersVisible = false;
            this.connectionsGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.connectionsGridView.Size = new System.Drawing.Size(679, 260);
            this.connectionsGridView.TabIndex = 0;
            this.connectionsGridView.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.connectionsGridView_CellFormatting);
            this.connectionsGridView.SelectionChanged += new System.EventHandler(this.connectionsGridView_SelectionChanged);
            // 
            // connectionStateIconColumn
            // 
            this.connectionStateIconColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.connectionStateIconColumn.DataPropertyName = "StateIcon";
            this.connectionStateIconColumn.FillWeight = 27.62246F;
            this.connectionStateIconColumn.HeaderText = "*";
            this.connectionStateIconColumn.MinimumWidth = 20;
            this.connectionStateIconColumn.Name = "connectionStateIconColumn";
            this.connectionStateIconColumn.ReadOnly = true;
            this.connectionStateIconColumn.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.connectionStateIconColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.connectionStateIconColumn.Width = 25;
            // 
            // connectionNameColumn
            // 
            this.connectionNameColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.connectionNameColumn.DataPropertyName = "Name";
            this.connectionNameColumn.FillWeight = 20F;
            this.connectionNameColumn.HeaderText = "Name";
            this.connectionNameColumn.MinimumWidth = 20;
            this.connectionNameColumn.Name = "connectionNameColumn";
            this.connectionNameColumn.ReadOnly = true;
            this.connectionNameColumn.Width = 60;
            // 
            // connectionUserNameColumn
            // 
            this.connectionUserNameColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.connectionUserNameColumn.DataPropertyName = "UserName";
            this.connectionUserNameColumn.HeaderText = "Username";
            this.connectionUserNameColumn.Name = "connectionUserNameColumn";
            this.connectionUserNameColumn.ReadOnly = true;
            this.connectionUserNameColumn.Width = 80;
            // 
            // connectionAddressColumn
            // 
            this.connectionAddressColumn.DataPropertyName = "Address";
            this.connectionAddressColumn.FillWeight = 95.02126F;
            this.connectionAddressColumn.HeaderText = "Address";
            this.connectionAddressColumn.Name = "connectionAddressColumn";
            this.connectionAddressColumn.ReadOnly = true;
            // 
            // connectionStateColumn
            // 
            this.connectionStateColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.connectionStateColumn.DataPropertyName = "State";
            this.connectionStateColumn.FillWeight = 95.02126F;
            this.connectionStateColumn.HeaderText = "State";
            this.connectionStateColumn.Name = "connectionStateColumn";
            this.connectionStateColumn.ReadOnly = true;
            this.connectionStateColumn.Width = 57;
            // 
            // connectionParentColumn
            // 
            this.connectionParentColumn.DataPropertyName = "Parent";
            this.connectionParentColumn.HeaderText = "Parent";
            this.connectionParentColumn.Name = "connectionParentColumn";
            this.connectionParentColumn.ReadOnly = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(826, 557);
            this.Controls.Add(this.myHorizontalSplitContainer);
            this.Controls.Add(this.myStatusStrip);
            this.Controls.Add(this.myToolStrip);
            this.Controls.Add(this.mainMenuStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.Text = "SSH Tunnel Manager";
            this.mainMenuStrip.ResumeLayout(false);
            this.mainMenuStrip.PerformLayout();
            this.myToolStrip.ResumeLayout(false);
            this.myToolStrip.PerformLayout();
            this.myHorizontalSplitContainer.Panel1.ResumeLayout(false);
            this.myHorizontalSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.myHorizontalSplitContainer)).EndInit();
            this.myHorizontalSplitContainer.ResumeLayout(false);
            this.myVerticalSplitContainer.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.myVerticalSplitContainer)).EndInit();
            this.myVerticalSplitContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.connectionsGridView)).EndInit();
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
        private System.Windows.Forms.ToolStrip myToolStrip;
        private System.Windows.Forms.ToolStripButton saveButton;
        private System.Windows.Forms.ToolStripButton addConnectionButton;
        private System.Windows.Forms.ToolStripButton removeConnectionButton;
        private System.Windows.Forms.ToolStripButton editConnectionButton;
        private System.Windows.Forms.ToolStripButton startButton;
        private System.Windows.Forms.ToolStripButton stopButton;
        private System.Windows.Forms.StatusStrip myStatusStrip;
        private System.Windows.Forms.SplitContainer myHorizontalSplitContainer;
        private System.Windows.Forms.TreeView filterTreeView;
        private System.Windows.Forms.SplitContainer myVerticalSplitContainer;
        private System.Windows.Forms.DataGridView connectionsGridView;
        private System.Windows.Forms.DataGridViewImageColumn connectionStateIconColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn connectionNameColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn connectionUserNameColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn connectionAddressColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn connectionStateColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn connectionParentColumn;
    }
}

