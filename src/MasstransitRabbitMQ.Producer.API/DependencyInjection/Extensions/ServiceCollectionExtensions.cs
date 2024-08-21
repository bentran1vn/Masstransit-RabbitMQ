using MassTransit;
using MasstransitRabbitMQ.Producer.API.DependencyInjection.Options;

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
                
                // Rename for Root Exchange and setup for consumer also
                // Exchange: MasstransitRabbitMQ.Contract.IntegrationEvents:DomainEvent-SmsNotificationEvent =>> Exchange: sms-notification-event
                bus.MessageTopology.SetEntityNameFormatter(new KebabCaseEntityNameFormatter());
            });
        });

        return services;
    }
}