namespace STM.UI.Controls.ConnectionControl
{
    partial class ConnectionControl
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
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.Label nameLabel;
            System.Windows.Forms.Label userNameLabel;
            System.Windows.Forms.Label parentLabel;
            System.Windows.Forms.Label stateLabel;
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.Label addressLabel;
            this.connectionTabControl = new System.Windows.Forms.TabControl();
            this.generalTabPage = new System.Windows.Forms.TabPage();
            this.generalTableLayout = new System.Windows.Forms.TableLayoutPanel();
            this.nameValueLabel = new System.Windows.Forms.Label();
            this.userNameValueLabel = new System.Windows.Forms.Label();
            this.parentValueLabel = new System.Windows.Forms.Label();
            this.addressValueLabel = new System.Windows.Forms.Label();
            this.stateValueLabel = new System.Windows.Forms.Label();
            this.tunnelsGridView = new System.Windows.Forms.DataGridView();
            this.tunnelNameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tunnelTypeColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tunnelSrcPortColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tunnelDstHostColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tunnelDstPortColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.logTabPage = new System.Windows.Forms.TabPage();
            this.logListView = new System.Windows.Forms.ListView();
            this.logListViewColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            nameLabel = new System.Windows.Forms.Label();
            userNameLabel = new System.Windows.Forms.Label();
            parentLabel = new System.Windows.Forms.Label();
            stateLabel = new System.Windows.Forms.Label();
            addressLabel = new System.Windows.Forms.Label();
            this.connectionTabControl.SuspendLayout();
            this.generalTabPage.SuspendLayout();
            this.generalTableLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tunnelsGridView)).BeginInit();
            this.logTabPage.SuspendLayout();
            this.SuspendLayout();
            // 
            // connectionTabControl
            // 
            this.connectionTabControl.Controls.Add(this.generalTabPage);
            this.connectionTabControl.Controls.Add(this.logTabPage);
            this.connectionTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.connectionTabControl.Location = new System.Drawing.Point(0, 0);
            this.connectionTabControl.Name = "connectionTabControl";
            this.connectionTabControl.SelectedIndex = 0;
            this.connectionTabControl.Size = new System.Drawing.Size(785, 249);
            this.connectionTabControl.TabIndex = 2;
            // 
            // generalTabPage
            // 
            this.generalTabPage.Controls.Add(this.generalTableLayout);
            this.generalTabPage.Location = new System.Drawing.Point(4, 22);
            this.generalTabPage.Name = "generalTabPage";
            this.generalTabPage.Padding = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.generalTabPage.Size = new System.Drawing.Size(777, 223);
            this.generalTabPage.TabIndex = 0;
            this.generalTabPage.Text = "General";
            this.generalTabPage.UseVisualStyleBackColor = true;
            // 
            // generalTableLayout
            // 
            this.generalTableLayout.ColumnCount = 6;
            this.generalTableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.generalTableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.generalTableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.generalTableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.generalTableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.generalTableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.generalTableLayout.Controls.Add(nameLabel, 0, 0);
            this.generalTableLayout.Controls.Add(userNameLabel, 0, 1);
            this.generalTableLayout.Controls.Add(this.nameValueLabel, 1, 0);
            this.generalTableLayout.Controls.Add(this.userNameValueLabel, 1, 1);
            this.generalTableLayout.Controls.Add(parentLabel, 2, 0);
            this.generalTableLayout.Controls.Add(this.parentValueLabel, 3, 0);
            this.generalTableLayout.Controls.Add(this.addressValueLabel, 3, 1);
            this.generalTableLayout.Controls.Add(stateLabel, 4, 0);
            this.generalTableLayout.Controls.Add(this.stateValueLabel, 5, 0);
            this.generalTableLayout.Controls.Add(this.tunnelsGridView, 0, 2);
            this.generalTableLayout.Controls.Add(addressLabel, 2, 1);
            this.generalTableLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.generalTableLayout.Location = new System.Drawing.Point(3, 3);
            this.generalTableLayout.Name = "generalTableLayout";
            this.generalTableLayout.RowCount = 3;
            this.generalTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.generalTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.generalTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.generalTableLayout.Size = new System.Drawing.Size(771, 220);
            this.generalTableLayout.TabIndex = 0;
            // 
            // nameLabel
            // 
            nameLabel.AutoSize = true;
            nameLabel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            nameLabel.Location = new System.Drawing.Point(3, 3);
            nameLabel.Margin = new System.Windows.Forms.Padding(3);
            nameLabel.Name = "nameLabel";
            nameLabel.Size = new System.Drawing.Size(38, 13);
            nameLabel.TabIndex = 0;
            nameLabel.Text = "Name:";
            // 
            // userNameLabel
            // 
            userNameLabel.AutoSize = true;
            userNameLabel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            userNameLabel.Location = new System.Drawing.Point(3, 22);
            userNameLabel.Margin = new System.Windows.Forms.Padding(3);
            userNameLabel.Name = "userNameLabel";
            userNameLabel.Size = new System.Drawing.Size(58, 13);
            userNameLabel.TabIndex = 1;
            userNameLabel.Text = "Username:";
            // 
            // nameValueLabel
            // 
            this.nameValueLabel.AutoSize = true;
            this.nameValueLabel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.nameValueLabel.Location = new System.Drawing.Point(67, 3);
            this.nameValueLabel.Margin = new System.Windows.Forms.Padding(3);
            this.nameValueLabel.Name = "nameValueLabel";
            this.nameValueLabel.Size = new System.Drawing.Size(33, 13);
            this.nameValueLabel.TabIndex = 0;
            this.nameValueLabel.Text = "value";
            // 
            // userNameValueLabel
            // 
            this.userNameValueLabel.AutoSize = true;
            this.userNameValueLabel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.userNameValueLabel.Location = new System.Drawing.Point(67, 22);
            this.userNameValueLabel.Margin = new System.Windows.Forms.Padding(3);
            this.userNameValueLabel.Name = "userNameValueLabel";
            this.userNameValueLabel.Size = new System.Drawing.Size(33, 13);
            this.userNameValueLabel.TabIndex = 0;
            this.userNameValueLabel.Text = "value";
            // 
            // parentLabel
            // 
            parentLabel.AutoSize = true;
            parentLabel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            parentLabel.Location = new System.Drawing.Point(271, 3);
            parentLabel.Margin = new System.Windows.Forms.Padding(3);
            parentLabel.Name = "parentLabel";
            parentLabel.Size = new System.Drawing.Size(41, 13);
            parentLabel.TabIndex = 3;
            parentLabel.Text = "Parent:";
            // 
            // parentValueLabel
            // 
            this.parentValueLabel.AutoSize = true;
            this.parentValueLabel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.parentValueLabel.Location = new System.Drawing.Point(325, 3);
            this.parentValueLabel.Margin = new System.Windows.Forms.Padding(3);
            this.parentValueLabel.Name = "parentValueLabel";
            this.parentValueLabel.Size = new System.Drawing.Size(33, 13);
            this.parentValueLabel.TabIndex = 2;
            this.parentValueLabel.Text = "value";
            // 
            // addressValueLabel
            // 
            this.addressValueLabel.AutoSize = true;
            this.addressValueLabel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.addressValueLabel.Location = new System.Drawing.Point(325, 22);
            this.addressValueLabel.Margin = new System.Windows.Forms.Padding(3);
            this.addressValueLabel.Name = "addressValueLabel";
            this.addressValueLabel.Size = new System.Drawing.Size(33, 13);
            this.addressValueLabel.TabIndex = 2;
            this.addressValueLabel.Text = "value";
            // 
            // stateLabel
            // 
            stateLabel.AutoSize = true;
            stateLabel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            stateLabel.Location = new System.Drawing.Point(529, 3);
            stateLabel.Margin = new System.Windows.Forms.Padding(3);
            stateLabel.Name = "stateLabel";
            stateLabel.Size = new System.Drawing.Size(35, 13);
            stateLabel.TabIndex = 3;
            stateLabel.Text = "State:";
            // 
            // stateValueLabel
            // 
            this.stateValueLabel.AutoSize = true;
            this.stateValueLabel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.stateValueLabel.Location = new System.Drawing.Point(570, 3);
            this.stateValueLabel.Margin = new System.Windows.Forms.Padding(3);
            this.stateValueLabel.Name = "stateValueLabel";
            this.stateValueLabel.Size = new System.Drawing.Size(33, 13);
            this.stateValueLabel.TabIndex = 2;
            this.stateValueLabel.Text = "value";
            // 
            // tunnelsGridView
            // 
            this.tunnelsGridView.AllowUserToAddRows = false;
            this.tunnelsGridView.AllowUserToDeleteRows = false;
            this.tunnelsGridView.AllowUserToResizeRows = false;
            this.tunnelsGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.tunnelsGridView.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.tunnelsGridView.BackgroundColor = System.Drawing.SystemColors.Window;
            this.tunnelsGridView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tunnelsGridView.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.WhiteSmoke;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Gray;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.tunnelsGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.tunnelsGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.tunnelsGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.tunnelNameColumn,
            this.tunnelTypeColumn,
            this.tunnelSrcPortColumn,
            this.tunnelDstHostColumn,
            this.tunnelDstPortColumn});
            this.generalTableLayout.SetColumnSpan(this.tunnelsGridView, 6);
            this.tunnelsGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tunnelsGridView.EnableHeadersVisualStyles = false;
            this.tunnelsGridView.Location = new System.Drawing.Point(3, 41);
            this.tunnelsGridView.Margin = new System.Windows.Forms.Padding(3, 3, 0, 0);
            this.tunnelsGridView.MultiSelect = false;
            this.tunnelsGridView.Name = "tunnelsGridView";
            this.tunnelsGridView.ReadOnly = true;
            this.tunnelsGridView.RowHeadersVisible = false;
            this.tunnelsGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.tunnelsGridView.Size = new System.Drawing.Size(768, 179);
            this.tunnelsGridView.TabIndex = 7;
            this.tunnelsGridView.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.tunnelsGridView_CellFormatting);
            this.tunnelsGridView.SelectionChanged += new System.EventHandler(this.tunnelsGridView_SelectionChanged);
            // 
            // tunnelNameColumn
            // 
            this.tunnelNameColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.tunnelNameColumn.HeaderText = "Tunnel";
            this.tunnelNameColumn.Name = "tunnelNameColumn";
            this.tunnelNameColumn.ReadOnly = true;
            this.tunnelNameColumn.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // tunnelTypeColumn
            // 
            this.tunnelTypeColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.tunnelTypeColumn.HeaderText = "Type";
            this.tunnelTypeColumn.Name = "tunnelTypeColumn";
            this.tunnelTypeColumn.ReadOnly = true;
            this.tunnelTypeColumn.Width = 56;
            // 
            // tunnelSrcPortColumn
            // 
            this.tunnelSrcPortColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.tunnelSrcPortColumn.HeaderText = "Src Port";
            this.tunnelSrcPortColumn.Name = "tunnelSrcPortColumn";
            this.tunnelSrcPortColumn.ReadOnly = true;
            this.tunnelSrcPortColumn.Width = 70;
            // 
            // tunnelDstHostColumn
            // 
            this.tunnelDstHostColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.tunnelDstHostColumn.HeaderText = "Dest Host";
            this.tunnelDstHostColumn.Name = "tunnelDstHostColumn";
            this.tunnelDstHostColumn.ReadOnly = true;
            this.tunnelDstHostColumn.Width = 79;
            // 
            // tunnelDstPortColumn
            // 
            this.tunnelDstPortColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.tunnelDstPortColumn.HeaderText = "Dest Port";
            this.tunnelDstPortColumn.Name = "tunnelDstPortColumn";
            this.tunnelDstPortColumn.ReadOnly = true;
            this.tunnelDstPortColumn.Width = 76;
            // 
            // addressLabel
            // 
            addressLabel.AutoSize = true;
            addressLabel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            addressLabel.Location = new System.Drawing.Point(271, 22);
            addressLabel.Margin = new System.Windows.Forms.Padding(3);
            addressLabel.Name = "addressLabel";
            addressLabel.Size = new System.Drawing.Size(48, 13);
            addressLabel.TabIndex = 3;
            addressLabel.Text = "Address:";
            // 
            // logTabPage
            // 
            this.logTabPage.Controls.Add(this.logListView);
            this.logTabPage.Location = new System.Drawing.Point(4, 22);
            this.logTabPage.Name = "logTabPage";
            this.logTabPage.Size = new System.Drawing.Size(777, 223);
            this.logTabPage.TabIndex = 1;
            this.logTabPage.Text = "Log";
            this.logTabPage.UseVisualStyleBackColor = true;
            // 
            // logListView
            // 
            this.logListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.logListViewColumnHeader});
            this.logListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.logListView.FullRowSelect = true;
            this.logListView.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.logListView.Location = new System.Drawing.Point(0, 0);
            this.logListView.MultiSelect = false;
            this.logListView.Name = "logListView";
            this.logListView.OwnerDraw = true;
            this.logListView.ShowGroups = false;
            this.logListView.Size = new System.Drawing.Size(777, 223);
            this.logListView.TabIndex = 2;
            this.logListView.UseCompatibleStateImageBehavior = false;
            this.logListView.View = System.Windows.Forms.View.Details;
            this.logListView.DrawSubItem += new System.Windows.Forms.DrawListViewSubItemEventHandler(this.logListView_DrawSubItem);
            // 
            // logListViewColumnHeader
            // 
            this.logListViewColumnHeader.Text = "Message";
            // 
            // ConnectionControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.connectionTabControl);
            this.Name = "ConnectionControl";
            this.Size = new System.Drawing.Size(785, 249);
            this.connectionTabControl.ResumeLayout(false);
            this.generalTabPage.ResumeLayout(false);
            this.generalTableLayout.ResumeLayout(false);
            this.generalTableLayout.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tunnelsGridView)).EndInit();
            this.logTabPage.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl connectionTabControl;
        private System.Windows.Forms.TabPage generalTabPage;
        private System.Windows.Forms.TableLayoutPanel generalTableLayout;
        private System.Windows.Forms.Label nameValueLabel;
        private System.Windows.Forms.Label userNameValueLabel;
        private System.Windows.Forms.Label parentValueLabel;
        private System.Windows.Forms.Label addressValueLabel;
        private System.Windows.Forms.Label stateValueLabel;
        private System.Windows.Forms.DataGridView tunnelsGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn tunnelNameColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn tunnelTypeColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn tunnelSrcPortColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn tunnelDstHostColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn tunnelDstPortColumn;
        private System.Windows.Forms.TabPage logTabPage;
        private System.Windows.Forms.ListView logListView;
        private System.Windows.Forms.ColumnHeader logListViewColumnHeader;
    }
}
