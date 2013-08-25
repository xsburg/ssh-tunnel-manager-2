// ***********************************************************************
// <author>Stephan Burguchev</author>
// <copyright company="Stephan Burguchev">
//   Copyright (c) Stephan Burguchev 2012-2013. All rights reserved.
// </copyright>
// <summary>
//   TunnelViewModel.cs
// </summary>
// ***********************************************************************

using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using STM.Core.Data;
using STM.UI.Annotations;

namespace STM.UI.Controls.ConnectionControl
{
    public class TunnelViewModel : INotifyPropertyChanged
    {
        private string errorText;

        public TunnelViewModel(TunnelInfo tunnel)
        {
            if (tunnel == null)
            {
                throw new ArgumentNullException("tunnel");
            }

            this.Tunnel = tunnel;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public string ErrorText
        {
            get
            {
                return this.errorText;
            }
            set
            {
                if (value == this.errorText)
                {
                    return;
                }

                this.errorText = value;
                this.OnPropertyChanged();
            }
        }

        public int LocalPort
        {
            get
            {
                return this.Tunnel.LocalPort;
            }
        }

        public string Name
        {
            get
            {
                return this.Tunnel.Name;
            }
        }

        public string RemoteHostName
        {
            get
            {
                return this.Tunnel.RemoteHostName;
            }
        }

        public int RemotePort
        {
            get
            {
                return this.Tunnel.RemotePort;
            }
        }

        public TunnelInfo Tunnel { get; private set; }

        public TunnelType Type
        {
            get
            {
                return this.Tunnel.Type;
            }
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
