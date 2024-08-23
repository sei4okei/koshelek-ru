using Clients.DAL.Interface;
using Clients.DAL.Model;
using System.Net.Http.Json;

namespace Clients.DAL.Repository
{
    public class MessageRepository : IMessagesRepository
    {
        private readonly string _url = "http://localhost:5095/Message/";

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
                //var response = await client.PostAsJsonAsync(_url + "add", message);
                response.EnsureSuccessStatusCode();
                if (response.IsSuccessStatusCode)
                {
                    return response.Content.ReadAsStringAsync().Result
                        == "Message added successfully." ? true : false;
                }
                return false;
            }
        }
    }
}
