using Clients.BLL.Interface;
using Clients.BLL.Service;
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

            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Messages/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
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
