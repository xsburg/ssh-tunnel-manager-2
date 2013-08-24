// ***********************************************************************
// <author>Stephan Burguchev</author>
// <copyright company="Stephan Burguchev">
//   Copyright (c) Stephan Burguchev 2012-2013. All rights reserved.
// </copyright>
// <summary>
//   IMainForm.cs
// </summary>
// ***********************************************************************

using STM.UI.Framework.Mvc;

namespace STM.UI.Forms.MainForm
{
    public interface IMainForm : IForm<MainFormController>
    {
        void Render();
    }
}
