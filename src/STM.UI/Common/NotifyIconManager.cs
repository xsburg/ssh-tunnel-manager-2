// ***********************************************************************
// <author>Stephan Burguchev</author>
// <copyright company="Stephan Burguchev">
//   Copyright (c) Stephan Burguchev 2012-2013. All rights reserved.
// </copyright>
// <summary>
//   NotifyIconManager.cs
// </summary>
// ***********************************************************************

using System;
using System.Windows.Forms;
using STM.Core.Util;

namespace STM.UI.Common
{
    public class NotifyIconManager
    {
        private bool closing;
        private Form form;

        public NotifyIconManager(Form form)
        {
            if (form == null)
            {
                throw new ArgumentNullException("form");
            }

            this.RegisterForm(form);

            this.NotifyIcon = new NotifyIcon
                {
                    Visible = true,
                    ContextMenuStrip = new ContextMenuStrip()
                };
            this.NotifyIcon.MouseClick += this.HandleNotifyIconClick;
        }

        public NotifyIcon NotifyIcon { get; private set; }

        public void CloseForm()
        {
            this.closing = true;
            this.form.Close();
        }

        public void MinimizeFormToTray()
        {
            if (this.form.Visible)
            {
                this.form.Hide();
            }

            this.form.WindowState = FormWindowState.Minimized;
        }

        public void RestoreForm()
        {
            if (!this.form.Visible)
            {
                this.form.Show();
            }

            if (!this.form.Focused)
            {
                this.form.Activate();
            }

            this.form.WindowState = FormWindowState.Normal;
        }

        public void SwitchFormState()
        {
            if (this.form.WindowState == FormWindowState.Minimized)
            {
                this.RestoreForm();
            }
            else
            {
                this.MinimizeFormToTray();
            }
        }

        private void HandleFormClosing(object sender, FormClosingEventArgs e)
        {
            if (!this.closing && e.CloseReason == CloseReason.UserClosing)
            {
                var applicationName = AssemblyAttributes.AssemblyTitle;
                this.NotifyIcon.ShowBalloonTip(
                    2000,
                    applicationName,
                    string.Format("{0} is minimized here", applicationName),
                    ToolTipIcon.Info);
                this.MinimizeFormToTray();
                e.Cancel = true;
            }
            else
            {
                this.NotifyIcon.Dispose();
            }
        }

        private void HandleFormResize(object sender, EventArgs e)
        {
            switch (this.form.WindowState)
            {
            case FormWindowState.Minimized:
                this.MinimizeFormToTray();
                break;
            case FormWindowState.Normal:
                this.RestoreForm();
                break;
            }
        }

        private void HandleNotifyIconClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.SwitchFormState();
            }
        }

        // ReSharper disable once ParameterHidesMember
        private void RegisterForm(Form form)
        {
            this.form = form;
            this.form.FormClosing += this.HandleFormClosing;
            this.form.Resize += this.HandleFormResize;
        }
    }
}
