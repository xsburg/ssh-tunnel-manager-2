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
using STM.UI.Forms.MainForm;
using STM.UI.Forms.StorageSelection;
using STM.UI.Framework;

namespace STM.UI
{
    public class Bootstrapper
    {
        public void Run()
        {
            this.LoadStyles();
            this.LoadDependecyInjector();

            var windowManager = IoC.Get<IWindowManager>();
            var userSettingsManager = IoC.Get<IUserSettingsManager>();
            var storage = IoC.Get<IEncryptedStorage>();

            storage.Parameters.FileName = userSettingsManager.FileName;
            storage.Parameters.Password = userSettingsManager.Password;
            string errorText;
            if (!storage.Test(out errorText))
            {
                var form = windowManager.CreateView<IStorageSelectionForm>();
                form.Controller.Load();
                if (form.ShowDialog() != true)
                {
                    return;
                }
            }

            // TODO
            Application.Run((Form)windowManager.CreateView<IMainForm>());
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
