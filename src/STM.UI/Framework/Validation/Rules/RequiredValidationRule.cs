// ***********************************************************************
// <author>Stephan Burguchev</author>
// <copyright company="Stephan Burguchev">
//   Copyright (c) Stephan Burguchev 2012-2014. All rights reserved.
// </copyright>
// <summary>
//   RequiredValidationRule.cs
// </summary>
// ***********************************************************************

namespace STM.UI.Framework.Validation.Rules
{
    public class RequiredValidationRule : ValidationRule
    {
        private readonly string errorText;

        public RequiredValidationRule(string errorText = null)
        {
            this.errorText = errorText ?? "The field is required";
        }

        public override bool Validate(object value)
        {
            var text = value as string;
            if (text == null || text.Trim() == "")
            {
                this.ErrorText = this.errorText;
                return false;
            }

            return true;
        }

        public static readonly RequiredValidationRule Instance = new RequiredValidationRule();
    }
}
