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
using STM.UI.Forms.ChangePassword;
using STM.UI.Forms.Connection;
using STM.UI.Framework;
using STM.UI.Framework.Mvc;

namespace STM.UI.Forms.Main
{
    public class MainFormController : ControllerBase<IMainForm>
    {
        private readonly ApplicationLauncher appLauncher;
        private readonly ConnectionManager connectionManager;
        private readonly ExceptionHandler exceptionHandler;
        private readonly IEncryptedStorage storage;
        private readonly IWindowManager windowManager;
        private ConnectionControlController connectionController;
        private BindingList<ConnectionViewModel> connections;

        public MainFormController(
            IEncryptedStorage storage,
            [NotNull] ConnectionManager connectionManager,
            [NotNull] ApplicationLauncher appLauncher,
            [NotNull] IWindowManager windowManager,
            [NotNull] ExceptionHandler exceptionHandler,
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

            if (windowManager == null)
            {
                throw new ArgumentNullException("windowManager");
            }
            if (exceptionHandler == null)
            {
                throw new ArgumentNullException("exceptionHandler");
            }

            this.storage = storage;
            this.connectionManager = connectionManager;
            this.appLauncher = appLauncher;
            this.windowManager = windowManager;
            this.exceptionHandler = exceptionHandler;
        }

        public bool DisplayStorageSelectionAfterExit { get; private set; }

        public bool IsModified { get; private set; }

        public ConnectionViewModel SelectedConnection { get; private set; }

        public void CloseConnection()
        {
            if (this.SelectedConnection == null)
            {
                return;
            }

            try
            {
                this.connectionManager.Close(this.SelectedConnection.Info);
            }
            catch (Exception ex)
            {
                this.exceptionHandler.HandleException(ex);
            }
        }

        public void DisplayAboutDialog()
        {
            throw new NotImplementedException();
        }

        public void DisplayNewConnectionDialog()
        {
            var form = this.windowManager.CreateView<IConnectionForm>();
            form.Controller.Load(this.connections.Select(c => c.Info));
            if (form.ShowDialog() == true)
            {
                var c = form.Controller.Connection;
                this.storage.Data.Connections.Add(c);
                this.connections.Add(new ConnectionViewModel(c));
                // do something to display it
                //this.View.Render(this.connections);
            }
        }

        public void DisplayChangePasswordDialog()
        {
            var form = this.windowManager.CreateView<IChangePasswordForm>();
            if (form.ShowDialog() == true)
            {
                this.IsModified = false;
                this.UpdateActions();
            }
        }

        public void DisplayChangeStorageDialog()
        {
            this.DisplayStorageSelectionAfterExit = true;
            this.View.Close(true);
        }

        public void DisplayEditConnectionDialog()
        {
            if (this.SelectedConnection == null)
            {
                return;
            }

            var form = this.windowManager.CreateView<IConnectionForm>();
            form.Controller.Connection = this.SelectedConnection.Info;
            if (form.ShowDialog() == true)
            {
                var cvm = this.connections.First(c => c.Info.Equals(form.Controller.Connection));
                this.View.Render(cvm);
            }
        }

        public void DisplaySettingsDialog()
        {
            throw new NotImplementedException();
        }

        public void Exit()
        {
            try
            {
                this.connectionManager.CloseAll();
                this.View.Close(true);
            }
            catch (Exception ex)
            {
                this.exceptionHandler.HandleException(ex);
            }
        }

        public void Load()
        {
            try
            {
                this.DisplayStorageSelectionAfterExit = false;
                this.storage.Read();

                this.connections = new BindingList<ConnectionViewModel>(
                    this.storage.Data.Connections.Select(c => new ConnectionViewModel(c)).ToList());
                this.connections.Apply(c => c.State = this.connectionManager.GetState(c.Info));
                this.View.Render(this.connections);
            }
            catch (Exception ex)
            {
                this.exceptionHandler.HandleException(ex);
            }
        }

        public void OpenConnection()
        {
            if (this.SelectedConnection == null)
            {
                return;
            }

            try
            {
                this.connectionManager.Open(this.SelectedConnection.Info);
            }
            catch (Exception ex)
            {
                this.exceptionHandler.HandleException(ex);
            }
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

            try
            {
                this.connectionManager.Close(this.SelectedConnection.Info);
                this.connections.Remove(this.SelectedConnection);
                this.storage.Data.Connections.Remove(this.SelectedConnection.Info);
            }
            catch (Exception ex)
            {
                this.exceptionHandler.HandleException(ex);
            }
        }

        public void Save()
        {
            try
            {
                this.storage.Save();
                this.IsModified = false;
            }
            catch (Exception ex)
            {
                this.exceptionHandler.HandleException(ex);
            }
        }

        public void SelectConnection(ConnectionViewModel viewModel)
        {
            try
            {
                this.connectionController.Load(viewModel);
                this.SelectedConnection = viewModel;
                this.UpdateActions();
            }
            catch (Exception ex)
            {
                this.exceptionHandler.HandleException(ex);
            }
        }

        public void StartFileZilla()
        {
            if (this.SelectedConnection == null)
            {
                return;
            }

            try
            {
                this.appLauncher.StartFileZilla(this.SelectedConnection.Info);
            }
            catch (Exception ex)
            {
                this.exceptionHandler.HandleException(ex);
            }
        }

        public void StartPsftp()
        {
            if (this.SelectedConnection == null)
            {
                return;
            }

            try
            {
                this.appLauncher.StartPsftp(this.SelectedConnection.Info);
            }
            catch (Exception ex)
            {
                this.exceptionHandler.HandleException(ex);
            }
        }

        public void StartPutty()
        {
            if (this.SelectedConnection == null)
            {
                return;
            }

            try
            {
                this.appLauncher.StartPutty(this.SelectedConnection.Info);
            }
            catch (Exception ex)
            {
                this.exceptionHandler.HandleException(ex);
            }
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
