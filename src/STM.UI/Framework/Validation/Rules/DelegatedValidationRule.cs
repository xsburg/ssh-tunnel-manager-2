// ***********************************************************************
// <author>Stephan Burguchev</author>
// <copyright company="Stephan Burguchev">
//   Copyright (c) Stephan Burguchev 2012-2014. All rights reserved.
// </copyright>
// <summary>
//   DelegatedValidationRule.cs
// </summary>
// ***********************************************************************

using System;
using STM.UI.Annotations;

namespace STM.UI.Framework.Validation.Rules
{
    public class DelegatedValidationRule<T> : ValidationRule
    {
        private readonly Func<T, ValidationResult> validationFunc;

        public DelegatedValidationRule([NotNull] Func<T, ValidationResult> validationFunc)
        {
            if (validationFunc == null)
            {
                throw new ArgumentNullException("validationFunc");
            }

            this.validationFunc = validationFunc;
        }

        public override bool Validate(object value)
        {
            var result = this.validationFunc((T)value);
            if (!result.IsValid)
            {
                this.ErrorText = result.ErrorText;
            }

            return result.IsValid;
        }
    }
}
