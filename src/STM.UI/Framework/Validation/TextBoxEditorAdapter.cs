// ***********************************************************************
// <author>Stephan Burguchev</author>
// <copyright company="Stephan Burguchev">
//   Copyright (c) Stephan Burguchev 2012-2013. All rights reserved.
// </copyright>
// <summary>
//   TextBoxEditorAdapter.cs
// </summary>
// ***********************************************************************

using System.Windows.Forms;

namespace STM.UI.Framework.Validation
{
    [EditorAdapter(typeof(TextBox))]
    public class TextBoxEditorAdapter : EditorAdapterBase<TextBox>
    {
        public TextBoxEditorAdapter(TextBox control) : base(control)
        {
        }
    }
}
