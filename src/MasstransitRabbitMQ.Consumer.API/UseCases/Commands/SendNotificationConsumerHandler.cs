using MasstransitRabbitMQ.Contract.Abstractions.IntegrationEvents;
using MediatR;

namespace MasstransitRabbitMQ.Consumer.API.UseCases.Commands;

public class SendNotificationConsumerHandler: IRequestHandler<Command.SendNotification>
{
    private readonly ILogger<SendNotificationConsumerHandler> _logger;
    
    public SendNotificationConsumerHandler(ILogger<SendNotificationConsumerHandler> logger)
    {
        _logger = logger;
    }

    public async Task Handle(Command.SendNotification request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Message Received: {message}", request);
    }
}