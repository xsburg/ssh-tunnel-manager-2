// ***********************************************************************
// <author>Stephan Burguchev</author>
// <copyright company="Stephan Burguchev">
//   Copyright (c) Stephan Burguchev 2012-2013. All rights reserved.
// </copyright>
// <summary>
//   IMainForm.cs
// </summary>
// ***********************************************************************

using System.Collections.Generic;
using System.ComponentModel;
using STM.Core.Data;
using STM.UI.Controls.ConnectionControl;
using STM.UI.Framework.Mvc;

namespace STM.UI.Forms.MainForm
{
    public interface IMainForm : IForm<MainFormController>
    {
        void Render(IList<ConnectionViewModel> connections);
        void UpdateActionState(MainFormActionsViewModel viewModel);
        void Close(bool dontMinimize);
    }
}
