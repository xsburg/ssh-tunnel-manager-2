// ***********************************************************************
// <author>Stephan Burguchev</author>
// <copyright company="Stephan Burguchev">
//   Copyright (c) Stephan Burguchev 2012-2013. All rights reserved.
// </copyright>
// <summary>
//   MainForm.cs
// </summary>
// ***********************************************************************

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using STM.Core;
using STM.Core.Data;
using STM.UI.Common;
using STM.UI.Controls.ConnectionControl;

namespace STM.UI.Forms.MainForm
{
    public partial class MainForm : Form, IMainForm
    {
        private NotifyIconManager notifyIconManager;

        public MainForm(MainFormController controller, ConnectionControl connectionControl)
        {
            if (controller == null)
            {
                throw new ArgumentNullException("controller");
            }

            this.InitializeComponent();

            this.Controller = controller;
            this.Controller.Register(this);
            this.Controller.Register(connectionControl.Controller);

            // TODO: View initialization code here
            this.InitializeNotifyIcon();

            connectionControl.Dock = DockStyle.Fill;
            this.myVerticalSplitContainer.Panel2.Controls.Add(connectionControl);

            this.connectionsGridView.AutoGenerateColumns = false;
        }

        public MainFormController Controller { get; private set; }

        public void Render(IList<ConnectionViewModel> connections)
        {
            this.SuspendLayout();

            this.connectionsGridView.DataSource = connections;

            this.ResumeLayout(true);
        }

        private void InitializeNotifyIcon()
        {
            this.notifyIconManager = new NotifyIconManager(this);
            this.notifyIconManager.NotifyIcon.Icon = this.Icon;
            this.notifyIconManager.NotifyIcon.Text = this.Text;
            this.TextChanged += (s, a) => this.notifyIconManager.NotifyIcon.Text = this.Text;

            var showHideMenuItem = new ToolStripMenuItem("Show/Hide");
            showHideMenuItem.Font = new Font(showHideMenuItem.Font, FontStyle.Bold);
            showHideMenuItem.Click += (s, a) => this.notifyIconManager.SwitchFormState();

            this.notifyIconManager.NotifyIcon.ContextMenuStrip.Items.Add(showHideMenuItem);
            this.notifyIconManager.NotifyIcon.ContextMenuStrip.Items.Add("-");
            this.notifyIconManager.NotifyIcon.ContextMenuStrip.Items.Add(
                "Exit",
                null,
                (s, a) => this.notifyIconManager.CloseForm());
        }

        private void connectionsGridView_SelectionChanged(object sender, EventArgs e)
        {
            var viewModel = this.GetSelectedConnection();
            if (viewModel == null)
            {
                return;
            }

            this.Controller.SelectConnection(viewModel);
        }

        private ConnectionViewModel GetSelectedConnection()
        {
            var row = this.connectionsGridView.SelectedRows.Cast<DataGridViewRow>().FirstOrDefault();
            if (row == null)
            {
                return null;
            }

            return row.DataBoundItem as ConnectionViewModel;
        }

        private void connectionsGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
            {
                return;
            }

            if (connectionsGridView.Columns[e.ColumnIndex].Name != connectionStateColumn.Name)
            {
                return;
            }

            var viewModel = connectionsGridView.Rows[e.RowIndex].DataBoundItem as ConnectionViewModel;
            if (viewModel == null)
            {
                return;
            }

            e.CellStyle.ForeColor = viewModel.StateColor;
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            var viewModel = this.GetSelectedConnection();
            if (viewModel == null)
            {
                return;
            }

            this.Controller.OpenConnection(viewModel);
        }
    }
}
