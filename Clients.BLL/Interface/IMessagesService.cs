using Clients.BLL.Models;
using Clients.DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clients.BLL.Interface
{
    public interface IMessagesService
    {
        public Task<bool> SendMessage(string message);
        public Task<List<Message>> GetMessagesByDateRange(DateRange range);
    }
}
