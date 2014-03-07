// ***********************************************************************
// <author>Stephan Burguchev</author>
// <copyright company="Stephan Burguchev">
//   Copyright (c) Stephan Burguchev 2012-2013. All rights reserved.
// </copyright>
// <summary>
//   LineSeparator.cs
// </summary>
// ***********************************************************************

using System.Drawing;
using System.Windows.Forms;

namespace SSHTunnelManagerGUI.Controls
{
    public partial class LineSeparator : UserControl
    {
        public LineSeparator()
        {
            this.InitializeComponent();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            var g = e.Graphics;
            g.DrawLine(Pens.DarkGray, new Point(0, 0), new Point(this.Width, 0));
            g.DrawLine(Pens.White, new Point(0, 1), new Point(this.Width, 1));
        }
    }
}
