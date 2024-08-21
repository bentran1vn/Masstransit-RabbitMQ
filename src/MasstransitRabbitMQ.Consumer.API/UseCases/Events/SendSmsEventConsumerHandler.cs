using MasstransitRabbitMQ.Contract.Abstractions.IntegrationEvents;
using MediatR;

namespace MasstransitRabbitMQ.Consumer.API.UseCases.Events;

public class SendSmsEventConsumerHandler : IRequestHandler<DomainEvent.SmsNotificationEvent>
{
    private readonly ILogger<SendSmsEventConsumerHandler> _logger;

    public SendSmsEventConsumerHandler(ILogger<SendSmsEventConsumerHandler> logger)
    {
        _logger = logger;
    }
    
    public async Task Handle(DomainEvent.SmsNotificationEvent request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Message Received: {message}", request);
    }
}