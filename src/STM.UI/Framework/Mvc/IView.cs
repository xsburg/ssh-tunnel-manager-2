using System;

namespace STM.UI.Framework.Mvc
{
    // ReSharper disable once TypeParameterCanBeVariant
    public interface IView<TController> : IDisposable where TController : class
    {
        TController Controller { get; }
        bool IsDisposed { get; }
    }
}