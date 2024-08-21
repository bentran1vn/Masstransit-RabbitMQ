using MassTransit;
using MasstransitRabbitMQ.Contract.Abstractions.IntegrationEvents;
using MasstransitRabbitMQ.Contract.Abstractions.IntegrationEvents.Enumerations;
using Microsoft.AspNetCore.Mvc;

namespace MasstransitRabbitMQ.Producer.API.Controller
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class ProducersController(IPublishEndpoint publishEndpoint) : ControllerBase
    {
        [HttpPost(Name = "public-sms-notification")]
        public async Task<IActionResult> PublicSmsNotificationEvent()
        {
            using var source = new CancellationTokenSource(TimeSpan.FromSeconds(30));
            await publishEndpoint.Publish(new DomainEvent.SmsNotificationEvent()
            {
                Id = Guid.NewGuid(),
                Description = "Sms description",
                Name = "sms notification",
                TransactionId = Guid.NewGuid(),
                Type = NotificationType.sms
            }, source.Token);
            return Accepted();
        }
        
        [HttpPost(Name = "public-email-notification")]
        public async Task<IActionResult> PublicEmailNotificationEvent()
        {
            using var source = new CancellationTokenSource(TimeSpan.FromSeconds(30));
            await publishEndpoint.Publish(new DomainEvent.EmailNotificationEvent()
            {
                Id = Guid.NewGuid(),
                Description = "Email description",
                Name = "Email notification",
                TransactionId = Guid.NewGuid(),
                Type = NotificationType.email
            }, source.Token);
            return Accepted();
        }
    }
}

