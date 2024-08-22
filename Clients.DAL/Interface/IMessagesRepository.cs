using Clients.DAL.Model;

namespace Clients.DAL.Interface
{
    public interface IMessagesRepository
    {
        public Task<bool> PostMessage(Request message);
        public Task<List<Response>> GetMessagesByDateRange(DateTime startDate, DateTime endDate);
    }
}
