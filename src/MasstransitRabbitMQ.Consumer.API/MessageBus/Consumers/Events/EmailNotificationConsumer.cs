using MasstransitRabbitMQ.Consumer.API.Abstract.Messages;
using MasstransitRabbitMQ.Contract.Abstractions.IntegrationEvents;
using MediatR;

namespace MasstransitRabbitMQ.Consumer.API.MessageBus.Consumers.Events;

public class EmailNotificationConsumer(ISender _sender) : Consumer<DomainEvent.EmailNotification>(_sender)
{

}