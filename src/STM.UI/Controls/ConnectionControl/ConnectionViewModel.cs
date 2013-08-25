// ***********************************************************************
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
using System.ComponentModel;
using System.Drawing;
using System.Runtime.CompilerServices;
using STM.Core;
using STM.Core.Data;
using STM.UI.Annotations;
using STM.UI.Properties;

namespace STM.UI.Controls.ConnectionControl
{
    public class ConnectionViewModel : INotifyPropertyChanged
    {
        private const int MaxLogSize = 1000;
        private readonly List<LogMessage> logMessages = new List<LogMessage>();
        private ConnectionState state;

        public ConnectionViewModel(ConnectionInfo info)
        {
            if (info == null)
            {
                throw new ArgumentNullException("info");
            }

            this.Info = info;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public string Address
        {
            get
            {
                return string.Format("{0}:{1}", this.Info.HostName, this.Info.Port);
            }
        }

        public ConnectionInfo Info { get; private set; }

        public IEnumerable<LogMessage> LogMessages
        {
            get
            {
                return this.logMessages;
            }
        }

        public string Name
        {
            get
            {
                return this.Info.Name;
            }
        }

        public string Parent
        {
            get
            {
                return this.Info.ParentName;
            }
        }

        public ConnectionState State
        {
            get
            {
                return this.state;
            }
            set
            {
                if (value == this.state)
                {
                    return;
                }
                this.state = value;
                this.OnPropertyChanged();
                this.OnPropertyChanged("StateIcon");
                this.OnPropertyChanged("StateColor");
            }
        }

        public Image StateIcon
        {
            get
            {
                switch (this.State)
                {
                case ConnectionState.Open:
                    return Resources.greenCircle;
                case ConnectionState.Closed:
                    return Resources.redCircle;
                case ConnectionState.Opening:
                case ConnectionState.Closing:
                    return Resources.yellowCircle;
                default:
                    throw new ArgumentOutOfRangeException();
                }
            }
        }

        public Color StateColor
        {
            get
            {
                switch (this.State)
                {
                case ConnectionState.Open:
                    return Color.Green;
                case ConnectionState.Closed:
                    return Color.DarkRed;
                case ConnectionState.Opening:
                case ConnectionState.Closing:
                    return Color.Goldenrod;
                default:
                    throw new ArgumentOutOfRangeException();
                }
            }
        }

        public string UserName
        {
            get
            {
                return this.Info.UserName;
            }
        }

        public void AddLogMessage(MessageSeverity severity, string message)
        {
            this.logMessages.Add(new LogMessage(severity, message));
            if (this.logMessages.Count > MaxLogSize)
            {
                this.logMessages.RemoveAt(0);
            }
        }

        public void ClearLog()
        {
            this.logMessages.Clear();
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = this.PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
