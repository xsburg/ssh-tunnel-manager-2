// ***********************************************************************
// <author>Stephan Burguchev</author>
// <copyright company="Stephan Burguchev">
//   Copyright (c) Stephan Burguchev 2012-2013. All rights reserved.
// </copyright>
// <summary>
//   MyControl.cs
// </summary>
// ***********************************************************************

using System;

namespace STM.UI.Tests.Mvc
{
    public class MyControl : IMyControl
    {
        public MyControl(MyControlController controller)
        {
            if (controller == null)
            {
                throw new ArgumentNullException("controller");
            }

            this.Controller = controller;
            this.Controller.Register(this);
        }

        public MyControlController Controller { get; private set; }

        public bool IsDisposed { get; private set; }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
