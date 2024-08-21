using MassTransit;
using MasstransitRabbitMQ.Contract.Abstractions.IntegrationEvents;
using MasstransitRabbitMQ.Contract.Abstractions.Messages;
using MasstransitRabbitMQ.Producer.API.DependencyInjection.Options;
using RabbitMQ.Client;

namespace MasstransitRabbitMQ.Producer.API.DependencyInjection.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddConfigureMasstransitRabbitMQ(this IServiceCollection services, IConfiguration configuration)
    {
        var masstransitConfiguration = new MasstransitConfiguration();
        configuration.GetSection(nameof(MasstransitConfiguration)).Bind(masstransitConfiguration);

        services.AddMassTransit(cfg =>
        {
            cfg.UsingRabbitMq((context, bus) =>
            {
                bus.Host(masstransitConfiguration.Host, masstransitConfiguration.VHost, h =>
                {
                    h.Username(masstransitConfiguration.UserName);
                    h.Password(masstransitConfiguration.Password);
                });
                
                bus.Message<INotification>( e => e.SetEntityName(masstransitConfiguration.ExchangeName) );
                
                bus.Publish<INotification>(e =>
                {
                    e.Durable = true;
                    e.AutoDelete = false;
                    e.ExchangeType = ExchangeType.Topic;
                });
                
                bus.Send<INotification>(e =>
                {
                    // Use type field of message for routing key: sms | email
                    e.UseRoutingKeyFormatter(context => context.Message.Type.ToString());
                });
                
                // Rename for Root Exchange and setup for consumer also
                // Exchange: MasstransitRabbitMQ.Contract.IntegrationEvents:DomainEvent-SmsNotificationEvent =>> Exchange: sms-notification-event
                bus.MessageTopology.SetEntityNameFormatter(new KebabCaseEntityNameFormatter());
            });
        });

        return services;
    }
}