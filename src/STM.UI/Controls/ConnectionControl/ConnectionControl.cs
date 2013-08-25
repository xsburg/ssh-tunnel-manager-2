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
using System.Windows.Forms;
using STM.Core;
using STM.Core.Data;

namespace STM.UI.Controls.ConnectionControl
{
    public partial class ConnectionControl : UserControl, IConnectionControl
    {
        public ConnectionControl(ConnectionControlController controller)
        {
            if (controller == null)
            {
                throw new ArgumentNullException("controller");
            }

            this.InitializeComponent();

            this.Controller = controller;
            this.Controller.Register(this);

            // TODO: View initialization code here
        }

        public ConnectionControlController Controller { get; private set; }

        public void Render(ConnectionState state)
        {
            this.SuspendLayout();

            // TODO: View render here

            this.ResumeLayout(true);
        }

        public void Render(ConnectionViewModel connectionInfo)
        {
            this.SuspendLayout();

            // TODO: View render here

            this.ResumeLayout(true);
        }

        private void logListView_DrawSubItem(object sender, DrawListViewSubItemEventArgs e)
        {

        }

        private void tunnelsGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {

        }

        private void tunnelsGridView_SelectionChanged(object sender, EventArgs e)
        {

        }
    }
}
