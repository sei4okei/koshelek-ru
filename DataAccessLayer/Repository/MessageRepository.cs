using DataAccessLayer.Interface;
using DataAccessLayer.Model;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace DataAccessLayer.Repository
{
    public class MessageRepository : IMessageRepository
    {
        private string _connectionString = "UserID=postgres;Password=0000;Server=localhost;Port=5432;Database=Messages;Pooling=true";
        
        public bool AddMessage(Message message)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();

                using (var command = new NpgsqlCommand("INSERT INTO Messages (Text, DateTime) VALUES (@Text, @DateTime)", connection))
                {
                    command.Parameters.AddWithValue("@Text", message.Text);
                    command.Parameters.AddWithValue("@DateTime", message.DateTime);

                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }

        public List<Message> GetMessagesByDateRange(DateTime startDate, DateTime endDate)
        {
            var messages = new List<Message>();

            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();

                using (var command = new NpgsqlCommand("SELECT Id, Text, DateTime FROM Messages WHERE DateTime >= @StartDate AND DateTime <= @EndDate", connection))
                {
                    command.Parameters.AddWithValue("@StartDate", startDate);
                    command.Parameters.AddWithValue("@EndDate", endDate);

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var message = new Message
                            {
                                Id = reader.GetInt32(0),
                                Text = reader.GetString(1),
                                DateTime = reader.GetDateTime(2)
                            };

                            messages.Add(message);
                        }
                    }
                }
            }

            return messages;
        }

        public bool WriteMessage(Message message)
        {
            throw new NotImplementedException();
        }
    }
}
