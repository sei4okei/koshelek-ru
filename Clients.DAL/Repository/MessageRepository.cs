using Clients.DAL.Interface;
using Clients.DAL.Model;
using System.Net.Http.Json;
using Microsoft.AspNetCore.SignalR;
using Clients.DAL.Hub;

namespace Clients.DAL.Repository
{
    public class MessageRepository : IMessagesRepository
    {
        private readonly string _url = "http://localhost:5095/Message/";
        private readonly IHubContext<MessageHub> _hubContext;

        public MessageRepository(IHubContext<MessageHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task<List<Response>> GetMessagesByDateRange(DateTime startDate, DateTime endDate)
        {
            List<Response> messages = new List<Response>();

            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync(_url + "range" + "?startDate=" + startDate + "&endDate=" + endDate);
                response.EnsureSuccessStatusCode();
                if (response.IsSuccessStatusCode)
                {
                    messages = await response.Content.ReadFromJsonAsync<List<Response>>();

                    return messages;
                }

                return null;
            }
        }

        public async Task<bool> PostMessage(Request message)
        {
            using (HttpClient client = new HttpClient())
            {
                var response = await client.PostAsJsonAsync(_url + "add", message.Text);
                response.EnsureSuccessStatusCode();
                if (response.IsSuccessStatusCode)
                {
                    // Асинхронно отправляем сообщение через SignalR
                    await _hubContext.Clients.All.SendAsync("ReceiveMessage", message.Text);

                    string result = await response.Content.ReadAsStringAsync();
                    return result == "Message added successfully.";
                }
                return false;
            }
        }

    }
}
