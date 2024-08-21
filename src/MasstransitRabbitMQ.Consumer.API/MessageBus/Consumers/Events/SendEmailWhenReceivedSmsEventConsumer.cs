using MasstransitRabbitMQ.Consumer.API.Abstract.Messages;
using MasstransitRabbitMQ.Contract.Abstractions.IntegrationEvents;
using MediatR;

namespace MasstransitRabbitMQ.Consumer.API.MessageBus.Consumers.Events;

public class SendEmailWhenReceivedSmsEventConsumer(ISender _sender) : Consumer<DomainEvent.EmailNotificationEvent>(_sender)
{

}