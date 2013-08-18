// ***********************************************************************
// <author>Stephan Burguchev</author>
// <copyright company="Stephan Burguchev">
//   Copyright (c) Stephan Burguchev 2012-2013. All rights reserved.
// </copyright>
// <summary>
//   WindowManager.cs
// </summary>
// ***********************************************************************

using STM.Core.Util;
using STM.UI.Framework.Mvc;

namespace STM.UI.Framework
{
    public class WindowManager : IWindowManager
    {
        public TViewInterface CreateView<TViewInterface>() where TViewInterface : IView
        {
            var view = IoC.Get<TViewInterface>();
            return view;
        }
    }
}
