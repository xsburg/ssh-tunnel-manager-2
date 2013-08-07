using STM.Core.Data;

namespace STM.Core
{
    public interface IConnectionObserver
    {
        void HandleForwardingError(IConnection sender, TunnelInfo tunnel, string errorMessage);
        void HandleStateChanged(IConnection sender);
        void HandleMessage(IConnection sender, MessageSeverity severity, string message);
        void HandleFatalError(string errorMessage);
    }
}