using MasstransitRabbitMQ.Contract.Abstractions.IntegrationEvents;
using MediatR;

namespace MasstransitRabbitMQ.Consumer.API.UseCases.Events;

public class SmsNotificationConsumerHandler : IRequestHandler<DomainEvent.SmsNotification>
{
    private readonly ILogger<SmsNotificationConsumerHandler> _logger;

    public SmsNotificationConsumerHandler(ILogger<SmsNotificationConsumerHandler> logger)
    {
        _logger = logger;
    }
    
    public async Task Handle(DomainEvent.SmsNotification request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Message Received: {message}", request);
    }
}