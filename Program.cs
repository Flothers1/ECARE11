using ECARE.Helper;
using ECARE.Interface;
using ECARE.Interface.FileStorage;
using ECARE.Models;
using ECARE.Services;
using ECARE.Services.FileStorage;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ECARE
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //Hello
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.Configure<FormOptions>(options =>
            {
                options.MultipartBodyLengthLimit = 104857600;
            });
            builder.Services.AddControllersWithViews(options =>
            {
                options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
            });
            //DBContext
            builder.Services.AddDbContext<ECAREContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("ECAREConnection")));
            //Identity
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
             .AddEntityFrameworkStores<ECAREContext>()
              .AddDefaultTokenProviders();
            builder.Services.AddControllersWithViews();
            


            builder.Services.AddHttpClient();

            // Add services to the container.
                       builder.Services.AddControllersWithViews();

            builder.Services.AddScoped<IUsersService, UsersService>();
            builder.Services.AddScoped<IFileStorageService, FileStorageService>();
            //Other Services
            builder.Services.AddSignalR();
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            //HttpService


            //app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Account}/{action=Login}/{id?}");
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<NotificationHub>("/notificationHub");
            });
            try
            {
                app.Run();

            } catch(Exception ex) { 
                File.AppendAllText(Path.Combine(AppContext.BaseDirectory , "LogError.txt"), ex.Message + Environment.NewLine);
            }
            
        }
    }
}
