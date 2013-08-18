using System;

namespace STM.UI.Framework.Mvc
{
    public interface IView : IDisposable
    {
        bool IsDisposed { get; }
    }

    // ReSharper disable once TypeParameterCanBeVariant
    public interface IView<TController> : IView where TController : class
    {
        TController Controller { get; }
    }
}