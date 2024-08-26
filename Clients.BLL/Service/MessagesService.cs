using Clients.BLL.Interface;
using Clients.BLL.Models;
using Clients.DAL.Interface;
using Clients.DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clients.BLL.Service
{
    public class MessagesService : IMessagesService
    {
        private IMessagesRepository _messagesRepository;

        public MessagesService(IMessagesRepository messagesRepository)
        {
            _messagesRepository = messagesRepository;
        }

        public async Task<List<Message>> GetMessagesByDateRange(DateRange range)
        {
            var result = await _messagesRepository.GetMessagesByDateRange(range);

            if (result == null)
            {
                return null;
            }

            List<Message> messages = new List<Message>();

            foreach (var message in result)
            {
                messages.Add(new Message()
                {
                    Text = message.Text,
                    DateTime = message.DateTime
                });
            }

            return messages;
        }

        public async Task<bool> SendMessage(string message)
        {
            var result = await _messagesRepository.PostMessage(new Request()
            {
                Text = message,
            });

            if (result)
            {
                return true;
            }

            return false;
        }
    }
}
