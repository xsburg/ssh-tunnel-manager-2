// ***********************************************************************
// <author>Stephan Burguchev</author>
// <copyright company="Stephan Burguchev">
//   Copyright (c) Stephan Burguchev 2012-2013. All rights reserved.
// </copyright>
// <summary>
//   Bootstrapper.cs
// </summary>
// ***********************************************************************

using System.Windows.Forms;
using STM.UI.Forms;

namespace STM.UI
{
    public class Bootstrapper
    {
        public void Run()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // TODO

            Application.Run(new MainForm());
        }
    }
}