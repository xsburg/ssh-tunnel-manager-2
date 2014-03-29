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
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using STM.UI.Common;
using STM.UI.Controls.ConnectionControl;

namespace STM.UI.Forms.Main
{
    // ReSharper disable InconsistentNaming
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

        public void Close(bool dontMinimize)
        {
            if (dontMinimize)
            {
                this.notifyIconManager.CloseForm();
            }
            else
            {
                this.Close();
            }
        }

        public void Select(ConnectionViewModel connection)
        {
        }

        public void Render(IList<ConnectionViewModel> connections)
        {
            this.SuspendLayout();

            this.connectionsGridView.DataSource = connections;

            this.ResumeLayout(true);
        }

        public void UpdateActionState(MainFormActionsViewModel viewModel)
        {
            this.saveMenuItem.Enabled = this.saveButton.Enabled = viewModel.CanSave;
            this.editConnectionButton.Enabled = this.editConnectionMenuItem.Enabled = viewModel.CanEditConnectionInfo;
            this.startMenuItem.Enabled = this.startButton.Enabled = viewModel.CanOpenConnection;
            this.stopButton.Enabled = this.stopMenuItem.Enabled = viewModel.CanCloseConnection;
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

        private void InitializeNotifyIcon()
        {
            this.notifyIconManager = new NotifyIconManager(this)
                {
                    NotifyIcon =
                        {
                            Icon = this.Icon,
                            Text = this.Text
                        }
                };

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

        private void aboutMenuItem_Click(object sender, EventArgs e)
        {
            this.Controller.DisplayAboutDialog();
        }

        private void addConnectionButton_Click(object sender, EventArgs e)
        {
            this.Controller.DisplayNewConnectionDialog();
        }

        private void addConnectionMenuItem_Click(object sender, EventArgs e)
        {
            this.Controller.DisplayNewConnectionDialog();
        }

        private void changePasswordMenuItem_Click(object sender, EventArgs e)
        {
            this.Controller.DisplayChangePasswordDialog();
        }

        private void changeStorageMenuItem_Click(object sender, EventArgs e)
        {
            this.Controller.DisplayChangeStorageDialog();
        }

        private void connectionsGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
            {
                return;
            }

            if (this.connectionsGridView.Columns[e.ColumnIndex].Name != this.connectionStateColumn.Name)
            {
                return;
            }

            var viewModel = this.connectionsGridView.Rows[e.RowIndex].DataBoundItem as ConnectionViewModel;
            if (viewModel == null)
            {
                return;
            }

            e.CellStyle.ForeColor = viewModel.StateColor;
        }

        private void connectionsGridView_SelectionChanged(object sender, EventArgs e)
        {
            var viewModel = this.GetSelectedConnection();
            if (viewModel == null)
            {
                return;
            }

            this.Controller.Select(viewModel);
        }

        private void editConnectionButton_Click(object sender, EventArgs e)
        {
            this.Controller.DisplayEditConnectionDialog();
        }

        private void editConnectionMenuItem_Click(object sender, EventArgs e)
        {
            this.Controller.DisplayEditConnectionDialog();
        }

        private void exitMenuItem_Click(object sender, EventArgs e)
        {
            this.Controller.Exit();
        }

        private void removeConnectionButton_Click(object sender, EventArgs e)
        {
            this.Controller.RemoveConnection();
        }

        private void removeConnectionMenuItem_Click(object sender, EventArgs e)
        {
            this.Controller.RemoveConnection();
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            this.Controller.Save();
        }

        private void saveMenuItem_Click(object sender, EventArgs e)
        {
            this.Controller.Save();
        }

        private void settingsMenuItem_Click(object sender, EventArgs e)
        {
            this.Controller.DisplaySettingsDialog();
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            this.Controller.OpenConnection();
        }

        private void startFileZillaHereMenuItem_Click(object sender, EventArgs e)
        {
            this.Controller.StartFileZilla();
        }

        private void startMenuItem_Click(object sender, EventArgs e)
        {
            this.Controller.OpenConnection();
        }

        private void startPsftpHereMenuItem_Click(object sender, EventArgs e)
        {
            this.Controller.StartPsftp();
        }

        private void startPuttyHereMenuItem_Click(object sender, EventArgs e)
        {
            this.Controller.StartPutty();
        }

        private void stopButton_Click(object sender, EventArgs e)
        {
            this.Controller.CloseConnection();
        }

        private void stopMenuItem_Click(object sender, EventArgs e)
        {
            this.Controller.CloseConnection();
        }
    }

    // ReSharper restore InconsistentNaming
}
