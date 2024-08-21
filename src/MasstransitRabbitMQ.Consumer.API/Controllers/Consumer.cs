using Microsoft.AspNetCore.Mvc;

namespace MasstransitRabbitMQ.Consumer.API.Controllers;

public class Consumer : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}