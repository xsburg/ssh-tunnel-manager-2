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
        private readonly UserSettingsManager userSettingsManager;

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
                this.storage.Parameters = this.View.Collect();
                this.storage.Save(new EncryptedStorageContent());
            }
            catch (Exception ex)
            {
                this.View.RenderError(ex.Message);
                this.Logger.Debug(ex, "Failed to create a storage.");
            }

            /*if (!this._validatorCreate.ValidateControls())
                return;

            var filename = this.textBoxNewFile.Text;
            var password = this.textBoxNewPassword.Text;
            var savePass = this.checkBoxSavePassNew.Checked;
            try
            {
                var storage = new EncryptedStorage();
                /*storage.Data.Config.RestartEnabled = Settings.Default.Config_RestartEnabled;
                storage.Data.Config.RestartDelay = Settings.Default.Config_RestartDelay;
                storage.Data.Config.MaxAttemptsCount = Settings.Default.Config_MaxAttemptsCount;#1#
                storage.Save(filename, password);
                this.Storage = storage;
                this.Filename = filename;
                this.Password = password;
                Settings.Default.EncryptedStorageFile = filename;
                Settings.Default.EncryptedStoragePassword = savePass ? password : null;
                Settings.Default.Save();
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                this.Error = ex.Message;
            }*/
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
                        SavePassword = password != null
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
            string errorText;
            if (this.storage.Test(out errorText))
            {
                this.View.RenderError("");
                this.View.Close(true);
            }
            else
            {
                this.View.RenderError(errorText);
            }

            /*if (!this._validatorOpen.ValidateControls())
                return;

            var filename = this.textBoxExistingFile.Text;
            var password = this.textBoxOpenPassword.Text;
            var savePass = this.checkBoxSavePassOpen.Checked;
            try
            {
                this.Storage = new EncryptedStorage(filename, password);
                this.Filename = filename;
                this.Password = password;
                Settings.Default.EncryptedStorageFile = filename;
                Settings.Default.EncryptedStoragePassword = savePass ? password : null;
                Settings.Default.Save();
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                this.Error = ex.Message.TrimEnd('.');
            }*/
        }
    }
}
