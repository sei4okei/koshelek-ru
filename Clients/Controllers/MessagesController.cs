using Clients.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Clients.Controllers
{
    public class MessagesController : Controller
    {
        private readonly ILogger<MessagesController> _logger;

        public MessagesController(ILogger<MessagesController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult AddMessage()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddMessage(string message)
        {

        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
