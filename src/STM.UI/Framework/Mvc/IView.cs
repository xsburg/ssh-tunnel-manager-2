using System;

namespace STM.UI.Framework.Mvc
{
    public interface IView : IDisposable
    {
        bool IsDisposed { get; }
        event EventHandler Disposed;
    }

    // ReSharper disable once TypeParameterCanBeVariant
    public interface IView<TController> : IView where TController : class
    {
        TController Controller { get; }
    }
}