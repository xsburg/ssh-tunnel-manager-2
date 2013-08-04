// ***********************************************************************
// <author>Stephan Burguchev</author>
// <copyright company="Stephan Burguchev">
//   Copyright (c) Stephan Burguchev 2012-2013. All rights reserved.
// </copyright>
// <summary>
//   IConnection.cs
// </summary>
// ***********************************************************************

using STM.Core.Data;

namespace STM.Core
{
    public interface IConnection
    {
        ConnectionInfo Info { get; }
        IConnectionObserver Observer { get; set; }
        ConnectionState State { get; }
        void Close();
        void Open();
    }
}
