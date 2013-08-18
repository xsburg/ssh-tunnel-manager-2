// ***********************************************************************
// <author>Stephan Burguchev</author>
// <copyright company="Stephan Burguchev">
//   Copyright (c) Stephan Burguchev 2012-2013. All rights reserved.
// </copyright>
// <summary>
//   HostNameAndPortNumberValidationRule.cs
// </summary>
// ***********************************************************************

using System.Text.RegularExpressions;

namespace STM.UI.Framework.Validation.Rules
{
    public class HostNameAndPortNumberValidationRule : RegexValidationRule
    {
        private static readonly string HostNameAndPortNumberPattern = string.Format(
            @"^(?:{0}|{1}):{2}$",
            RegexHelper.HostNamePattern,
            RegexHelper.Ip4AddressPattern,
            RegexHelper.PortNumberPattern);

        private static readonly Regex HostNameAndPortNumberRegex = new Regex(
            HostNameAndPortNumberPattern,
            RegexOptions.Singleline | RegexOptions.IgnoreCase | RegexOptions.Compiled);

        public HostNameAndPortNumberValidationRule()
            : base(HostNameAndPortNumberRegex, "Invalid host name format")
        {
        }
    }
}
