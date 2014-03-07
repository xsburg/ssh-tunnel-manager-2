// ***********************************************************************
// <author>Stephan Burguchev</author>
// <copyright company="Stephan Burguchev">
//   Copyright (c) Stephan Burguchev 2012-2014. All rights reserved.
// </copyright>
// <summary>
//   LabelEditorAdapter.cs
// </summary>
// ***********************************************************************

using System.Windows.Forms;

namespace STM.UI.Framework.Validation
{
    [EditorAdapter(typeof(Label))]
    public class LabelEditorAdapter : EditorAdapterBase<Label>
    {
        public LabelEditorAdapter(Label control) : base(control)
        {
        }
    }
}
