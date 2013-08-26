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
using STM.Core.Util;
using STM.UI.Annotations;
using STM.UI.Controls.ConnectionControl;
using STM.UI.Framework;
using STM.UI.Framework.Mvc;

namespace STM.UI.Forms.MainForm
{
    public class MainFormController : ControllerBase<IMainForm>
    {
        private readonly ConnectionManager connectionManager;
        private readonly ApplicationLauncher appLauncher;
        private readonly IEncryptedStorage storage;
        private ConnectionControlController connectionController;
        private BindingList<ConnectionViewModel> connections;

        public MainFormController(
            IEncryptedStorage storage,
            [NotNull] ConnectionManager connectionManager,
            [NotNull] ApplicationLauncher appLauncher,
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

            if (appLauncher == null)
            {
                throw new ArgumentNullException("appLauncher");
            }

            this.storage = storage;
            this.connectionManager = connectionManager;
            this.appLauncher = appLauncher;
        }

        public bool IsModified { get; private set; }
        public ConnectionViewModel SelectedConnection { get; private set; }

        public void CloseConnection()
        {
            if (this.SelectedConnection == null)
            {
                return;
            }

            this.connectionManager.Close(this.SelectedConnection.Info);
        }

        public void DisplayAboutDialog()
        {
            throw new NotImplementedException();
        }

        public void DisplayAddConnectionDialog()
        {
            throw new NotImplementedException();
        }

        public void DisplayChangePasswordDialog()
        {
            throw new NotImplementedException();
        }

        public void DisplayChangeStorageDialog()
        {
            throw new NotImplementedException();
        }

        public void DisplayEditConnectionDialog()
        {
            if (this.SelectedConnection == null)
            {
                return;
            }

            throw new NotImplementedException();
        }

        public void DisplaySettingsDialog()
        {
            throw new NotImplementedException();
        }

        public void Exit()
        {
            this.connectionManager.CloseAll();
            this.View.Close(true);
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

        public void OpenConnection()
        {
            if (this.SelectedConnection == null)
            {
                return;
            }

            this.connectionManager.Open(this.SelectedConnection.Info);
        }

        public void Register(ConnectionControlController controller)
        {
            this.connectionController = controller;
        }

        public void RemoveConnection()
        {
            if (this.SelectedConnection == null)
            {
                return;
            }

            var message = string.Format("Are you sure you want to remove connection '{0}'?", this.SelectedConnection.Name);
            if (!this.MessageBoxService.AskYesNo(message))
            {
                return;
            }

            this.connectionManager.Close(this.SelectedConnection.Info);
            this.connections.Remove(this.SelectedConnection);
            this.storage.Content.Connections.Remove(this.SelectedConnection.Info);
        }

        public void Save()
        {
            this.storage.Save();
            this.IsModified = false;
        }

        public void SelectConnection(ConnectionViewModel viewModel)
        {
            this.connectionController.Load(viewModel);
            this.SelectedConnection = viewModel;
            this.UpdateActions();
        }

        public void StartFileZilla()
        {
            if (this.SelectedConnection == null)
            {
                return;
            }

            appLauncher.StartFileZilla(this.SelectedConnection.Info);
        }

        public void StartPsftp()
        {
            if (this.SelectedConnection == null)
            {
                return;
            }

            appLauncher.StartPsftp(this.SelectedConnection.Info);
        }

        public void StartPutty()
        {
            if (this.SelectedConnection == null)
            {
                return;
            }

            appLauncher.StartPutty(this.SelectedConnection.Info);
        }

        private void UpdateActions()
        {
            var canOpenConnection = this.SelectedConnection != null && this.SelectedConnection.State == ConnectionState.Closed;
            var canClose = !canOpenConnection;
            var canEditConnectionInfo = canOpenConnection;
            this.View.UpdateActionState(
                new MainFormActionsViewModel
                    {
                        CanOpenConnection = canOpenConnection,
                        CanCloseConnection = canClose,
                        CanEditConnectionInfo = canEditConnectionInfo,
                        CanSave = this.IsModified
                    });
        }
    }
}
