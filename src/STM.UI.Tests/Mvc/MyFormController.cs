// ***********************************************************************
// <author>Stephan Burguchev</author>
// <copyright company="Stephan Burguchev">
//   Copyright (c) Stephan Burguchev 2012-2013. All rights reserved.
// </copyright>
// <summary>
//   MyFormController.cs
// </summary>
// ***********************************************************************

using STM.UI.Framework.Mvc;

namespace STM.UI.Tests.Mvc
{
    public class MyFormController : ControllerBase<IMyForm>
    {
        public MyControlController MyControlController { get; private set; }

        public void Register(MyControlController controller)
        {
            this.MyControlController = controller;
        }
    }
}
