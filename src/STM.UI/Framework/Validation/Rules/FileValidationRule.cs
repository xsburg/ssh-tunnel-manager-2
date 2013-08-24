// ***********************************************************************
// <author>Stephan Burguchev</author>
// <copyright company="Stephan Burguchev">
//   Copyright (c) Stephan Burguchev 2012-2013. All rights reserved.
// </copyright>
// <summary>
//   FileValidationRule.cs
// </summary>
// ***********************************************************************

using System;
using System.IO;

namespace STM.UI.Framework.Validation.Rules
{
    public class FileValidationRule : ValidationRule
    {
        private readonly RequiredValidationRule requiredRule = new RequiredValidationRule();

        public FileValidationRule(bool isNew)
        {
            this.IsNew = isNew;
        }

        public bool IsNew { get; private set; }

        public override bool Validate(object value)
        {
            this.ErrorText = "";
            if (!this.requiredRule.Validate(value))
            {
                this.ErrorText = this.requiredRule.ErrorText;
                return false;
            }

            var text = value as string;
            if (this.IsNew)
            {
                var isValidPath = Path.IsPathRooted(text);
                if (!isValidPath)
                {
                    ErrorText = "The path is not valid";
                }

                return isValidPath;
            }

            if (!File.Exists(text))
            {
                this.ErrorText = "The file does not exist";
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
                this.ErrorText = e.Message;
                return false;
            }

            return true;
        }
    }
}
