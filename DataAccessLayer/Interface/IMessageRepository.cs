using DataAccessLayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Interface
{
    public interface IMessageRepository
    {
        public bool AddMessage(Message message);
        public List<Message> GetMessagesByDateRange(DateTime startDate, DateTime endDate);
    }
}
