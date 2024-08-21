using MassTransit;

namespace MasstransitRabbitMQ.Contract.Abstractions.Messages;

[ExcludeFromTopology]
public interface INotification : IMessage
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string Type { get; set; }
    public Guid TransactionId { get; set; }
}