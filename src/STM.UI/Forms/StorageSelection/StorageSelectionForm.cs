// ***********************************************************************
// <author>Stephan Burguchev</author>
// <copyright company="Stephan Burguchev">
//   Copyright (c) Stephan Burguchev 2012-2013. All rights reserved.
// </copyright>
// <summary>
//   StorageSelectionForm.cs
// </summary>
// ***********************************************************************

using System;
using System.Windows.Forms;
using STM.Core;
using STM.UI.Framework.Validation;
using STM.UI.Framework.Validation.Rules;

namespace STM.UI.Forms.StorageSelection
{
    // ReSharper disable InconsistentNaming
    public partial class StorageSelectionForm : Form, IStorageSelectionForm
    {
        private readonly ValidationProvider newStorageValidation = new ValidationProvider();
        private readonly ValidationProvider openStorageValidation = new ValidationProvider();

        public StorageSelectionForm(StorageSelectionFormController controller)
        {
            if (controller == null)
            {
                throw new ArgumentNullException("controller");
            }

            this.InitializeComponent();

            this.Controller = controller;
            this.Controller.Register(this);

            this.newStorageValidation.ErrorProvider = this.myErrorProvider;
            this.newStorageValidation.SetValidationRule(this.newFileNameTextBox, new FileValidationRule(true));
            this.newStorageValidation.SetValidationRule(this.newPasswordTextBox, new PasswordValidationRule());
            this.newStorageValidation.SetValidationRule(this.confirmNewPasswordTextBox, new PasswordValidationRule());
            this.newStorageValidation.SetValidationRule(
                this.newPasswordTextBox,
                new EqualityValidationRule(this.confirmNewPasswordTextBox));

            this.openStorageValidation.ErrorProvider = this.myErrorProvider;
            this.openStorageValidation.SetValidationRule(this.fileNameTextBox, new FileValidationRule(false));
            this.openStorageValidation.SetValidationRule(this.passwordTextBox, new PasswordValidationRule());
        }

        public StorageSelectionFormController Controller { get; private set; }

        public void Close(bool result)
        {
            this.DialogResult = result
                ? DialogResult.OK
                : DialogResult.Cancel;
            this.Close();
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

        public bool DoValidate()
        {
            var isNew = this.createStorageRadioButton.Checked;
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

            if (viewModel.Mode != null)
            {
                var isNew = viewModel.Mode == StorageSelectionFormMode.New;
                this.createStorageRadioButton.Checked = isNew;
                this.openStorageRadioButton.Checked = !isNew;
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

        public void RenderError(string errorText)
        {
            this.errorTextLabel.Text = errorText;
            this.errorTextLabel.Visible = !string.IsNullOrEmpty(errorText);
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

        private void browseFileNameButton_Click(object sender, EventArgs e)
        {
            this.Controller.BrowseStorageFile();
        }

        private void browseNewFileNameButton_Click(object sender, EventArgs e)
        {
            this.Controller.BrowseNewStorageFile();
        }

        private void createButton_Click(object sender, EventArgs e)
        {
            this.Controller.Create();
        }

        private void createStorageRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            this.Render(
                new StorageSelectionFormViewModel
                    {
                        Mode = this.createStorageRadioButton.Checked
                            ? StorageSelectionFormMode.New
                            : StorageSelectionFormMode.Open
                    });
        }

        private void openButton_Click(object sender, EventArgs e)
        {
            this.Controller.Open();
        }
    }

    // ReSharper restore InconsistentNaming
}
