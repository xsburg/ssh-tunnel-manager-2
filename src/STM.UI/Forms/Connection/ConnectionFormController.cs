// ***********************************************************************
// <author>Stephan Burguchev</author>
// <copyright company="Stephan Burguchev">
//   Copyright (c) Stephan Burguchev 2012-2013. All rights reserved.
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
using STM.Core.Data;
using STM.UI.Annotations;
using STM.UI.Framework;
using STM.UI.Framework.Mvc;

namespace STM.UI.Forms.Connection
{
    public class ConnectionFormController : ControllerBase<IConnectionForm>
    {
        private IEnumerable<ConnectionInfo> connections;

        public ConnectionFormController(
            ILogger logger,
            IMessageBoxService messageBoxService,
            IStandardDialogService standardDialogService,
            IEventAggregator eventAggregator = null)
            : base(logger, messageBoxService, standardDialogService, eventAggregator)
        {
        }

        public ConnectionInfo Connection { get; set; }

        public void Load([NotNull] IEnumerable<ConnectionInfo> allConnections)
        {
            this.connections = allConnections.ToArray();
            if (allConnections == null)
            {
                throw new ArgumentNullException("allConnections");
            }

            this.View.Render(this.connections, Connection);

            this.LoadedPrivateKeyData = this.Connection != null
                ? this.Connection.PrivateKeyData
                : null;
        }

        public void LoadPrivateKey()
        {
            var fileName = StandardDialogService.ShowOpenFileDialog("PuTTY Private Key Files|*.ppk|All Files|*.*");
            if (fileName == null)
            {
                return;
            }

            this.LoadedPrivateKeyData = File.ReadAllText(fileName, Encoding.ASCII);
            this.View.RenderPrivateKeyFileName(fileName);
        }

        public string LoadedPrivateKeyData { get; private set; }

        public void Apply()
        {
            this.View.Collect(this.Connection);
            this.Connection.PrivateKeyData = this.LoadedPrivateKeyData;
        }

        public void Ok()
        {
            this.Apply();
            this.View.Close(true);
        }
    }
}
