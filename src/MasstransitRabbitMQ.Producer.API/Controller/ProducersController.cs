using MassTransit;
using MasstransitRabbitMQ.Contract.Abstractions.IntegrationEvents;
using MasstransitRabbitMQ.Contract.Abstractions.IntegrationEvents.Enumerations;
using Microsoft.AspNetCore.Mvc;

namespace MasstransitRabbitMQ.Producer.API.Controller
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class ProducersController(IPublishEndpoint publishEndpoint, IBus bus) : ControllerBase
    {
        [HttpPost(Name = "public-sms-notification")]
        public async Task<IActionResult> PublicSmsNotificationEvent()
        {
            using var source = new CancellationTokenSource(TimeSpan.FromSeconds(30));
            await publishEndpoint.Publish(new DomainEvent.SmsNotification()
            {
                Id = Guid.NewGuid(),
                Description = "Sms description",
                Name = "sms notification",
                TransactionId = Guid.NewGuid(),
                TimeStamp = DateTime.Now,
                Type = NotificationType.sms
            }, source.Token);
            return Accepted();
        }
        
        [HttpPost(Name = "public-email-notification")]
        public async Task<IActionResult> PublicEmailNotificationEvent()
        {
            using var source = new CancellationTokenSource(TimeSpan.FromSeconds(30));
            await publishEndpoint.Publish(new DomainEvent.EmailNotification()
            {
                Id = Guid.NewGuid(),
                Description = "Email description",
                Name = "Email notification",
                TransactionId = Guid.NewGuid(),
                TimeStamp = DateTime.Now,
                Type = NotificationType.email
            }, source.Token);
            return Accepted();
        }

        [HttpPost()]
        public async Task<IActionResult> SendNotification()
        {
            var sendNotification = new Command.SendNotification()
            {
                Id = Guid.NewGuid(),
                Description = "Send email Notification",
                Name = "Send email notification name",
                TransactionId = Guid.NewGuid(),
                TimeStamp = DateTime.Now,
                Type = NotificationType.email
            };
            var endpoint = await bus.GetSendEndpoint(Address<Command.SendNotification>());
            using var source = new CancellationTokenSource(TimeSpan.FromSeconds(30));
            await endpoint.Send(sendNotification, source.Token);
            return Accepted();
        }

        private static Uri Address<T>()
            => new($"queue:{KebabCaseEndpointNameFormatter.Instance.SanitizeName(typeof(T).Name)}");
    }
}

