// ***********************************************************************
// <author>Stephan Burguchev</author>
// <copyright company="Stephan Burguchev">
//   Copyright (c) Stephan Burguchev 2012-2013. All rights reserved.
// </copyright>
// <summary>
//   IChangePasswordForm.cs
// </summary>
// ***********************************************************************


using STM.UI.Framework.Mvc;

namespace STM.UI.Forms.ChangePassword
{
    public interface IChangePasswordForm : IDialog<ChangePasswordFormController>
    {
        string Collect();
        bool DoValidate();
    }
}
