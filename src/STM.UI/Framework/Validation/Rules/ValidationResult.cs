// ***********************************************************************
// <author>Stephan Burguchev</author>
// <copyright company="Stephan Burguchev">
//   Copyright (c) Stephan Burguchev 2012-2014. All rights reserved.
// </copyright>
// <summary>
//   ValidationResult.cs
// </summary>
// ***********************************************************************

namespace STM.UI.Framework.Validation.Rules
{
    public class ValidationResult
    {
        private ValidationResult(bool isValid, string errorText)
        {
            this.IsValid = isValid;
            this.ErrorText = errorText;
        }

        public string ErrorText { get; private set; }
        public bool IsValid { get; private set; }

        public static ValidationResult Fail(string errorText)
        {
            return new ValidationResult(false, errorText);
        }

        public static ValidationResult Success()
        {
            return new ValidationResult(true, "");
        }
    }
}
