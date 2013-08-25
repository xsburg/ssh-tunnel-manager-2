// ***********************************************************************
// <author>Stephan Burguchev</author>
// <copyright company="Stephan Burguchev">
//   Copyright (c) Stephan Burguchev 2012-2013. All rights reserved.
// </copyright>
// <summary>
//   IConnectionControl.cs
// </summary>
// ***********************************************************************

using STM.Core;
using STM.Core.Data;
using STM.UI.Framework.Mvc;

namespace STM.UI.Controls.ConnectionControl
{
    public interface IConnectionControl : IView<ConnectionControlController>
    {
        void Render(ConnectionState state);
        void Render(ConnectionViewModel connectionInfo);
    }
}
