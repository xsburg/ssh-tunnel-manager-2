// ***********************************************************************
// <author>Stephan Burguchev</author>
// <copyright company="Stephan Burguchev">
//   Copyright (c) Stephan Burguchev 2012-2013. All rights reserved.
// </copyright>
// <summary>
//   MainFormController.cs
// </summary>
// ***********************************************************************

using Ninject.Extensions.Logging;
using STM.UI.Controls.ConnectionControl;
using STM.UI.Framework;
using STM.UI.Framework.Mvc;

namespace STM.UI.Forms.MainForm
{
    public class MainFormController : ControllerBase<IMainForm>
    {
        private ConnectionControlController connectionController;

        public MainFormController(
            ILogger logger,
            IMessageBoxService messageBoxService,
            IStandardDialogService standardDialogService,
            IEventAggregator eventAggregator = null)
            : base(logger, messageBoxService, standardDialogService, eventAggregator)
        {
        }

        public void Register(ConnectionControlController controller)
        {
            this.connectionController = controller;
        }
    }
}
