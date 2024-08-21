using System.Reflection;
using MassTransit;
using MasstransitRabbitMQ.Consumer.API.MessageBus.Consumers.Events;
using MasstransitRabbitMQ.Producer.API.DependencyInjection.Options;

namespace MasstransitRabbitMQ.Producer.API.DependencyInjection.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddMediatR(this IServiceCollection services)
        => services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));
    
    public static IServiceCollection AddConfigureMasstransitRabbitMQ(this IServiceCollection services, IConfiguration configuration)
    {
        var masstransitConfiguration = new MasstransitConfiguration();
        configuration.GetSection(nameof(MasstransitConfiguration)).Bind(masstransitConfiguration);

        services.AddMassTransit(cfg =>
        {
            // cfg.AddConsumer<SendSmsWhenReceivedSmsEventConsumer>();
            
            cfg.AddConsumers(Assembly.GetExecutingAssembly());
            
            /** Rename: Exchange and Queue
             * Exchange: SendEmailWhenReceivedEmailEvent =>> Exchange: send-email-when-received-email-event
             * Exchange: SendSmsWhenReceivedSmsEvent =>> Exchange: send-sms-when-received-sms-event
             *
             * Queue SendEmailWhenReceivedEmailEvent =>> Queue send-email-when-received-email-event
             * Queue SendSmsWhenReceivedSmsEvent =>> Queue send-sms-when-received-sms-event
             *
             * =>> Just for endpoint for ComsumerExchange and ConsumerQueue
             * Can not change for root Exchange, actualy TMessageExchange
             * in this ex: Exchange: MasstransitRabbitMQ.Contract.IntegrationEvents:DomainEvent-SmsNotificationEvent
             */
            cfg.SetKebabCaseEndpointNameFormatter();
            
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
                
                bus.ConfigureEndpoints(context);
            });
        });

        return services;
    }
}