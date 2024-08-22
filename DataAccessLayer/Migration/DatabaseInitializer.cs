using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Migration
{
    public class DatabaseInitializer
    {
        private string _connectionString = "UserID=postgres;Password=0000;Server=localhost;Port=5432;Database=Messages;Pooling=true";

        public bool CreateMessagesTable()
        {
            string checkTableQuery = @"
            SELECT EXISTS (
                SELECT FROM information_schema.tables 
                WHERE table_schema = 'public' 
                AND table_name = 'messages'
            );";

            string createTableQuery = @"
            CREATE TABLE public.messages (
                id int NOT NULL GENERATED ALWAYS AS IDENTITY,
                ""text"" varchar NOT NULL,
                datetime timestamp with time zone NOT NULL,
                CONSTRAINT messages_pk PRIMARY KEY (id)
            );";

            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();

                using (var command = new NpgsqlCommand(checkTableQuery, connection))
                {
                    try
                    {
                        bool tableExists = (bool)command.ExecuteScalar();

                        if (tableExists)
                        {
                            Console.WriteLine("Table 'messages' already exists. Skipping creation.");
                            return true; // Таблица уже существует, ничего не делаем
                        }

                        // Таблицы нет, создаём её
                        command.CommandText = createTableQuery;
                        command.ExecuteNonQuery();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error creating table: {ex.Message}");
                        return false;
                    }
                }
            }
        }
    }
}
