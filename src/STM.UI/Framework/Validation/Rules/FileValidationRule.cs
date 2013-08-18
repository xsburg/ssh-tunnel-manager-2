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
            if (!this.requiredRule.Validate(value))
            {
                return false;
            }

            if (this.IsNew)
            {
                return true;
            }

            var text = value as string;
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
