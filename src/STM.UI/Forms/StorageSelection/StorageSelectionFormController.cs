// ***********************************************************************
// <author>Stephan Burguchev</author>
// <copyright company="Stephan Burguchev">
//   Copyright (c) Stephan Burguchev 2012-2013. All rights reserved.
// </copyright>
// <summary>
//   StorageSelectionFormController.cs
// </summary>
// ***********************************************************************

using System;
using System.IO;
using STM.Core;
using STM.UI.Framework;
using STM.UI.Framework.Mvc;

namespace STM.UI.Forms.StorageSelection
{
    public class StorageSelectionFormController : ControllerBase<IStorageSelectionForm>
    {
        private readonly UserSettingsManager userSettingsManager;

        public StorageSelectionFormController(
            IMessageBoxService messageBoxService,
            IStandardDialogService standardDialogService,
            UserSettingsManager userSettingsManager)
            : base(messageBoxService, standardDialogService)
        {
            if (userSettingsManager == null)
            {
                throw new ArgumentNullException("userSettingsManager");
            }

            this.userSettingsManager = userSettingsManager;
        }

        public void Load()
        {
            var fileName = this.userSettingsManager.FileName;
            var password = this.userSettingsManager.Password;

            this.View.Render(fileName, password);
            var isNew = !string.IsNullOrEmpty(fileName);
            this.View.Render(isNew);
        }

        public void Open()
        {
            
        }

        public void Create()
        {
            
        }
    }
}
