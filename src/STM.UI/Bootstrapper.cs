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
using STM.UI.Forms.Main;
using STM.UI.Forms.StorageSelection;
using STM.UI.Framework;

namespace STM.UI
{
    public class Bootstrapper
    {
        private IWindowManager windowManager;

        public void Run()
        {
            LoadStyles();
            LoadDependecyInjector();

            this.windowManager = IoC.Get<IWindowManager>();
            var userSettingsManager = IoC.Get<IUserSettingsManager>();
            var storage = IoC.Get<IEncryptedStorage>();

            storage.Parameters.FileName = userSettingsManager.FileName;
            storage.Parameters.Password = userSettingsManager.Password;
            string errorText;
            if (!storage.Test(out errorText) && !this.DisplayStorageSelectionForm())
            {
                return;
            }

            var mainForm = this.DisplayMainForm();
            while (mainForm.Controller.DisplayStorageSelectionAfterExit && this.DisplayStorageSelectionForm())
            {
                mainForm = this.DisplayMainForm();
            }
        }

        private static void LoadDependecyInjector()
        {
            IoC.Kernel = new StandardKernel(new CoreModule(), new UIModule());
        }

        private static void LoadStyles()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
        }

        private IMainForm DisplayMainForm()
        {
            var mainForm = this.windowManager.CreateView<IMainForm>();
            mainForm.Controller.Load();
            Application.Run((Form)mainForm);
            return mainForm;
        }

        private bool DisplayStorageSelectionForm()
        {
            var form = this.windowManager.CreateView<IStorageSelectionForm>();
            form.Controller.Load();
            return form.ShowDialog() == true;
        }
    }
}
