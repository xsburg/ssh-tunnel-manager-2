﻿// ***********************************************************************
// <author>Stephan Burguchev</author>
// <copyright company="Stephan Burguchev">
//   Copyright (c) Stephan Burguchev 2012-2013. All rights reserved.
// </copyright>
// <summary>
//   ConnectionViewModel.cs
// </summary>
// ***********************************************************************

using System;
using System.Collections.Generic;
using STM.Core;
using STM.Core.Data;

namespace STM.UI.Controls.ConnectionControl
{
    public class ConnectionViewModel
    {
        private const int MaxLogSize = 1000;
        private readonly List<string> logMessages = new List<string>();

        public ConnectionViewModel(ConnectionInfo info)
        {
            if (info == null)
            {
                throw new ArgumentNullException("info");
            }

            this.Info = info;
        }

        public ConnectionInfo Info { get; private set; }

        public IEnumerable<string> LogMessages
        {
            get
            {
                return this.logMessages;
            }
        }

        public ConnectionState State { get; set; }

        public void AddLogMessage(string message)
        {
            this.logMessages.Add(message);
            if (this.logMessages.Count > MaxLogSize)
            {
                this.logMessages.RemoveAt(0);
            }
        }

        public void ClearLog()
        {
            this.logMessages.Clear();
        }
    }
}