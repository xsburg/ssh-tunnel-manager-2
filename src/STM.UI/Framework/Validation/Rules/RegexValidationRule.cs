// ***********************************************************************
// <author>Stephan Burguchev</author>
// <copyright company="Stephan Burguchev">
//   Copyright (c) Stephan Burguchev 2012-2013. All rights reserved.
// </copyright>
// <summary>
//   RegexValidationRule.cs
// </summary>
// ***********************************************************************

using System;
using System.Text.RegularExpressions;

namespace STM.UI.Framework.Validation.Rules
{
    public class RegexValidationRule : ValidationRule
    {
        private readonly string errorText;
        private readonly Regex regex;

        public RegexValidationRule(Regex regex, string errorText)
        {
            if (regex == null)
            {
                throw new ArgumentNullException("regex");
            }

            if (errorText == null)
            {
                throw new ArgumentNullException("errorText");
            }

            this.regex = regex;
            this.errorText = errorText;
        }

        public override bool Validate(object value)
        {
            var text = value as string;
            var result = text == null || !this.regex.IsMatch(text);
            this.ErrorText = result
                ? this.errorText
                : "";
            return !result;
        }
    }
}
