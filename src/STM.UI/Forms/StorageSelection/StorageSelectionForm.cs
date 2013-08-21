using System;
using System.Windows.Forms;
using STM.Core;
using STM.UI.Framework.Validation;
using STM.UI.Framework.Validation.Rules;

namespace STM.UI.Forms.StorageSelection
{
    public partial class StorageSelectionForm : Form, IStorageSelectionForm
    {
        private readonly ValidationProvider newStorageValidation = new ValidationProvider();
        private readonly ValidationProvider openStorageValidation = new ValidationProvider();

        public bool DoValidate()
        {
            var isNew = createStorageRadioButton.Checked;
            var validation = isNew
                ? this.newStorageValidation
                : this.openStorageValidation;
            return validation.Validate();
        }

        public void Render(StorageSelectionFormViewModel viewModel)
        {
            if (viewModel.FileName != null)
            {
                this.fileNameTextBox.Text = viewModel.FileName;
            }

            if (viewModel.NewFileName != null)
            {
                this.newFileNameTextBox.Text = viewModel.NewFileName;
            }
            
            if (viewModel.Password != null)
            {
                this.passwordTextBox.Text = viewModel.Password;
            }

            if (viewModel.IsNew != null)
            {
                var isNew = viewModel.IsNew == true;
                this.createStorageRadioButton.Checked = isNew;
                this.NewStorageTableLayout.Visible = isNew;
                this.ExistingStorageTableLayout.Visible = !isNew;
                this.createButton.Visible = isNew;
                this.openButton.Visible = !isNew;
            }

            if (viewModel.SavePassword != null)
            {
                this.savePasswordCheckBox.Checked = viewModel.SavePassword == true;
            }
        }

        public EncryptedStorageParameters Collect()
        {
            var parameters = new EncryptedStorageParameters();
            if (this.createStorageRadioButton.Checked)
            {
                parameters.FileName = this.newFileNameTextBox.Text;
                parameters.Password = this.newPasswordTextBox.Text;
            }
            else
            {
                parameters.FileName = this.fileNameTextBox.Text;
                parameters.Password = this.passwordTextBox.Text;
            }

            return parameters;
        }

        public void RenderError(string errorText)
        {
            this.errorTextLabel.Text = errorText;
            this.errorTextLabel.Visible = !string.IsNullOrEmpty(errorText);
        }

        // ReSharper disable once InconsistentNaming
        private void createStorageRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            this.Render(
                new StorageSelectionFormViewModel
                    {
                        IsNew = this.createStorageRadioButton.Checked
                    });
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

        public new bool? ShowDialog()
        {
            var result = base.ShowDialog();
            switch (result)
            {
            case DialogResult.OK:
                return true;
            case DialogResult.Cancel:
                return false;
            default:
                return null;
            }
        }

        public void Close(bool result)
        {
            this.DialogResult = result
                ? DialogResult.OK
                : DialogResult.Cancel;
            this.Close();
        }

        // ReSharper disable once InconsistentNaming
        private void browseNewFileNameButton_Click(object sender, EventArgs e)
        {
            this.Controller.BrowseNewStorageFile();
        }

        // ReSharper disable once InconsistentNaming
        private void browseFileNameButton_Click(object sender, EventArgs e)
        {
            this.Controller.BrowseStorageFile();
        }

        public StorageSelectionFormController Controller { get; private set; }

        #region New region

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
