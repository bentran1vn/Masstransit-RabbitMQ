using Microsoft.AspNetCore.Mvc;

namespace MasstransitRabbitMQ.Consumer.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ConsumerController : ControllerBase
    {
        [HttpGet(Name = "GetWeatherForecast")]
        public async Task<IActionResult> PublicSmsNotificationEvent()
        {
            return Ok("");
        }
    }
}

