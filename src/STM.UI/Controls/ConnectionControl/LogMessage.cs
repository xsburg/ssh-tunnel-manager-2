using STM.Core;

namespace STM.UI.Controls.ConnectionControl
{
    public class LogMessage
    {
        public LogMessage(MessageSeverity severity, string message)
        {
            this.Severity = severity;
            this.Message = message;
        }

        public MessageSeverity Severity { get; private set; }
        public string Message { get; private set; }
    }
}