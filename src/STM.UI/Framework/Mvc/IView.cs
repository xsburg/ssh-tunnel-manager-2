using System;

namespace STM.UI.Framework.Mvc
{
    public interface IView<TController> : IDisposable
    {
        TController Controller { get; }
        bool IsDisposed { get; }
    }
}