// ***********************************************************************
// <author>Stephan Burguchev</author>
// <copyright company="Stephan Burguchev">
//   Copyright (c) Stephan Burguchev 2012-2013. All rights reserved.
// </copyright>
// <summary>
//   ChangePasswordForm.cs
// </summary>
// ***********************************************************************

using System;
using System.Windows.Forms;
using STM.UI.Framework.Validation;
using STM.UI.Framework.Validation.Rules;

namespace STM.UI.Forms.ChangePassword
{
    public partial class ChangePasswordForm : Form, IChangePasswordForm
    {
        private readonly ValidationProvider validation = new ValidationProvider();

        public ChangePasswordForm(ChangePasswordFormController controller)
        {
            if (controller == null)
            {
                throw new ArgumentNullException("controller");
            }

            this.InitializeComponent();

            this.Controller = controller;
            this.Controller.Register(this);

            this.validation.ErrorProvider = this.myErrorProvider;
            this.validation.SetValidationRule(
                this.passwordTextBox,
                new AggregatedValidationRule(
                    AggregatedValidationMode.FirstFailed,
                    new PasswordValidationRule(),
                    new CompareToValidationRule(this.confirmPasswordTextBox, "The values of password fields does not match.")
                    ));
            this.validation.SetValidationRule(this.confirmPasswordTextBox, new PasswordValidationRule());
        }

        public ChangePasswordFormController Controller { get; private set; }

        public void Close(bool result)
        {
            this.DialogResult = result
                ? DialogResult.OK
                : DialogResult.Cancel;
            this.Close();
        }

        public string Collect()
        {
            return this.passwordTextBox.Text;
        }

        public bool DoValidate()
        {
            return this.validation.Validate();
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

        // ReSharper disable once InconsistentNaming
        private void okButton_Click(object sender, EventArgs e)
        {
            this.Controller.Ok();
        }
    }
}
