// ***********************************************************************
// <author>Stephan Burguchev</author>
// <copyright company="Stephan Burguchev">
//   Copyright (c) Stephan Burguchev 2012-2013. All rights reserved.
// </copyright>
// <summary>
//   EqualityValidationRule.cs
// </summary>
// ***********************************************************************

using System;
using System.Windows.Forms;

namespace STM.UI.Framework.Validation.Rules
{
    public class EqualityValidationRule : ValidationRule
    {
        private readonly IEditorAdapter adapter;

        public EqualityValidationRule(Control control)
        {
            if (control == null)
            {
                throw new ArgumentNullException("control");
            }

            this.Control = control;
            this.adapter = EditorAdapterFactory.Create(control);
        }

        public Control Control { get; private set; }

        public override bool Validate(object value)
        {
            return Equals(this.adapter.EditValue, value);
        }
    }
}
