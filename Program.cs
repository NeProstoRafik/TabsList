using Microsoft.EntityFrameworkCore;
using TabsList.DAL;
using TabsList.DAL.Interfaces;
using TabsList.DAL.Repositories;
using TabsList.Domain.Entity;
using TabsList.Service.Emplementations;
using TabsList.Service.Interfaces;

namespace TabsList
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            //Регистрируем зависимости
            builder.Services.AddScoped<IBaseRepository<Tabs>, TabsRepository>();
            builder.Services.AddScoped<ITabsService, TabsService>();

            //регестрируем ДБ
            var connectionStrings = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(connectionStrings));

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}