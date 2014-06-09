namespace STM.UI.Forms.Main
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
            System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
            System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
            System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
            System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
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
            this.myMainMenu = new System.Windows.Forms.MenuStrip();
            this.fileMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.saveMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.changeStorageMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.changePasswordMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.settingsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.connectionMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.addConnectionMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editConnectionMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.removeConnectionMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.startMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stopMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.startPuttyHereMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.startPsftpHereMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.startFileZillaHereMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutMenuItem = new System.Windows.Forms.ToolStripMenuItem();
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
            toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.myMainMenu.SuspendLayout();
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
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new System.Drawing.Size(174, 6);
            // 
            // toolStripSeparator4
            // 
            toolStripSeparator4.Name = "toolStripSeparator4";
            toolStripSeparator4.Size = new System.Drawing.Size(174, 6);
            // 
            // toolStripSeparator2
            // 
            toolStripSeparator2.Name = "toolStripSeparator2";
            toolStripSeparator2.Size = new System.Drawing.Size(201, 6);
            // 
            // toolStripSeparator7
            // 
            toolStripSeparator7.Name = "toolStripSeparator7";
            toolStripSeparator7.Size = new System.Drawing.Size(201, 6);
            // 
            // toolStripSeparator5
            // 
            toolStripSeparator5.Name = "toolStripSeparator5";
            toolStripSeparator5.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripSeparator3
            // 
            toolStripSeparator3.Name = "toolStripSeparator3";
            toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // myMainMenu
            // 
            this.myMainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileMenu,
            this.connectionMenu,
            this.helpMenu});
            this.myMainMenu.Location = new System.Drawing.Point(0, 0);
            this.myMainMenu.Name = "myMainMenu";
            this.myMainMenu.Size = new System.Drawing.Size(826, 24);
            this.myMainMenu.TabIndex = 2;
            this.myMainMenu.Text = "menuStrip1";
            // 
            // fileMenu
            // 
            this.fileMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveMenuItem,
            toolStripSeparator1,
            this.changeStorageMenuItem,
            this.changePasswordMenuItem,
            this.toolStripSeparator6,
            this.settingsMenuItem,
            toolStripSeparator4,
            this.exitMenuItem});
            this.fileMenu.Name = "fileMenu";
            this.fileMenu.Size = new System.Drawing.Size(37, 20);
            this.fileMenu.Text = "&File";
            // 
            // saveMenuItem
            // 
            this.saveMenuItem.Image = global::STM.UI.Properties.Resources.disk;
            this.saveMenuItem.Name = "saveMenuItem";
            this.saveMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.saveMenuItem.Size = new System.Drawing.Size(177, 22);
            this.saveMenuItem.Text = "Sa&ve";
            this.saveMenuItem.Click += new System.EventHandler(this.saveMenuItem_Click);
            // 
            // changeStorageMenuItem
            // 
            this.changeStorageMenuItem.Image = global::STM.UI.Properties.Resources.databases__arrow;
            this.changeStorageMenuItem.Name = "changeStorageMenuItem";
            this.changeStorageMenuItem.Size = new System.Drawing.Size(177, 22);
            this.changeStorageMenuItem.Text = "&Change Storage...";
            this.changeStorageMenuItem.Click += new System.EventHandler(this.changeStorageMenuItem_Click);
            // 
            // changePasswordMenuItem
            // 
            this.changePasswordMenuItem.Image = global::STM.UI.Properties.Resources.key__arrow;
            this.changePasswordMenuItem.Name = "changePasswordMenuItem";
            this.changePasswordMenuItem.Size = new System.Drawing.Size(177, 22);
            this.changePasswordMenuItem.Text = "C&hange Password...";
            this.changePasswordMenuItem.Click += new System.EventHandler(this.changePasswordMenuItem_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(174, 6);
            // 
            // settingsMenuItem
            // 
            this.settingsMenuItem.Image = global::STM.UI.Properties.Resources.Gear;
            this.settingsMenuItem.Name = "settingsMenuItem";
            this.settingsMenuItem.Size = new System.Drawing.Size(177, 22);
            this.settingsMenuItem.Text = "Settings...";
            this.settingsMenuItem.Click += new System.EventHandler(this.settingsMenuItem_Click);
            // 
            // exitMenuItem
            // 
            this.exitMenuItem.Name = "exitMenuItem";
            this.exitMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
            this.exitMenuItem.Size = new System.Drawing.Size(177, 22);
            this.exitMenuItem.Text = "Ex&it";
            this.exitMenuItem.Click += new System.EventHandler(this.exitMenuItem_Click);
            // 
            // connectionMenu
            // 
            this.connectionMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addConnectionMenuItem,
            this.editConnectionMenuItem,
            this.removeConnectionMenuItem,
            toolStripSeparator2,
            this.startMenuItem,
            this.stopMenuItem,
            toolStripSeparator7,
            this.startPuttyHereMenuItem,
            this.startPsftpHereMenuItem,
            this.startFileZillaHereMenuItem});
            this.connectionMenu.Name = "connectionMenu";
            this.connectionMenu.Size = new System.Drawing.Size(81, 20);
            this.connectionMenu.Text = "&Connection";
            // 
            // addConnectionMenuItem
            // 
            this.addConnectionMenuItem.Image = global::STM.UI.Properties.Resources.server__plus;
            this.addConnectionMenuItem.Name = "addConnectionMenuItem";
            this.addConnectionMenuItem.Size = new System.Drawing.Size(204, 22);
            this.addConnectionMenuItem.Text = "&Add connection...";
            this.addConnectionMenuItem.Click += new System.EventHandler(this.addConnectionMenuItem_Click);
            // 
            // editConnectionMenuItem
            // 
            this.editConnectionMenuItem.Image = global::STM.UI.Properties.Resources.server__pencil;
            this.editConnectionMenuItem.Name = "editConnectionMenuItem";
            this.editConnectionMenuItem.Size = new System.Drawing.Size(204, 22);
            this.editConnectionMenuItem.Text = "&Edit connection...";
            this.editConnectionMenuItem.Click += new System.EventHandler(this.editConnectionMenuItem_Click);
            // 
            // removeConnectionMenuItem
            // 
            this.removeConnectionMenuItem.Image = global::STM.UI.Properties.Resources.server__minus;
            this.removeConnectionMenuItem.Name = "removeConnectionMenuItem";
            this.removeConnectionMenuItem.ShortcutKeys = System.Windows.Forms.Keys.Delete;
            this.removeConnectionMenuItem.Size = new System.Drawing.Size(204, 22);
            this.removeConnectionMenuItem.Text = "&Remove connection";
            this.removeConnectionMenuItem.Click += new System.EventHandler(this.removeConnectionMenuItem_Click);
            // 
            // startMenuItem
            // 
            this.startMenuItem.Image = global::STM.UI.Properties.Resources.control;
            this.startMenuItem.Name = "startMenuItem";
            this.startMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F5;
            this.startMenuItem.Size = new System.Drawing.Size(204, 22);
            this.startMenuItem.Text = "&Start";
            this.startMenuItem.Click += new System.EventHandler(this.startMenuItem_Click);
            // 
            // stopMenuItem
            // 
            this.stopMenuItem.Image = global::STM.UI.Properties.Resources.control_stop_square;
            this.stopMenuItem.Name = "stopMenuItem";
            this.stopMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.F5)));
            this.stopMenuItem.Size = new System.Drawing.Size(204, 22);
            this.stopMenuItem.Text = "S&top";
            this.stopMenuItem.Click += new System.EventHandler(this.stopMenuItem_Click);
            // 
            // startPuttyHereMenuItem
            // 
            this.startPuttyHereMenuItem.Image = global::STM.UI.Properties.Resources.icon_16x16_putty;
            this.startPuttyHereMenuItem.Name = "startPuttyHereMenuItem";
            this.startPuttyHereMenuItem.Size = new System.Drawing.Size(204, 22);
            this.startPuttyHereMenuItem.Text = "Start &PuTTY here";
            this.startPuttyHereMenuItem.Click += new System.EventHandler(this.startPuttyHereMenuItem_Click);
            // 
            // startPsftpHereMenuItem
            // 
            this.startPsftpHereMenuItem.Image = global::STM.UI.Properties.Resources.psftp;
            this.startPsftpHereMenuItem.Name = "startPsftpHereMenuItem";
            this.startPsftpHereMenuItem.Size = new System.Drawing.Size(204, 22);
            this.startPsftpHereMenuItem.Text = "Start ps&ftp here";
            this.startPsftpHereMenuItem.Click += new System.EventHandler(this.startPsftpHereMenuItem_Click);
            // 
            // startFileZillaHereMenuItem
            // 
            this.startFileZillaHereMenuItem.Image = global::STM.UI.Properties.Resources.filezilla;
            this.startFileZillaHereMenuItem.Name = "startFileZillaHereMenuItem";
            this.startFileZillaHereMenuItem.Size = new System.Drawing.Size(204, 22);
            this.startFileZillaHereMenuItem.Text = "Start File&Zilla here";
            this.startFileZillaHereMenuItem.Click += new System.EventHandler(this.startFileZillaHereMenuItem_Click);
            // 
            // helpMenu
            // 
            this.helpMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutMenuItem});
            this.helpMenu.Name = "helpMenu";
            this.helpMenu.Size = new System.Drawing.Size(44, 20);
            this.helpMenu.Text = "H&elp";
            // 
            // aboutMenuItem
            // 
            this.aboutMenuItem.Name = "aboutMenuItem";
            this.aboutMenuItem.Size = new System.Drawing.Size(221, 22);
            this.aboutMenuItem.Text = "&About SSH Tunnel Manager";
            this.aboutMenuItem.Click += new System.EventHandler(this.aboutMenuItem_Click);
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
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // addConnectionButton
            // 
            this.addConnectionButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.addConnectionButton.Image = global::STM.UI.Properties.Resources.server__plus;
            this.addConnectionButton.Name = "addConnectionButton";
            this.addConnectionButton.Size = new System.Drawing.Size(23, 22);
            this.addConnectionButton.Text = "New host...";
            this.addConnectionButton.Click += new System.EventHandler(this.addConnectionButton_Click);
            // 
            // removeConnectionButton
            // 
            this.removeConnectionButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.removeConnectionButton.Image = global::STM.UI.Properties.Resources.server__minus;
            this.removeConnectionButton.Name = "removeConnectionButton";
            this.removeConnectionButton.Size = new System.Drawing.Size(23, 22);
            this.removeConnectionButton.Text = "Remove Host";
            this.removeConnectionButton.Click += new System.EventHandler(this.removeConnectionButton_Click);
            // 
            // editConnectionButton
            // 
            this.editConnectionButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.editConnectionButton.Image = global::STM.UI.Properties.Resources.server__pencil;
            this.editConnectionButton.Name = "editConnectionButton";
            this.editConnectionButton.Size = new System.Drawing.Size(23, 22);
            this.editConnectionButton.Text = "Edit Host...";
            this.editConnectionButton.Click += new System.EventHandler(this.editConnectionButton_Click);
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
            this.stopButton.Click += new System.EventHandler(this.stopButton_Click);
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
            this.filterTreeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.filterTreeView_AfterSelect);
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
            this.connectionsGridView.RowContextMenuStripNeeded += new System.Windows.Forms.DataGridViewRowContextMenuStripNeededEventHandler(this.connectionsGridView_RowContextMenuStripNeeded);
            this.connectionsGridView.SelectionChanged += new System.EventHandler(this.connectionsGridView_SelectionChanged);
            this.connectionsGridView.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.connectionsGridView_MouseDoubleClick);
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
            this.Controls.Add(this.myMainMenu);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.Text = "SSH Tunnel Manager";
            this.myMainMenu.ResumeLayout(false);
            this.myMainMenu.PerformLayout();
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

        private System.Windows.Forms.MenuStrip myMainMenu;
        private System.Windows.Forms.ToolStripMenuItem fileMenu;
        private System.Windows.Forms.ToolStripMenuItem saveMenuItem;
        private System.Windows.Forms.ToolStripMenuItem changeStorageMenuItem;
        private System.Windows.Forms.ToolStripMenuItem changePasswordMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitMenuItem;
        private System.Windows.Forms.ToolStripMenuItem connectionMenu;
        private System.Windows.Forms.ToolStripMenuItem addConnectionMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editConnectionMenuItem;
        private System.Windows.Forms.ToolStripMenuItem removeConnectionMenuItem;
        private System.Windows.Forms.ToolStripMenuItem startMenuItem;
        private System.Windows.Forms.ToolStripMenuItem stopMenuItem;
        private System.Windows.Forms.ToolStripMenuItem startPuttyHereMenuItem;
        private System.Windows.Forms.ToolStripMenuItem startPsftpHereMenuItem;
        private System.Windows.Forms.ToolStripMenuItem startFileZillaHereMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpMenu;
        private System.Windows.Forms.ToolStripMenuItem aboutMenuItem;
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
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripMenuItem settingsMenuItem;
    }
}

