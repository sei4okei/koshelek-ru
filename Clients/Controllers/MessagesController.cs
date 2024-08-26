using Clients.BLL.Interface;
using Clients.DAL.Model;
using Clients.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Clients.Controllers
{
    public class MessagesController : Controller
    {
        private readonly ILogger<MessagesController> _logger;
        private readonly IMessagesService _messagesService;

        public MessagesController(ILogger<MessagesController> logger, IMessagesService messagesService)
        {
            _logger = logger;
            _messagesService = messagesService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult AddMessage()
        {
            var message = new Request();

            return View(message);
        }

        [HttpPost]
        public async Task<IActionResult> AddMessage(Request message)
        {
            var result = await _messagesService.SendMessage(message.Text);

            return View();
        }

        [HttpGet("messages")]
        public IActionResult WebSocketMessages()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
