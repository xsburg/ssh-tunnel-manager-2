// ***********************************************************************
// <author>Stephan Burguchev</author>
// <copyright company="Stephan Burguchev">
//   Copyright (c) Stephan Burguchev 2012-2013. All rights reserved.
// </copyright>
// <summary>
//   MyFormController.cs
// </summary>
// ***********************************************************************

using Ninject.Extensions.Logging;
using STM.UI.Framework;
using STM.UI.Framework.Mvc;

namespace STM.UI.Tests.Mvc
{
    public class MyFormController : ControllerBase<IMyForm>
    {
        public MyFormController(
            ILogger logger,
            IMessageBoxService messageBoxService,
            IStandardDialogService standardDialogService,
            IEventAggregator eventAggregator = null)
            : base(logger, messageBoxService, standardDialogService, eventAggregator)
        {
        }

        public MyControlController MyControlController { get; private set; }

        public void Register(MyControlController controller)
        {
            this.MyControlController = controller;
        }
    }
}
