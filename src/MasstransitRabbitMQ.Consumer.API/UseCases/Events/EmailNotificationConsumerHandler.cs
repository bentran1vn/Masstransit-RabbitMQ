using MasstransitRabbitMQ.Contract.Abstractions.IntegrationEvents;
using MediatR;

namespace MasstransitRabbitMQ.Consumer.API.UseCases.Events;

public class EmailNotificationConsumerHandler : IRequestHandler<DomainEvent.EmailNotification>
{
    private readonly ILogger<EmailNotificationConsumerHandler> _logger;
    
    public EmailNotificationConsumerHandler(ILogger<EmailNotificationConsumerHandler> logger)
    {
        _logger = logger;
    }

    public async Task Handle(DomainEvent.EmailNotification request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Message Received: {message}", request);
    }
}