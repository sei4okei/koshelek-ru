using DataAccessLayer.Interface;
using DataAccessLayer.Model;
using Microsoft.AspNetCore.Mvc;
using WebAPI.DAL.Model;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MessageController : ControllerBase
    {
        private readonly ILogger<MessageController> _logger;
        private IMessageRepository _messageRepository;

        public MessageController(ILogger<MessageController> logger, IMessageRepository messageRepository)
        {
            _messageRepository = messageRepository;
            _logger = logger;
        }

        [HttpPost("add")]
        public IActionResult AddMessage([FromBody] string text)
        {
            bool isAdded = _messageRepository.AddMessage(new Message()
            {
                Text = text,
                DateTime = DateTime.Now,
            });

            if (!isAdded)
            {
                return StatusCode(500, "Failed to add the message.");
            }

            return Ok("Message added successfully.");
        }

        [HttpGet("range")]
        public ActionResult<List<Message>> GetMessagesByDateRange([FromBody] DateRange dateRange)
        {
            var messages = _messageRepository.GetMessagesByDateRange(dateRange.startDate, dateRange.endDate);

            if (messages.Count > 0)
            {
                return Ok(messages);
            }
            else
            {
                return NotFound("No messages found in the specified date range.");
            }
        }
    }
}
