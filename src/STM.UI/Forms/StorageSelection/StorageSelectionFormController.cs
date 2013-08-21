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
using Ninject.Extensions.Logging;
using STM.Core;
using STM.Core.Data;
using STM.UI.Framework;
using STM.UI.Framework.Mvc;

namespace STM.UI.Forms.StorageSelection
{
    public class StorageSelectionFormController : ControllerBase<IStorageSelectionForm>
    {
        private const string FileDialogFilter = "Storage files|*.xstg|All files|*.*";
        private const string StorageFileExtension = "*.xstg";
        private readonly UserSettingsManager userSettingsManager;
        private readonly IEncryptedStorage storage;

        public StorageSelectionFormController(
            ILogger logger,
            IMessageBoxService messageBoxService,
            IStandardDialogService standardDialogService,
            UserSettingsManager userSettingsManager,
            IEncryptedStorage storage)
            : base(logger, messageBoxService, standardDialogService)
        {
            if (userSettingsManager == null)
            {
                throw new ArgumentNullException("userSettingsManager");
            }

            if (storage == null)
            {
                throw new ArgumentNullException("storage");
            }

            this.userSettingsManager = userSettingsManager;
            this.storage = storage;
        }

        public void Load()
        {
            var fileName = this.userSettingsManager.FileName;
            var password = this.userSettingsManager.Password;
            var isNew = !string.IsNullOrEmpty(fileName);

            this.View.RenderError("");
            this.View.Render(
                new StorageSelectionFormViewModel
                    {
                        FileName = fileName,
                        Password = password,
                        IsNew = isNew,
                        SavePassword = password != null ? (bool?)true : null
                    });
        }

        public void Open()
        {
            if (!this.View.DoValidate())
            {
                return;
            }

            storage.Parameters = this.View.Collect();
            string errorText;
            if (storage.Test(out errorText))
            {
                this.View.RenderError("");
                this.View.Close(true);
            }
            else
            {
                this.View.RenderError(errorText);
            }
        }

        public void Create()
        {
            if (!this.View.DoValidate())
            {
                return;
            }

            try
            {
                storage.Parameters = this.View.Collect();
                this.storage.Save(new EncryptedStorageContent());
            }
            catch (Exception ex)
            {
                this.View.RenderError(ex.Message);
                Logger.Debug(ex, "Failed to create a storage.");
            }
        }

        public void BrowseNewStorageFile()
        {
            var fileName = StandardDialogService.ShowSaveFileDialog(FileDialogFilter, StorageFileExtension);
            if (fileName != null)
            {
                this.View.Render(
                    new StorageSelectionFormViewModel
                        {
                            NewFileName = fileName
                        });
            }
        }

        public void BrowseStorageFile()
        {
            var fileName = StandardDialogService.ShowOpenFileDialog(FileDialogFilter, StorageFileExtension);
            if (fileName != null)
            {
                this.View.Render(
                    new StorageSelectionFormViewModel
                        {
                            FileName = fileName
                        });
            }
        }
    }
}
