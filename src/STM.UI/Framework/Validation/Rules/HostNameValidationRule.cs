// ***********************************************************************
// <author>Stephan Burguchev</author>
// <copyright company="Stephan Burguchev">
//   Copyright (c) Stephan Burguchev 2012-2013. All rights reserved.
// </copyright>
// <summary>
//   HostNameValidationRule.cs
// </summary>
// ***********************************************************************

using System.Text.RegularExpressions;

namespace STM.UI.Framework.Validation.Rules
{
    public class HostNameValidationRule : RegexValidationRule
    {
        private static readonly string HostNamePattern = string.Format(
            @"^(?:{0}|{1})$",
            RegexHelper.HostNamePattern,
            RegexHelper.Ip4AddressPattern);

        private static readonly Regex HostNameRegex = new Regex(
            HostNamePattern,
            RegexOptions.Singleline | RegexOptions.IgnoreCase | RegexOptions.Compiled);

        public HostNameValidationRule()
            : base(HostNameRegex, "Invalid host name format")
        {
        }
    }
}
