// ***********************************************************************
// <author>Stephan Burguchev</author>
// <copyright company="Stephan Burguchev">
//   Copyright (c) Stephan Burguchev 2012-2013. All rights reserved.
// </copyright>
// <summary>
//   PortNumberValidationRule.cs
// </summary>
// ***********************************************************************

using System.Text.RegularExpressions;

namespace STM.UI.Framework.Validation.Rules
{
    public class PortNumberValidationRule : RegexValidationRule
    {
        private static readonly string PortNumberPattern = string.Format(@"^{0}$", RegexHelper.PortNumberPattern);

        private static readonly Regex PortNumberRegex = new Regex(
            PortNumberPattern,
            RegexOptions.Singleline | RegexOptions.IgnoreCase | RegexOptions.Compiled);

        public PortNumberValidationRule()
            : base(PortNumberRegex, "Invalid port number. A valid port number ranges from 0 to 65535")
        {
        }

        public static readonly PortNumberValidationRule Instance = new PortNumberValidationRule();
    }
}
