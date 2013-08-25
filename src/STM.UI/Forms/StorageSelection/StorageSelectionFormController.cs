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
        private readonly IEncryptedStorage storage;
        private readonly IUserSettingsManager userSettingsManager;

        public StorageSelectionFormController(
            ILogger logger,
            IMessageBoxService messageBoxService,
            IStandardDialogService standardDialogService,
            IUserSettingsManager userSettingsManager,
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

        public void BrowseNewStorageFile()
        {
            var fileName = this.StandardDialogService.ShowSaveFileDialog(FileDialogFilter, StorageFileExtension);
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
            var fileName = this.StandardDialogService.ShowOpenFileDialog(FileDialogFilter, StorageFileExtension);
            if (fileName != null)
            {
                this.View.Render(
                    new StorageSelectionFormViewModel
                        {
                            FileName = fileName
                        });
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
                this.View.RenderError("");
                this.storage.Parameters = this.View.Collect();
                this.storage.Save();
                this.SaveAndClose();
            }
            catch (Exception ex)
            {
                this.View.RenderError(ex.Message);
                this.Logger.Debug(ex, "Failed to create a storage.");
            }
        }

        public void Load()
        {
            var fileName = this.userSettingsManager.FileName;
            var password = this.userSettingsManager.Password;
            var mode = string.IsNullOrEmpty(fileName)
                ? StorageSelectionFormMode.New
                : StorageSelectionFormMode.Open;

            this.View.RenderError("");
            this.View.Render(
                new StorageSelectionFormViewModel
                    {
                        FileName = fileName,
                        Password = password,
                        Mode = mode,
                        SavePassword = !string.IsNullOrEmpty(password)
                            ? (bool?)true
                            : null
                    });
        }

        public void Open()
        {
            if (!this.View.DoValidate())
            {
                return;
            }

            this.storage.Parameters = this.View.Collect();
            try
            {
                string errorText;
                if (this.storage.Test(out errorText))
                {
                    this.View.RenderError("");
                    SaveAndClose();
                }
                else
                {
                    this.View.RenderError(errorText);
                }
            }
            catch (Exception ex)
            {
                this.View.RenderError(ex.Message);
            }
        }

        private void SaveAndClose()
        {
            if (this.View.SavePassword)
            {
                this.userSettingsManager.FileName = this.storage.Parameters.FileName;
                this.userSettingsManager.Password = this.storage.Parameters.Password;
                this.userSettingsManager.Save();
            }

            this.View.Close(true);
        }
    }
}
