// ***********************************************************************
// <author>Stephan Burguchev</author>
// <copyright company="Stephan Burguchev">
//   Copyright (c) Stephan Burguchev 2012-2013. All rights reserved.
// </copyright>
// <summary>
//   IWindowManager.cs
// </summary>
// ***********************************************************************

using STM.UI.Framework.Mvc;

namespace STM.UI.Framework
{
    public interface IWindowManager
    {
        TViewInterface CreateView<TViewInterface>() where TViewInterface : IView;
    }
}
