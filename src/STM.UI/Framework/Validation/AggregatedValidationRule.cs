// ***********************************************************************
// <author>Stephan Burguchev</author>
// <copyright company="Stephan Burguchev">
//   Copyright (c) Stephan Burguchev 2012-2013. All rights reserved.
// </copyright>
// <summary>
//   AggregatedValidationRule.cs
// </summary>
// ***********************************************************************

using System;
using System.Linq;

namespace STM.UI.Framework.Validation
{
    public class AggregatedValidationRule : ValidationRule
    {
        private readonly AggregatedValidationMode mode;

        public AggregatedValidationRule(AggregatedValidationMode mode, params ValidationRule[] rules)
        {
            if (rules == null)
            {
                throw new ArgumentNullException("rules");
            }

            this.mode = mode;
            this.Rules = rules;
        }

        public ValidationRule[] Rules { get; private set; }

        public override bool Validate(object value)
        {
            this.ErrorText = "";
            var result = true;
            foreach (ValidationRule rule in this.Rules)
            {
                result = rule.Validate(value) && result;
                if (!result && this.mode == AggregatedValidationMode.FirstFailed)
                {
                    this.ErrorText = rule.ErrorText;
                    return false;
                }
            }

            if (!result)
            {
                this.ErrorText = string.Join(Environment.NewLine, this.Rules.Select(r => "- " + r.ErrorText));
            }

            return result;
        }
    }
}
