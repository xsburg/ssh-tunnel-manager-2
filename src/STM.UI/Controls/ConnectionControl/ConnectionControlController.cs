// ***********************************************************************
// <author>Stephan Burguchev</author>
// <copyright company="Stephan Burguchev">
//   Copyright (c) Stephan Burguchev 2012-2013. All rights reserved.
// </copyright>
// <summary>
//   ConnectionControlController.cs
// </summary>
// ***********************************************************************

using System;
using Ninject.Extensions.Logging;
using STM.Core;
using STM.Core.Data;
using STM.UI.Annotations;
using STM.UI.Framework;
using STM.UI.Framework.Mvc;

namespace STM.UI.Controls.ConnectionControl
{
    public class ConnectionControlController : ControllerBase<IConnectionControl>, IConnectionManagerObserver
    {
        private ConnectionViewModel viewModel;

        public ConnectionControlController(
            ConnectionManager connectionManager,
            ILogger logger,
            IMessageBoxService messageBoxService,
            IStandardDialogService standardDialogService,
            IEventAggregator eventAggregator = null)
            : base(logger, messageBoxService, standardDialogService, eventAggregator)
        {
            if (connectionManager == null)
            {
                throw new ArgumentNullException("connectionManager");
            }

            connectionManager.AddObserver(this);
        }

        public void Load([NotNull] ConnectionViewModel connectionViewModel)
        {
            if (connectionViewModel == null)
            {
                throw new ArgumentNullException("connectionViewModel");
            }

            this.viewModel = connectionViewModel;
            this.View.Render(this.viewModel);
        }

        void IConnectionManagerObserver.HandleFatalError(ConnectionInfo sender, string errorMessage)
        {
            if (!this.IsActiveConnection(sender))
            {
                return;
            }

            this.viewModel.AddLogMessage(MessageSeverity.Fatal, errorMessage);
            this.View.AddLogMessage(MessageSeverity.Fatal, errorMessage);
        }

        void IConnectionManagerObserver.HandleForwardingError(ConnectionInfo sender, TunnelInfo tunnel, string errorMessage)
        {
            if (!this.IsActiveConnection(sender))
            {
                return;
            }

            var message = string.Format(
                @"Local port {0} forwarding to {1}:{2} failed: {3}",
                tunnel.LocalPort,
                tunnel.RemoteHostName,
                tunnel.RemotePort,
                errorMessage);
            this.viewModel.AddLogMessage(MessageSeverity.Warn, message);
            this.View.AddLogMessage(MessageSeverity.Warn, message);
            this.View.RenderTunnelError(tunnel, errorMessage);
        }

        void IConnectionManagerObserver.HandleMessage(ConnectionInfo sender, MessageSeverity severity, string message)
        {
            if (!this.IsActiveConnection(sender))
            {
                return;
            }

            this.viewModel.AddLogMessage(severity, message);
            this.View.AddLogMessage(severity, message);
        }

        void IConnectionManagerObserver.HandleStateChanged(ConnectionInfo sender, ConnectionState state)
        {
            if (!this.IsActiveConnection(sender))
            {
                return;
            }

            this.viewModel.State = state;
            this.View.RenderState(this.viewModel);
            if (state == ConnectionState.Closed)
            {
                this.View.ResetTunnelErrors();
            }
        }

        private bool IsActiveConnection(ConnectionInfo sender)
        {
            return this.viewModel != null && sender.Equals(this.viewModel.Info);
        }
    }
}
