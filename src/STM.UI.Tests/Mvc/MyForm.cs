// ***********************************************************************
// <author>Stephan Burguchev</author>
// <copyright company="Stephan Burguchev">
//   Copyright (c) Stephan Burguchev 2012-2013. All rights reserved.
// </copyright>
// <summary>
//   MyForm.cs
// </summary>
// ***********************************************************************

using System;

namespace STM.UI.Tests.Mvc
{
    public class MyForm : IMyForm
    {
        private MyControl myControl;

        public MyForm(MyFormController controller, MyControl myControl)
        {
            this.Controller = controller;

            this.myControl = myControl;

            controller.Register(myControl.Controller);
            controller.Register(this);
        }

        public MyFormController Controller { get; private set; }

        public bool IsDisposed { get; private set; }

        public void Close()
        {
            this.Dispose();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public void Show()
        {
            throw new NotImplementedException();
        }
    }
}
