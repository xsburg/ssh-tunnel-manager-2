// ***********************************************************************
// <author>Stephan Burguchev</author>
// <copyright company="Stephan Burguchev">
//   Copyright (c) Stephan Burguchev 2012-2013. All rights reserved.
// </copyright>
// <summary>
//   PasswordValidationRule.cs
// </summary>
// ***********************************************************************

using System.Text.RegularExpressions;

namespace STM.UI.Framework.Validation.Rules
{
    public class PasswordValidationRule : AggregatedValidationRule
    {
        private static readonly Regex InvalidSymbolsRegex = new Regex(@"\s", RegexOptions.Compiled);

        public PasswordValidationRule()
            : base(
                AggregatedValidationMode.FirstFailed,
                new RequiredValidationRule(),
                new RegexValidationRule(InvalidSymbolsRegex, "The password field contains invalid symbols"))
        {
        }
    }
}
