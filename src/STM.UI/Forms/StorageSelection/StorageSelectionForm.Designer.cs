using STM.UI.Common.Controls;

namespace STM.UI.Forms.StorageSelection
{
    partial class StorageSelectionForm
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
            System.Windows.Forms.Label newFileNameLabel;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StorageSelectionForm));
            System.Windows.Forms.Label newPasswordLabel;
            System.Windows.Forms.Label confirmNewPasswordLabel;
            System.Windows.Forms.Label fileNameLabel;
            System.Windows.Forms.Label passwordLabel;
            this.browseNewFileNameButton = new System.Windows.Forms.Button();
            this.newPasswordTextBox = new System.Windows.Forms.TextBox();
            this.newFileNameTextBox = new System.Windows.Forms.TextBox();
            this.confirmNewPasswordTextBox = new System.Windows.Forms.TextBox();
            this.actionGroupBox = new System.Windows.Forms.GroupBox();
            this.createStorageRadioButton = new System.Windows.Forms.RadioButton();
            this.openStorageRadioButton = new System.Windows.Forms.RadioButton();
            this.NewStorageTableLayout = new System.Windows.Forms.TableLayoutPanel();
            this.newSavePasswordCheckBox = new System.Windows.Forms.CheckBox();
            this.ExistingStorageTableLayout = new System.Windows.Forms.TableLayoutPanel();
            this.browseFileNameButton = new System.Windows.Forms.Button();
            this.savePasswordCheckBox = new System.Windows.Forms.CheckBox();
            this.passwordTextBox = new System.Windows.Forms.TextBox();
            this.fileNameTextBox = new System.Windows.Forms.TextBox();
            this.rootFlowLayout = new System.Windows.Forms.FlowLayoutPanel();
            this.errorTextLabel = new System.Windows.Forms.Label();
            this.buttonsFlowLayout = new System.Windows.Forms.FlowLayoutPanel();
            this.exitButton = new System.Windows.Forms.Button();
            this.createButton = new System.Windows.Forms.Button();
            this.openButton = new System.Windows.Forms.Button();
            this.myOpenFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.mySaveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.myErrorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.myLineSeparator = new STM.UI.Common.Controls.LineSeparatorControl();
            newFileNameLabel = new System.Windows.Forms.Label();
            newPasswordLabel = new System.Windows.Forms.Label();
            confirmNewPasswordLabel = new System.Windows.Forms.Label();
            fileNameLabel = new System.Windows.Forms.Label();
            passwordLabel = new System.Windows.Forms.Label();
            this.actionGroupBox.SuspendLayout();
            this.NewStorageTableLayout.SuspendLayout();
            this.ExistingStorageTableLayout.SuspendLayout();
            this.rootFlowLayout.SuspendLayout();
            this.buttonsFlowLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.myErrorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // newFileNameLabel
            // 
            resources.ApplyResources(newFileNameLabel, "newFileNameLabel");
            newFileNameLabel.Name = "newFileNameLabel";
            // 
            // newPasswordLabel
            // 
            resources.ApplyResources(newPasswordLabel, "newPasswordLabel");
            newPasswordLabel.Name = "newPasswordLabel";
            // 
            // confirmNewPasswordLabel
            // 
            resources.ApplyResources(confirmNewPasswordLabel, "confirmNewPasswordLabel");
            confirmNewPasswordLabel.Name = "confirmNewPasswordLabel";
            // 
            // fileNameLabel
            // 
            resources.ApplyResources(fileNameLabel, "fileNameLabel");
            fileNameLabel.Name = "fileNameLabel";
            // 
            // passwordLabel
            // 
            resources.ApplyResources(passwordLabel, "passwordLabel");
            passwordLabel.Name = "passwordLabel";
            // 
            // browseNewFileNameButton
            // 
            resources.ApplyResources(this.browseNewFileNameButton, "browseNewFileNameButton");
            this.browseNewFileNameButton.Name = "browseNewFileNameButton";
            this.browseNewFileNameButton.UseVisualStyleBackColor = true;
            this.browseNewFileNameButton.Click += new System.EventHandler(this.buttonNewFileBrowse_Click);
            // 
            // newPasswordTextBox
            // 
            resources.ApplyResources(this.newPasswordTextBox, "newPasswordTextBox");
            this.NewStorageTableLayout.SetColumnSpan(this.newPasswordTextBox, 2);
            this.newPasswordTextBox.Name = "newPasswordTextBox";
            this.newPasswordTextBox.UseSystemPasswordChar = true;
            // 
            // newFileNameTextBox
            // 
            resources.ApplyResources(this.newFileNameTextBox, "newFileNameTextBox");
            this.myErrorProvider.SetIconPadding(this.newFileNameTextBox, ((int)(resources.GetObject("newFileNameTextBox.IconPadding"))));
            this.newFileNameTextBox.Name = "newFileNameTextBox";
            // 
            // confirmNewPasswordTextBox
            // 
            resources.ApplyResources(this.confirmNewPasswordTextBox, "confirmNewPasswordTextBox");
            this.NewStorageTableLayout.SetColumnSpan(this.confirmNewPasswordTextBox, 2);
            this.confirmNewPasswordTextBox.Name = "confirmNewPasswordTextBox";
            this.confirmNewPasswordTextBox.UseSystemPasswordChar = true;
            // 
            // actionGroupBox
            // 
            resources.ApplyResources(this.actionGroupBox, "actionGroupBox");
            this.actionGroupBox.Controls.Add(this.createStorageRadioButton);
            this.actionGroupBox.Controls.Add(this.openStorageRadioButton);
            this.actionGroupBox.Name = "actionGroupBox";
            this.actionGroupBox.TabStop = false;
            // 
            // createStorageRadioButton
            // 
            resources.ApplyResources(this.createStorageRadioButton, "createStorageRadioButton");
            this.createStorageRadioButton.Checked = true;
            this.createStorageRadioButton.Name = "createStorageRadioButton";
            this.createStorageRadioButton.TabStop = true;
            this.createStorageRadioButton.UseVisualStyleBackColor = true;
            this.createStorageRadioButton.CheckedChanged += new System.EventHandler(this.radioButtonCreateStorage_CheckedChanged);
            // 
            // openStorageRadioButton
            // 
            resources.ApplyResources(this.openStorageRadioButton, "openStorageRadioButton");
            this.openStorageRadioButton.Name = "openStorageRadioButton";
            this.openStorageRadioButton.UseVisualStyleBackColor = true;
            // 
            // NewStorageTableLayout
            // 
            resources.ApplyResources(this.NewStorageTableLayout, "NewStorageTableLayout");
            this.NewStorageTableLayout.Controls.Add(newFileNameLabel, 0, 0);
            this.NewStorageTableLayout.Controls.Add(newPasswordLabel, 0, 1);
            this.NewStorageTableLayout.Controls.Add(this.browseNewFileNameButton, 2, 0);
            this.NewStorageTableLayout.Controls.Add(this.newSavePasswordCheckBox, 0, 3);
            this.NewStorageTableLayout.Controls.Add(confirmNewPasswordLabel, 0, 2);
            this.NewStorageTableLayout.Controls.Add(this.confirmNewPasswordTextBox, 1, 2);
            this.NewStorageTableLayout.Controls.Add(this.newPasswordTextBox, 1, 1);
            this.NewStorageTableLayout.Controls.Add(this.newFileNameTextBox, 1, 0);
            this.NewStorageTableLayout.Name = "NewStorageTableLayout";
            // 
            // newSavePasswordCheckBox
            // 
            resources.ApplyResources(this.newSavePasswordCheckBox, "newSavePasswordCheckBox");
            this.newSavePasswordCheckBox.Name = "newSavePasswordCheckBox";
            this.newSavePasswordCheckBox.UseVisualStyleBackColor = true;
            // 
            // ExistingStorageTableLayout
            // 
            resources.ApplyResources(this.ExistingStorageTableLayout, "ExistingStorageTableLayout");
            this.ExistingStorageTableLayout.Controls.Add(fileNameLabel, 0, 0);
            this.ExistingStorageTableLayout.Controls.Add(passwordLabel, 0, 1);
            this.ExistingStorageTableLayout.Controls.Add(this.browseFileNameButton, 2, 0);
            this.ExistingStorageTableLayout.Controls.Add(this.savePasswordCheckBox, 0, 2);
            this.ExistingStorageTableLayout.Controls.Add(this.passwordTextBox, 1, 1);
            this.ExistingStorageTableLayout.Controls.Add(this.fileNameTextBox, 1, 0);
            this.ExistingStorageTableLayout.Name = "ExistingStorageTableLayout";
            // 
            // browseFileNameButton
            // 
            resources.ApplyResources(this.browseFileNameButton, "browseFileNameButton");
            this.browseFileNameButton.Name = "browseFileNameButton";
            this.browseFileNameButton.UseVisualStyleBackColor = true;
            this.browseFileNameButton.Click += new System.EventHandler(this.buttonExistingFileBrowse_Click);
            // 
            // savePasswordCheckBox
            // 
            resources.ApplyResources(this.savePasswordCheckBox, "savePasswordCheckBox");
            this.savePasswordCheckBox.Name = "savePasswordCheckBox";
            this.savePasswordCheckBox.UseVisualStyleBackColor = true;
            // 
            // passwordTextBox
            // 
            resources.ApplyResources(this.passwordTextBox, "passwordTextBox");
            this.ExistingStorageTableLayout.SetColumnSpan(this.passwordTextBox, 2);
            this.passwordTextBox.Name = "passwordTextBox";
            this.passwordTextBox.UseSystemPasswordChar = true;
            // 
            // fileNameTextBox
            // 
            resources.ApplyResources(this.fileNameTextBox, "fileNameTextBox");
            this.myErrorProvider.SetIconPadding(this.fileNameTextBox, ((int)(resources.GetObject("fileNameTextBox.IconPadding"))));
            this.fileNameTextBox.Name = "fileNameTextBox";
            // 
            // rootFlowLayout
            // 
            resources.ApplyResources(this.rootFlowLayout, "rootFlowLayout");
            this.rootFlowLayout.Controls.Add(this.actionGroupBox);
            this.rootFlowLayout.Controls.Add(this.NewStorageTableLayout);
            this.rootFlowLayout.Controls.Add(this.ExistingStorageTableLayout);
            this.rootFlowLayout.Controls.Add(this.myLineSeparator);
            this.rootFlowLayout.Controls.Add(this.errorTextLabel);
            this.rootFlowLayout.Controls.Add(this.buttonsFlowLayout);
            this.rootFlowLayout.Name = "rootFlowLayout";
            // 
            // errorTextLabel
            // 
            resources.ApplyResources(this.errorTextLabel, "errorTextLabel");
            this.errorTextLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(165)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.errorTextLabel.Name = "errorTextLabel";
            // 
            // buttonsFlowLayout
            // 
            resources.ApplyResources(this.buttonsFlowLayout, "buttonsFlowLayout");
            this.buttonsFlowLayout.Controls.Add(this.exitButton);
            this.buttonsFlowLayout.Controls.Add(this.createButton);
            this.buttonsFlowLayout.Controls.Add(this.openButton);
            this.buttonsFlowLayout.Name = "buttonsFlowLayout";
            // 
            // exitButton
            // 
            resources.ApplyResources(this.exitButton, "exitButton");
            this.exitButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.exitButton.Name = "exitButton";
            this.exitButton.UseVisualStyleBackColor = true;
            // 
            // createButton
            // 
            resources.ApplyResources(this.createButton, "createButton");
            this.createButton.Name = "createButton";
            this.createButton.UseVisualStyleBackColor = true;
            this.createButton.Click += new System.EventHandler(this.buttonCreate_Click);
            // 
            // openButton
            // 
            resources.ApplyResources(this.openButton, "openButton");
            this.openButton.Name = "openButton";
            this.openButton.UseVisualStyleBackColor = true;
            this.openButton.Click += new System.EventHandler(this.buttonOpen_Click);
            // 
            // myOpenFileDialog
            // 
            this.myOpenFileDialog.DefaultExt = "*.xstg";
            resources.ApplyResources(this.myOpenFileDialog, "myOpenFileDialog");
            // 
            // mySaveFileDialog
            // 
            this.mySaveFileDialog.DefaultExt = "*.xstg";
            resources.ApplyResources(this.mySaveFileDialog, "mySaveFileDialog");
            // 
            // myErrorProvider
            // 
            this.myErrorProvider.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
            this.myErrorProvider.ContainerControl = this;
            resources.ApplyResources(this.myErrorProvider, "myErrorProvider");
            // 
            // myLineSeparator
            // 
            resources.ApplyResources(this.myLineSeparator, "myLineSeparator");
            this.myLineSeparator.Name = "myLineSeparator";
            // 
            // StorageSelectionForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.exitButton;
            this.Controls.Add(this.rootFlowLayout);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "StorageSelectionForm";
            this.actionGroupBox.ResumeLayout(false);
            this.actionGroupBox.PerformLayout();
            this.NewStorageTableLayout.ResumeLayout(false);
            this.NewStorageTableLayout.PerformLayout();
            this.ExistingStorageTableLayout.ResumeLayout(false);
            this.ExistingStorageTableLayout.PerformLayout();
            this.rootFlowLayout.ResumeLayout(false);
            this.rootFlowLayout.PerformLayout();
            this.buttonsFlowLayout.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.myErrorProvider)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button browseNewFileNameButton;
        private System.Windows.Forms.TextBox newPasswordTextBox;
        private System.Windows.Forms.TextBox newFileNameTextBox;
        private System.Windows.Forms.TextBox confirmNewPasswordTextBox;
        private System.Windows.Forms.GroupBox actionGroupBox;
        private System.Windows.Forms.RadioButton createStorageRadioButton;
        private System.Windows.Forms.RadioButton openStorageRadioButton;
        private System.Windows.Forms.TableLayoutPanel NewStorageTableLayout;
        private System.Windows.Forms.TableLayoutPanel ExistingStorageTableLayout;
        private System.Windows.Forms.Button browseFileNameButton;
        private System.Windows.Forms.TextBox passwordTextBox;
        private System.Windows.Forms.TextBox fileNameTextBox;
        private System.Windows.Forms.FlowLayoutPanel rootFlowLayout;
        private LineSeparatorControl myLineSeparator;
        private System.Windows.Forms.FlowLayoutPanel buttonsFlowLayout;
        private System.Windows.Forms.Button exitButton;
        private System.Windows.Forms.Button createButton;
        private System.Windows.Forms.Button openButton;
        private System.Windows.Forms.OpenFileDialog myOpenFileDialog;
        private System.Windows.Forms.SaveFileDialog mySaveFileDialog;
        private System.Windows.Forms.ErrorProvider myErrorProvider;
        private System.Windows.Forms.Label errorTextLabel;
        private System.Windows.Forms.CheckBox newSavePasswordCheckBox;
        private System.Windows.Forms.CheckBox savePasswordCheckBox;
    }
}