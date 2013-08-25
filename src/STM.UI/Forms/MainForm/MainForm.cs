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
using System.Drawing;
using System.Windows.Forms;
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
            this.splitContainerV1.Panel2.Controls.Add(connectionControl);
        }

        public MainFormController Controller { get; private set; }

        public void Render()
        {
            this.SuspendLayout();

            // TODO: View render here

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
    }
}
