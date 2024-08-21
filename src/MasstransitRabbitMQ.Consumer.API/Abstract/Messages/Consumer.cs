using MassTransit;
using MasstransitRabbitMQ.Contract.Abstractions.Messages;
using MediatR;
using INotification = MasstransitRabbitMQ.Contract.Abstractions.Messages.INotification;

namespace MasstransitRabbitMQ.Consumer.API.Abstract.Messages;

public abstract class Consumer<TMessage>(ISender _sender) : IConsumer<TMessage>
    where TMessage : class, INotification
{
    public async Task Consume(ConsumeContext<TMessage> context)
    {
        await _sender.Send(context.Message);
    }
}