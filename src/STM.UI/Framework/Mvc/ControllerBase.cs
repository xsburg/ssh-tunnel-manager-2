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
using Ninject.Extensions.Logging;

namespace STM.UI.Framework.Mvc
{
    public abstract class ControllerBase<TView> : IDisposable where TView : IView
    {
        protected ControllerBase(
            ILogger logger,
            IMessageBoxService messageBoxService,
            IStandardDialogService standardDialogService,
            IEventAggregator eventAggregator = null)
        {
            if (logger == null)
            {
                throw new ArgumentNullException("logger");
            }

            if (messageBoxService == null)
            {
                throw new ArgumentNullException("messageBoxService");
            }

            if (standardDialogService == null)
            {
                throw new ArgumentNullException("standardDialogService");
            }

            this.Logger = logger;
            this.EventAggregator = eventAggregator;
            this.MessageBoxService = messageBoxService;
            this.StandardDialogService = standardDialogService;

            if (this.EventAggregator != null)
            {
                this.EventAggregator.Subscribe(this);
            }
        }

        protected IEventAggregator EventAggregator { get; private set; }
        protected ILogger Logger { get; private set; }
        protected IMessageBoxService MessageBoxService { get; private set; }
        protected IStandardDialogService StandardDialogService { get; private set; }

        protected TView View { get; private set; }

        public void Dispose()
        {
            if (this.EventAggregator != null)
            {
                this.EventAggregator.Unsubscribe(this);
                this.EventAggregator = null;
            }
        }

        public void Register(TView view)
        {
            this.View = view;
            this.View.Disposed += (e, a) => this.Dispose();
            this.OnViewRegistered();
        }

        protected virtual void OnViewRegistered()
        {
        }
    }
}
