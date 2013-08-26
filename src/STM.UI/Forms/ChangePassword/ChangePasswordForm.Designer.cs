using STM.UI.Common.Controls;

namespace STM.UI.Forms.ChangePassword
{
    partial class ChangePasswordForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.Label passwordLabel;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ChangePasswordForm));
            System.Windows.Forms.Label confirmPasswordLabel;
            this.editorsTableLayout = new System.Windows.Forms.TableLayoutPanel();
            this.confirmPasswordTextBox = new System.Windows.Forms.TextBox();
            this.passwordTextBox = new System.Windows.Forms.TextBox();
            this.myLineSeparator = new STM.UI.Common.Controls.LineSeparatorControl();
            this.buttonsFlowLayout = new System.Windows.Forms.FlowLayoutPanel();
            this.cancelButton = new System.Windows.Forms.Button();
            this.okButton = new System.Windows.Forms.Button();
            this.rootFlowLayout = new System.Windows.Forms.FlowLayoutPanel();
            this.myErrorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            passwordLabel = new System.Windows.Forms.Label();
            confirmPasswordLabel = new System.Windows.Forms.Label();
            this.editorsTableLayout.SuspendLayout();
            this.buttonsFlowLayout.SuspendLayout();
            this.rootFlowLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.myErrorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // passwordLabel
            // 
            resources.ApplyResources(passwordLabel, "passwordLabel");
            passwordLabel.Name = "passwordLabel";
            // 
            // confirmPasswordLabel
            // 
            resources.ApplyResources(confirmPasswordLabel, "confirmPasswordLabel");
            confirmPasswordLabel.Name = "confirmPasswordLabel";
            // 
            // editorsTableLayout
            // 
            resources.ApplyResources(this.editorsTableLayout, "editorsTableLayout");
            this.editorsTableLayout.Controls.Add(passwordLabel, 0, 1);
            this.editorsTableLayout.Controls.Add(confirmPasswordLabel, 0, 2);
            this.editorsTableLayout.Controls.Add(this.confirmPasswordTextBox, 1, 2);
            this.editorsTableLayout.Controls.Add(this.passwordTextBox, 1, 1);
            this.editorsTableLayout.Name = "editorsTableLayout";
            // 
            // confirmPasswordTextBox
            // 
            resources.ApplyResources(this.confirmPasswordTextBox, "confirmPasswordTextBox");
            this.confirmPasswordTextBox.Name = "confirmPasswordTextBox";
            this.confirmPasswordTextBox.UseSystemPasswordChar = true;
            // 
            // passwordTextBox
            // 
            resources.ApplyResources(this.passwordTextBox, "passwordTextBox");
            this.passwordTextBox.Name = "passwordTextBox";
            this.passwordTextBox.UseSystemPasswordChar = true;
            // 
            // myLineSeparator
            // 
            resources.ApplyResources(this.myLineSeparator, "myLineSeparator");
            this.myLineSeparator.Name = "myLineSeparator";
            // 
            // buttonsFlowLayout
            // 
            resources.ApplyResources(this.buttonsFlowLayout, "buttonsFlowLayout");
            this.buttonsFlowLayout.Controls.Add(this.cancelButton);
            this.buttonsFlowLayout.Controls.Add(this.okButton);
            this.buttonsFlowLayout.Name = "buttonsFlowLayout";
            // 
            // cancelButton
            // 
            resources.ApplyResources(this.cancelButton, "cancelButton");
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.UseVisualStyleBackColor = true;
            // 
            // okButton
            // 
            resources.ApplyResources(this.okButton, "okButton");
            this.okButton.Name = "okButton";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // rootFlowLayout
            // 
            resources.ApplyResources(this.rootFlowLayout, "rootFlowLayout");
            this.rootFlowLayout.Controls.Add(this.editorsTableLayout);
            this.rootFlowLayout.Controls.Add(this.myLineSeparator);
            this.rootFlowLayout.Controls.Add(this.buttonsFlowLayout);
            this.rootFlowLayout.Name = "rootFlowLayout";
            // 
            // myErrorProvider
            // 
            this.myErrorProvider.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
            this.myErrorProvider.ContainerControl = this;
            resources.ApplyResources(this.myErrorProvider, "myErrorProvider");
            // 
            // ChangePasswordForm
            // 
            this.AcceptButton = this.okButton;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelButton;
            this.Controls.Add(this.rootFlowLayout);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ChangePasswordForm";
            this.editorsTableLayout.ResumeLayout(false);
            this.editorsTableLayout.PerformLayout();
            this.buttonsFlowLayout.ResumeLayout(false);
            this.rootFlowLayout.ResumeLayout(false);
            this.rootFlowLayout.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.myErrorProvider)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel editorsTableLayout;
        private System.Windows.Forms.TextBox confirmPasswordTextBox;
        private System.Windows.Forms.TextBox passwordTextBox;
        private LineSeparatorControl myLineSeparator;
        private System.Windows.Forms.FlowLayoutPanel buttonsFlowLayout;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.FlowLayoutPanel rootFlowLayout;
        private System.Windows.Forms.ErrorProvider myErrorProvider;
    }
}