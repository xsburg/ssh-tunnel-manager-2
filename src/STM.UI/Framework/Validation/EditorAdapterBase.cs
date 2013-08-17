// ***********************************************************************
// <author>Stephan Burguchev</author>
// <copyright company="Stephan Burguchev">
//   Copyright (c) Stephan Burguchev 2012-2013. All rights reserved.
// </copyright>
// <summary>
//   EditorAdapterBase.cs
// </summary>
// ***********************************************************************

using System;
using System.Windows.Forms;

namespace STM.UI.Framework.Validation
{
    public class EditorAdapterBase<T> : IEditorAdapter where T : Control
    {
        private readonly T control;

        public EditorAdapterBase(T control)
        {
            if (control == null)
            {
                throw new ArgumentNullException("control");
            }

            this.control = control;
        }

        public Control Control
        {
            get
            {
                return this.control;
            }
        }

        public virtual object EditValue
        {
            get
            {
                return this.control.Text;
            }
            set
            {
                this.control.Text = value == null
                    ? ""
                    : value.ToString();
            }
        }

        public override bool Equals(object obj)
        {
            if (obj == null || this.GetType() != obj.GetType())
            {
                return false;
            }

            return this.control.Equals(((IEditorAdapter)obj).Control);
        }

        public override int GetHashCode()
        {
            return this.control.GetHashCode();
        }
    }
}
