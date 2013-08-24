// ***********************************************************************
// <author>Stephan Burguchev</author>
// <copyright company="Stephan Burguchev">
//   Copyright (c) Stephan Burguchev 2012-2013. All rights reserved.
// </copyright>
// <summary>
//   CompareToValidationRule.cs
// </summary>
// ***********************************************************************

using System;
using System.Windows.Forms;

namespace STM.UI.Framework.Validation.Rules
{
    public class CompareToValidationRule : ValidationRule
    {
        private readonly string errorText;
        private readonly IEditorAdapter adapter;

        public CompareToValidationRule(Control control, string errorText)
        {
            if (control == null)
            {
                throw new ArgumentNullException("control");
            }

            if (errorText == null)
            {
                throw new ArgumentNullException("errorText");
            }

            this.errorText = errorText;
            this.Control = control;
            this.adapter = EditorAdapterFactory.Create(control);
        }

        public Control Control { get; private set; }

        public override bool Validate(object value)
        {
            var result = Equals(this.adapter.EditValue, value);
            this.ErrorText = !result
                ? errorText
                : "";
            return result;
        }
    }
}
