// ***********************************************************************
// <author>Stephan Burguchev</author>
// <copyright company="Stephan Burguchev">
//   Copyright (c) Stephan Burguchev 2012-2013. All rights reserved.
// </copyright>
// <summary>
//   IDialog.cs
// </summary>
// ***********************************************************************

namespace STM.UI.Framework.Mvc
{
    public interface IDialog<TController> : IView<TController> where TController : class
    {
        bool? ShowDialog();
        void Close(bool result);
    }
}
