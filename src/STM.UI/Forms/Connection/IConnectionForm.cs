// ***********************************************************************
// <author>Stephan Burguchev</author>
// <copyright company="Stephan Burguchev">
//   Copyright (c) Stephan Burguchev 2012-2013. All rights reserved.
// </copyright>
// <summary>
//   IConnectionForm.cs
// </summary>
// ***********************************************************************

using System.Collections.Generic;
using STM.Core.Data;
using STM.UI.Framework.Mvc;

namespace STM.UI.Forms.Connection
{
    public interface IConnectionForm : IDialog<ConnectionFormController>
    {
        void Render(IEnumerable<ConnectionInfo> allConnections, IEnumerable<SharedConnectionSettings> proxyList, ConnectionInfo connection);
        void RenderPrivateKeyFileName(string fileName);
        void Collect(ConnectionInfo connection);
        void ResetAddTunnelGroup();
        bool ValidateTunnel();
        void AddTunnel();
        bool ValidateConnection();
    }
}
