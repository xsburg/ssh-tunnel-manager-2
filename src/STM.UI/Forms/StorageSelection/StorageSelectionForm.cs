using System;
using System.IO;
using System.Windows.Forms;
using STM.Core;
using STM.Core.Util;
using STM.UI.Framework;
using STM.UI.Framework.Mvc;
using STM.UI.Framework.Validation;
using STM.UI.Framework.Validation.Rules;
using STM.UI.Properties;

namespace STM.UI.Forms.StorageSelection
{
    public partial class StorageSelectionForm : Form, IStorageSelectionForm
    {
        private readonly ValidationProvider newStorageValidation = new ValidationProvider();
        private readonly ValidationProvider openStorageValidation = new ValidationProvider();

        public void Render(bool isNew)
        {
            
        }

        public void UpdateErrorText(string errorText)
        {
            this.errorTextLabel.Text = errorText;
            this.errorTextLabel.Visible = !string.IsNullOrEmpty(errorText);
        }

        public void Render(string userName, string password)
        {
            
        }

        public StorageSelectionForm(StorageSelectionFormController controller)
        {
            if (controller == null)
            {
                throw new ArgumentNullException("controller");
            }

            this.InitializeComponent();

            this.Controller = controller;
            this.Controller.Register(this);

            newStorageValidation.ErrorProvider = this.myErrorProvider;
            newStorageValidation.SetValidationRule(this.newFileNameTextBox, new FileValidationRule(true));
            newStorageValidation.SetValidationRule(this.newPasswordTextBox, new PasswordValidationRule());
            newStorageValidation.SetValidationRule(this.confirmNewPasswordTextBox, new PasswordValidationRule());
            newStorageValidation.SetValidationRule(
                this.newPasswordTextBox,
                new EqualityValidationRule(this.confirmNewPasswordTextBox));

            openStorageValidation.ErrorProvider = this.myErrorProvider;
            openStorageValidation.SetValidationRule(this.fileNameTextBox, new FileValidationRule(false));
            openStorageValidation.SetValidationRule(this.passwordTextBox, new PasswordValidationRule());

            //-----

            this.applySource(StorageSource.NewStorage);

            this.Error = null;

            // Stored settings loading
            /*var lastFile = Settings.Default.EncryptedStorageFile;
            var lastFileEmpty = string.IsNullOrEmpty(lastFile);
            var lastPass = Settings.Default.EncryptedStoragePassword;
            var lastPassEmpty = string.IsNullOrEmpty(lastPass);
            if (!lastFileEmpty)
            {
                this.radioButtonOpenStorage.Checked = true;
                this.textBoxExistingFile.Text = lastFile;
            }
            if (!lastPassEmpty)
            {
                this.textBoxOpenPassword.Text = lastPass;
                this.checkBoxSavePassOpen.Checked = true;
            }
            if (!lastFileEmpty && !lastPassEmpty)
            {
                // Both file and pass was saved last time
                try
                {
                    this.Storage = new EncryptedStorage(lastFile, lastPass);
                    this.Filename = lastFile;
                    this.Password = lastPass;
                }
                catch (Exception e)
                {
                    this.Error = e.Message;
                }
            }*/
        }

        public StorageSelectionFormController Controller { get; private set; }

        #region Validators

        /*private bool validateOpenFile(Control control, string text)
        {
            if (!this._validatorOpen.ValidateNotNullOrWhitespaces(control, text))
                return false;
            if (!File.Exists(text))
            {
                this._validatorOpen.SetError(control, Resources.ValidatorError_FileDoesNotExist);
                return false;
            }
            try
            {
                using (File.OpenRead(text))
                {
                }
            }
            catch (Exception e)
            {
                this._validatorOpen.SetError(control, e.Message);
            }
            this._validatorOpen.SetGood(control);
            return true;
        }

        private bool validateNewFile(Control control, string text)
        {
            if (!this._validatorCreate.ValidateNotNullOrWhitespaces(control, text))
                return false;
            
            this._validatorCreate.SetGood(control);
            return true;
        }

        private bool validateOpenPassword(Control control, string text)
        {
            if (!this._validatorCreate.ValidateNotNullOrWhitespaces(control, text))
                return false;
            if (!this._validatorCreate.ValidateOnlyOneWord(control, text))
                return false;
            this._validatorCreate.SetGood(control);
            return true;
        }

        private bool validateNewPassword(Control control, string text)
        {
            if (!this._validatorCreate.ValidateNotNullOrWhitespaces(control, text))
                return false;
            if (!this._validatorCreate.ValidateOnlyOneWord(control, text))
                return false;
            if (this.textBoxNewPassword.Text != this.textBoxNewPasswordConfirm.Text)
            {
                this._validatorCreate.SetError(this.textBoxNewPassword, Resources.ValidatorError_NewPassword);
                this._validatorCreate.SetError(this.textBoxNewPasswordConfirm, Resources.ValidatorError_NewPassword);
                return false;
            }
            this._validatorCreate.SetGood(this.textBoxNewPassword);
            this._validatorCreate.SetGood(this.textBoxNewPasswordConfirm);
            return true;
        }*/

        #endregion

        #region New region

        private void radioButtonCreateStorage_CheckedChanged(object sender, EventArgs e)
        {
            var source = this.createStorageRadioButton.Checked ? StorageSource.NewStorage : StorageSource.OpenStorage;
            this.applySource(source);
        }

        private void applySource(StorageSource source)
        {
            switch (source)
            {
            case StorageSource.NewStorage:
                this.NewStorageTableLayout.Visible = true;
                this.ExistingStorageTableLayout.Visible = false;
                this.createButton.Visible = true;
                this.openButton.Visible = false;
                break;
            case StorageSource.OpenStorage:
                this.NewStorageTableLayout.Visible = false;
                this.ExistingStorageTableLayout.Visible = true;
                this.createButton.Visible = false;
                this.openButton.Visible = true;
                break;
            default:
                throw new ArgumentOutOfRangeException();
            }
        }

        public enum StorageSource
        {
            NewStorage,
            OpenStorage
        }

        /// <summary>
        /// Global error (error after all validations).
        /// </summary>
        private string Error
        {
            get { return this.errorTextLabel.Text; }
            set
            {
                /*var isSet = !string.IsNullOrWhiteSpace(value);
                this.labelError.Visible = isSet;
                if (isSet)
                    this._validatorOpen.Reset(); // reset validator marks before global error setting (looks strange when they both enabled).
                this.labelError.Text = value;*/
            }
        }

        public string Filename { get; private set; }
        public string Password { get; private set; }
        public EncryptedStorage Storage { get; private set; }
        public bool DialogRequired { get { return this.Storage == null; } }

        private void buttonNewFileBrowse_Click(object sender, EventArgs e)
        {
            var result = this.mySaveFileDialog.ShowDialog(this);
            if (result != DialogResult.OK)
                return;
            this.newFileNameTextBox.Text = this.mySaveFileDialog.FileName;
        }

        private void buttonExistingFileBrowse_Click(object sender, EventArgs e)
        {
            var result = this.myOpenFileDialog.ShowDialog(this);
            if (result != DialogResult.OK)
                return;
            this.fileNameTextBox.Text = this.myOpenFileDialog.FileName;
        }

        private void buttonCreate_Click(object sender, EventArgs e)
        {
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

        private void buttonOpen_Click(object sender, EventArgs e)
        {
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

        #endregion
    }
}
