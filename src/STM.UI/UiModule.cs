// ***********************************************************************
// <author>Stephan Burguchev</author>
// <copyright company="Stephan Burguchev">
//   Copyright (c) Stephan Burguchev 2012-2013. All rights reserved.
// </copyright>
// <summary>
//   CoreModule.cs
// </summary>
// ***********************************************************************

using Ninject.Extensions.Conventions;
using Ninject.Modules;
using STM.UI.Framework;

namespace STM.UI
{
    public class UIModule : NinjectModule
    {
        public override void Load()
        {
            this.Bind<IEventAggregator>().To<EventAggregator>().InSingletonScope();
            this.Bind<IWindowManager>().To<WindowManager>().InSingletonScope();
            this.Bind<IMessageBoxService>().To<MessageBoxService>().InSingletonScope();
            this.Bind<IStandardDialogService>().To<StandardDialogService>().InSingletonScope();
            this.Bind(
                f => f.FromThisAssembly()
                    .Select(t => t.IsClass && (t.Name.EndsWith("Form") || t.Name.EndsWith("Control")))
                    .BindDefaultInterface());
        }
    }
}