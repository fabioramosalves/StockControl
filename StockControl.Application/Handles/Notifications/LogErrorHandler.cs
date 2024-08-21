using MediatR;
using Microsoft.Extensions.Logging;
using StockControl.Application.Notifications;

namespace StockControl.Application.Handles.Notifications
{
    public class LogErrorHandler : INotificationHandler<LogErrorNotification>
    {
        private readonly ILogger<LogErrorHandler> _logger;

        public LogErrorHandler(ILogger<LogErrorHandler> logger)
        {
            _logger = logger;
        }

        public Task Handle(LogErrorNotification notification, CancellationToken cancellationToken)
        {
            _logger.LogError(notification.Exception, "A MediatR handled exception occurred.");

            return Task.CompletedTask;
        }
    }
}
