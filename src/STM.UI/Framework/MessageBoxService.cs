// ***********************************************************************
// <author>Stephan Burguchev</author>
// <copyright company="Stephan Burguchev">
//   Copyright (c) Stephan Burguchev 2012-2013. All rights reserved.
// </copyright>
// <summary>
//   MessageBoxService.cs
// </summary>
// ***********************************************************************

using System.Windows.Forms;
using STM.Core.Util;

namespace STM.UI.Framework
{
    internal class MessageBoxService : IMessageBoxService
    {
        private static string DefaultTitle
        {
            get
            {
                return AssemblyAttributes.AssemblyTitle;
            }
        }

        public bool AskYesNo(string text, string title = null, bool defaultValue = true)
        {
            var result = MessageBox.Show(
                text,
                title ?? DefaultTitle,
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question,
                defaultValue
                    ? MessageBoxDefaultButton.Button1
                    : MessageBoxDefaultButton.Button2);
            return result == DialogResult.Yes;
        }

        public bool? AskYesNoCancel(string text, string title = null, bool? defaultValue = null)
        {
            MessageBoxDefaultButton defaultButton;
            switch (defaultValue)
            {
            case true:
                defaultButton = MessageBoxDefaultButton.Button1;
                break;
            case false:
                defaultButton = MessageBoxDefaultButton.Button2;
                break;
            default:
                defaultButton = MessageBoxDefaultButton.Button3;
                break;
            }

            var result = MessageBox.Show(
                text,
                title ?? DefaultTitle,
                MessageBoxButtons.YesNoCancel,
                MessageBoxIcon.Question,
                defaultButton);
            return result == DialogResult.Yes
                ? true
                : result == DialogResult.No
                    ? (bool?)false
                    : null;
        }

        public void Error(string text, string title = null)
        {
            MessageBox.Show(text, title ?? DefaultTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public void Info(string text, string title = null)
        {
            MessageBox.Show(text, title ?? DefaultTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public void Warn(string text, string title = null)
        {
            MessageBox.Show(text, title ?? DefaultTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }
}
