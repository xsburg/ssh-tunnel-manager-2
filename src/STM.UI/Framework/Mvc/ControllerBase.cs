// ***********************************************************************
// <author>Stephan Burguchev</author>
// <copyright company="Stephan Burguchev">
//   Copyright (c) Stephan Burguchev 2012-2013. All rights reserved.
// </copyright>
// <summary>
//   ControllerBase.cs
// </summary>
// ***********************************************************************

using System;

namespace STM.UI.Framework.Mvc
{
    public abstract class ControllerBase<TView> : IDisposable where TView : IView
    {
        protected IEventAggregator EventAggregator { get; set; }
        protected IMessageBoxService MessageBoxService { get; set; }
        protected IStandardDialogService StandardDialogService { get; set; }

        public ControllerBase(IMessageBoxService messageBoxService, IStandardDialogService standardDialogService, IEventAggregator eventAggregator = null)
        {
            if (messageBoxService == null)
            {
                throw new ArgumentNullException("messageBoxService");
            }

            if (standardDialogService == null)
            {
                throw new ArgumentNullException("standardDialogService");
            }

            this.EventAggregator = eventAggregator;
            this.MessageBoxService = messageBoxService;
            this.StandardDialogService = standardDialogService;

            if (this.EventAggregator != null)
            {
                this.EventAggregator.Subscribe(this);
            }
        }

        protected TView View { get; private set; }

        public void Register(TView view)
        {
            this.View = view;
            this.View.Disposed += (e, a) => this.Dispose();
            this.OnViewRegistered();
        }

        protected virtual void OnViewRegistered()
        {
        }

        public void Dispose()
        {
            if (this.EventAggregator != null)
            {
                this.EventAggregator.Unsubscribe(this);
                this.EventAggregator = null;
            }
        }
    }
}
