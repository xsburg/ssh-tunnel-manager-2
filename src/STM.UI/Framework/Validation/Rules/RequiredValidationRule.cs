// ***********************************************************************
// <author>Stephan Burguchev</author>
// <copyright company="Stephan Burguchev">
//   Copyright (c) Stephan Burguchev 2012-2013. All rights reserved.
// </copyright>
// <summary>
//   RequiredValidationRule.cs
// </summary>
// ***********************************************************************

namespace STM.UI.Framework.Validation.Rules
{
    public class RequiredValidationRule : ValidationRule
    {
        public override bool Validate(object value)
        {
            var text = value as string;
            if (text == null || text.Trim() == "")
            {
                this.ErrorText = "The field is required";
                return false;
            }

            return true;
        }
    }
}
