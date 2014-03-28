// ***********************************************************************
// <author>Stephan Burguchev</author>
// <copyright company="Stephan Burguchev">
//   Copyright (c) Stephan Burguchev 2012-2014. All rights reserved.
// </copyright>
// <summary>
//   ConnectionFormController.cs
// </summary>
// ***********************************************************************

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Ninject.Extensions.Logging;
using STM.Core;
using STM.Core.Data;
using STM.UI.Annotations;
using STM.UI.Framework;
using STM.UI.Framework.Mvc;

namespace STM.UI.Forms.Connection
{
    public class ConnectionFormController : ControllerBase<IConnectionForm>
    {
        private readonly IEncryptedStorage storage;
        private IEnumerable<ConnectionInfo> connections;

        public ConnectionFormController(
            IEncryptedStorage storage,
            ILogger logger,
            IMessageBoxService messageBoxService,
            IStandardDialogService standardDialogService,
            IEventAggregator eventAggregator = null)
            : base(logger, messageBoxService, standardDialogService, eventAggregator)
        {
            this.storage = storage;
        }

        public ConnectionInfo Connection { get; set; }

        public string LoadedPrivateKeyData { get; private set; }

        public void Apply()
        {
            this.View.Collect(this.Connection);
            this.Connection.PrivateKeyData = this.LoadedPrivateKeyData;
        }

        public void Create()
        {
            if (!this.View.ValidateConnection())
            {
                return;
            }

            if (!this.EnsureActiveTunnelIsUseless())
            {
                return;
            }

            this.View.Collect(this.Connection);
            this.Connection.PrivateKeyData = this.LoadedPrivateKeyData;

            this.View.Close(true);
        }

        public void Load([NotNull] IEnumerable<ConnectionInfo> allConnections)
        {
            this.connections = allConnections.ToArray();
            if (allConnections == null)
            {
                throw new ArgumentNullException("allConnections");
            }

            var proxyList = (IEnumerable<SharedConnectionSettings>)this.storage.Data.SharedSettings;
            this.View.Render(this.connections, proxyList, this.Connection);
            this.LoadedPrivateKeyData = this.Connection.PrivateKeyData;
        }

        public void LoadPrivateKey()
        {
            var fileName = this.StandardDialogService.ShowOpenFileDialog("PuTTY Private Key Files|*.ppk|All Files|*.*");
            if (fileName == null)
            {
                return;
            }

            this.LoadedPrivateKeyData = File.ReadAllText(fileName, Encoding.ASCII);
            this.View.RenderPrivateKeyFileName(fileName);
        }

        public void Ok()
        {
            if (!this.EnsureActiveTunnelIsUseless())
            {
                return;
            }

            this.Apply();
            this.View.Close(true);
        }

        private bool EnsureActiveTunnelIsUseless()
        {
            if (!this.View.ValidateTunnel())
            {
                return true;
            }

            switch (
                this.MessageBoxService.AskYesNoCancel(
                    "You might have forgotten to press the 'Add' button. Do you want to add the new tunnel into the list?"))
            {
            case true:
                this.View.AddTunnel();
                return true;
            case false:
                this.View.ResetAddTunnelGroup();
                return true;
            case null:
                // Go back and change something
                return false;
            }

            return true;
        }
    }
}
