using DataAccessLayer.Interface;
using DataAccessLayer.Repository;
using DataAccessLayer.Migration;
namespace WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddScoped<IMessageRepository, MessageRepository>();
            builder.Services.AddSingleton(sp => new DatabaseInitializer());

            builder.Services.AddControllers();

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                var dbInitializer = scope.ServiceProvider.GetRequiredService<DatabaseInitializer>();
                bool isTableCreated = dbInitializer.CreateMessagesTable();

                if (isTableCreated)
                {
                    Console.WriteLine("Table 'messages' is ready.");
                }
                else
                {
                    Console.WriteLine("Failed to ensure the 'messages' table exists.");
                    return;
                }
            }

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
