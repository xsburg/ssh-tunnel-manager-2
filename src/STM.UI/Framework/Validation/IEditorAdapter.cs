// ***********************************************************************
// <author>Stephan Burguchev</author>
// <copyright company="Stephan Burguchev">
//   Copyright (c) Stephan Burguchev 2012-2013. All rights reserved.
// </copyright>
// <summary>
//   IEditorAdapter.cs
// </summary>
// ***********************************************************************

using System.Windows.Forms;

namespace STM.UI.Framework.Validation
{
    public interface IEditorAdapter
    {
        Control Control { get; }
        object EditValue { get; set; }
    }
}
