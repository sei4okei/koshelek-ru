using Clients.BLL.Interface;
using Clients.BLL.Service;
using Clients.DAL.Hub;
using Clients.DAL.Interface;
using Clients.DAL.Repository;

namespace Clients
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddScoped<IMessagesRepository, MessageRepository>();
            builder.Services.AddScoped<IMessagesService, MessagesService>();
            builder.Services.AddSignalR();

            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            app.UseWebSockets();

            app.MapHub<MessageHub>("/messageHub");

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Messages/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Messages}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
