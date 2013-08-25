// ***********************************************************************
// <author>Stephan Burguchev</author>
// <copyright company="Stephan Burguchev">
//   Copyright (c) Stephan Burguchev 2012-2013. All rights reserved.
// </copyright>
// <summary>
//   MainFormController.cs
// </summary>
// ***********************************************************************

using System;
using System.ComponentModel;
using System.Linq;
using Ninject.Extensions.Logging;
using STM.Core;
using STM.Core.Data;
using STM.Core.Util;
using STM.UI.Annotations;
using STM.UI.Controls.ConnectionControl;
using STM.UI.Framework;
using STM.UI.Framework.Mvc;

namespace STM.UI.Forms.MainForm
{
    public class MainFormController : ControllerBase<IMainForm>
    {
        private readonly IEncryptedStorage storage;
        private readonly ConnectionManager connectionManager;
        private ConnectionControlController connectionController;
        private BindingList<ConnectionViewModel> connections;

        public MainFormController(
            IEncryptedStorage storage,
            [NotNull] ConnectionManager connectionManager,
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
        }

        public void Register(ConnectionControlController controller)
        {
            this.connectionController = controller;
        }

        public void Load()
        {
            this.storage.Read();

            /*this.storage.Content.Connections.Add(
                new ConnectionInfo
                    {
                        Name = "name2",
                        UserName = "usr name2",
                        HostName = "hostname2.com",
                        Port = 12345,
                        Tunnels =
                            {
                                new TunnelInfo
                                    {
                                        Name = "some tunne2l",
                                        Type = TunnelType.Local,
                                        LocalPort = 123,
                                        RemotePort = 3112,
                                        RemoteHostName = "rem2ote.com"
                                    },
                                new TunnelInfo
                                    {
                                        Name = "zsome tunneaa2l",
                                        Type = TunnelType.Remote,
                                        LocalPort = 2123,
                                        RemotePort = 33112,
                                        RemoteHostName = "rem2ote.com"
                                    }
                            }
                    });
            this.storage.Save();*/

            this.connections = new BindingList<ConnectionViewModel>(
                this.storage.Content.Connections.Select(c => new ConnectionViewModel(c)).ToList());
            this.connections.Apply(c => c.State = this.connectionManager.GetState(c.Info));
            this.View.Render(this.connections);
        }

        public void OpenConnection(ConnectionViewModel viewModel)
        {
            this.connectionManager.Open(viewModel.Info);
        }

        public void SelectConnection(ConnectionViewModel viewModel)
        {
            this.connectionController.Load(viewModel);
        }
    }
}
