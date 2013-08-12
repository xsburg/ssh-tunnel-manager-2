// ***********************************************************************
// <author>Stephan Burguchev</author>
// <copyright company="Stephan Burguchev">
//   Copyright (c) Stephan Burguchev 2012-2013. All rights reserved.
// </copyright>
// <summary>
//   IConnectionManagerObserver.cs
// </summary>
// ***********************************************************************

using STM.Core.Data;

namespace STM.Core
{
    public interface IConnectionManagerObserver
    {
        void HandleFatalError(ConnectionInfo sender, string errorMessage);
        void HandleForwardingError(ConnectionInfo sender, TunnelInfo tunnel, string errorMessage);
        void HandleMessage(ConnectionInfo sender, MessageSeverity severity, string message);
        void HandleStateChanged(ConnectionInfo sender, ConnectionState state);
    }
}
