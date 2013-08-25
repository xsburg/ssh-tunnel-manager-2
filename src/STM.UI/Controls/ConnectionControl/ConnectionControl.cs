// ***********************************************************************
// <author>Stephan Burguchev</author>
// <copyright company="Stephan Burguchev">
//   Copyright (c) Stephan Burguchev 2012-2013. All rights reserved.
// </copyright>
// <summary>
//   ConnectionControl.cs
// </summary>
// ***********************************************************************

using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using STM.Core;
using STM.Core.Data;
using STM.Core.Util;

namespace STM.UI.Controls.ConnectionControl
{
    public partial class ConnectionControl : UserControl, IConnectionControl
    {
        private const int MaxLogSize = 1000;
        private TunnelViewModel[] tunnelViewModels;

        public ConnectionControl(ConnectionControlController controller)
        {
            if (controller == null)
            {
                throw new ArgumentNullException("controller");
            }

            this.InitializeComponent();

            this.tunnelsGridView.AutoGenerateColumns = false;

            this.Controller = controller;
            this.Controller.Register(this);
        }

        public ConnectionControlController Controller { get; private set; }

        public void AddLogMessage(MessageSeverity severity, string message)
        {
            if (this.logListView.Items.Count >= MaxLogSize)
            {
                this.logListView.Items.RemoveAt(0);
            }

            var text = string.Format("{0} [{1:HH:mm:ss.fff}] {2}", severity.ToString().ToUpper(), DateTime.Now, message);
            this.logListView.Items.Add(text);
            this.logListView.Columns[0].AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
            this.logListView.TopItem = this.logListView.Items[this.logListView.Items.Count - 1];
        }

        public void RenderState(ConnectionViewModel viewModel)
        {
            this.stateValueLabel.Text = viewModel.State.ToString();
            this.stateValueLabel.ForeColor = viewModel.StateColor;
        }

        public void Render(ConnectionViewModel viewModel)
        {
            this.SuspendLayout();

            var parentName = viewModel.Info.Parent == null
                ? "-"
                : viewModel.Info.Parent.Name;

            this.nameValueLabel.Text = viewModel.Info.Name;
            this.parentValueLabel.Text = parentName;
            this.userNameValueLabel.Text = viewModel.Info.UserName;
            this.addressValueLabel.Text = string.Format("{0}:{1}", viewModel.Info.HostName, viewModel.Info.Port);
            this.RenderState(viewModel);

            this.tunnelViewModels = viewModel.Info.Tunnels.Select(t => new TunnelViewModel(t)).ToArray();
            this.tunnelsGridView.DataSource = this.tunnelViewModels;

            this.ResumeLayout(true);
        }

        public void RenderTunnelError(TunnelInfo tunnel, string errorMessage)
        {
            var viewModel = this.tunnelViewModels.First(t => t.Tunnel.Equals(tunnel));
            viewModel.ErrorText = errorMessage;
        }

        public void ResetTunnelErrors()
        {
            this.tunnelViewModels.Apply(t => t.ErrorText = null);
        }

        private void logListView_DrawSubItem(object sender, DrawListViewSubItemEventArgs e)
        {
            var item = e.Item.SubItems[e.ColumnIndex];

            e.DrawBackground();
            var text = item.Text;

            Color forecolor;
            if (text.StartsWith(@"ERROR") || text.StartsWith(@"FATAL"))
            {
                forecolor = Color.DarkRed;
            }
            else if (text.StartsWith(@"WARN"))
            {
                forecolor = Color.FromArgb(181, 166, 16);
            }
            else if (text.StartsWith(@"DEBUG"))
            {
                forecolor = Color.Black;
            }
            else
            {
                forecolor = Color.DarkBlue;
            }

            TextRenderer.DrawText(e.Graphics, item.Text, this.logListView.Font, e.Bounds, forecolor, TextFormatFlags.Left);
            e.DrawFocusRectangle(e.Bounds);
        }

        private void tunnelsGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
            {
                return;
            }

            var viewModel = (TunnelViewModel)this.tunnelsGridView.Rows[e.RowIndex].DataBoundItem;
            if (string.IsNullOrEmpty(viewModel.ErrorText))
            {
                return;
            }

            e.CellStyle.ForeColor = Color.DarkRed;
        }

        private void tunnelsGridView_SelectionChanged(object sender, EventArgs e)
        {
            this.tunnelsGridView.ClearSelection();
        }
    }
}
