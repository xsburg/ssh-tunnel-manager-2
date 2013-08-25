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
using System.Collections.Generic;
using System.Linq;
using Ninject.Extensions.Logging;
using STM.Core;
using STM.Core.Data;
using STM.UI.Framework;
using STM.UI.Framework.Mvc;

namespace STM.UI.Controls.ConnectionControl
{
    public class ConnectionControlController : ControllerBase<IConnectionControl>, IConnectionManagerObserver
    {
        private readonly ConnectionManager connectionManager;
        private readonly IEncryptedStorage storage;

        private readonly List<ConnectionViewModel> viewModelsCache = new List<ConnectionViewModel>();
        private ConnectionViewModel viewModel;

        public ConnectionControlController(
            IEncryptedStorage storage,
            ConnectionManager connectionManager,
            ILogger logger,
            IMessageBoxService messageBoxService,
            IStandardDialogService standardDialogService,
            IEventAggregator eventAggregator = null)
            : base(logger, messageBoxService, standardDialogService, eventAggregator)
        {
            if (storage == null)
            {
                throw new ArgumentNullException("storage");
            }

            if (connectionManager == null)
            {
                throw new ArgumentNullException("connectionManager");
            }

            this.storage = storage;
            this.connectionManager = connectionManager;

            this.connectionManager.AddObserver(this);
        }

        public void Load(string connectionName)
        {
            var connection = this.storage.Content.FindConnection(connectionName);
            this.viewModel = this.viewModelsCache.FirstOrDefault(c => c.Info.Equals(connection));
            if (this.viewModel == null)
            {
                this.viewModelsCache.Add(this.viewModel = new ConnectionViewModel(connection));
            }

            this.View.Render(this.viewModel);
        }

        void IConnectionManagerObserver.HandleFatalError(ConnectionInfo sender, string errorMessage)
        {
            if (!this.IsActiveConnection(sender))
            {
                return;
            }

            throw new NotImplementedException();
        }

        void IConnectionManagerObserver.HandleForwardingError(ConnectionInfo sender, TunnelInfo tunnel, string errorMessage)
        {
            if (!this.IsActiveConnection(sender))
            {
                return;
            }

            throw new NotImplementedException();
        }

        void IConnectionManagerObserver.HandleMessage(ConnectionInfo sender, MessageSeverity severity, string message)
        {
            if (!this.IsActiveConnection(sender))
            {
                return;
            }

            throw new NotImplementedException();
        }

        void IConnectionManagerObserver.HandleStateChanged(ConnectionInfo sender, ConnectionState state)
        {
            if (!this.IsActiveConnection(sender))
            {
                return;
            }

            this.View.Render(state);
        }

        private bool IsActiveConnection(ConnectionInfo sender)
        {
            return this.viewModel == null || !sender.Equals(this.viewModel.Info);
        }
    }
}
