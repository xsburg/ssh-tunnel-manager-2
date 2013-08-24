// ***********************************************************************
// <author>Stephan Burguchev</author>
// <copyright company="Stephan Burguchev">
//   Copyright (c) Stephan Burguchev 2012-2013. All rights reserved.
// </copyright>
// <summary>
//   ConnectionControlController.cs
// </summary>
// ***********************************************************************

using Ninject.Extensions.Logging;
using STM.UI.Framework;
using STM.UI.Framework.Mvc;

namespace STM.UI.Controls.ConnectionControl
{
    public class ConnectionControlController : ControllerBase<IConnectionControl>
    {
        public ConnectionControlController(
            ILogger logger,
            IMessageBoxService messageBoxService,
            IStandardDialogService standardDialogService,
            IEventAggregator eventAggregator = null)
            : base(logger, messageBoxService, standardDialogService, eventAggregator)
        {
        }
    }
}
