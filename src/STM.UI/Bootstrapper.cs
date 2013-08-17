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
using Ninject;
using STM.Core;
using STM.Core.Util;
using STM.UI.Forms;

namespace STM.UI
{
    public class Bootstrapper
    {
        public void Run()
        {
            this.LoadStyles();
            this.LoadDependecyInjector();

            Application.Run(new MainForm());
        }

        private void LoadDependecyInjector()
        {
            IoC.Kernel = new StandardKernel(new CoreModule(), new UIModule());
        }

        private void LoadStyles()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
        }
    }
}
