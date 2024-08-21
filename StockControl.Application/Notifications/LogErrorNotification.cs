using MediatR;

namespace StockControl.Application.Notifications
{
    public class LogErrorNotification : INotification
    {
        public Exception Exception { get; set; }
    }
}
