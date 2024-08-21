using MasstransitRabbitMQ.Consumer.API.Abstract.Messages;
using MasstransitRabbitMQ.Contract.Abstractions.IntegrationEvents;
using MediatR;

namespace MasstransitRabbitMQ.Consumer.API.MessageBus.Consumers.Events;

public class SmsNotificationConsumer(ISender _sender) : Consumer<DomainEvent.SmsNotification>(_sender)
{

}