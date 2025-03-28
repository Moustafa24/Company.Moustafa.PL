using Company.BLL.Interfaces;
using Company.Moustafa.BLL;
using Company.Moustafa.BLL.Interfaces;
using Company.Moustafa.BLL.Repositories;
using Company.Moustafa.DAL.Data.Context;
using Company.Moustafa.DAL.Models;
using Company.Moustafa.PL.Controllers;
using Company.Moustafa.PL.Helpers;
using Company.Moustafa.PL.Mapping;
using Company.Moustafa.PL.Servies;
using Company.Moustafa.PL.Settings;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Company.Moustafa.PL
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews(); // Register Built-in MVC services

            builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>(); // Register DI for DepartmentRepository
            //builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>(); // Register DI for EmployeeRepository

            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>(); // Register DI for UnitOfWork

            builder.Services.AddDbContext<CompanyDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            }); // Register DI for CompanyDbContext

            //builder.Services.AddAutoMapper(typeof(EmployeeProfile)); // Register DI for AutoMapper
            builder.Services.AddAutoMapper(M => M.AddProfile(new EmployeeProfile())); // Register DI for AutoMapper
            builder.Services.AddAutoMapper(M => M.AddProfile(new DepartmentProfile())); // Register DI for AutoMapper


            //builder.Services.AddScoped();    // Create Object Life Time Per Request - Unreachable Object
            //builder.Services.AddTransient(); // Create Object Life Time Per Operation
            //builder.Services.AddSingleton(); // Create Object Life Time Per Application

            builder.Services.Configure<MailSettings>(builder.Configuration.GetSection(nameof(MailSettings)));
            builder.Services.AddScoped<IMailService , MailServies>();

            builder.Services.AddScoped<IScopedService, ScopedService>(); // Per Request
            builder.Services.AddTransient<ITransientService, TransientService>(); // Per Operation
            builder.Services.AddSingleton<ISingletonService, SingletonService>(); // Per Application

            builder.Services.AddIdentity<AppUser,IdentityRole>()
                .AddEntityFrameworkStores<CompanyDbContext>()
                .AddDefaultTokenProviders();
            builder.Services.ConfigureApplicationCookie(config =>
            {
               config.LoginPath = "/Account/SignIn";


            }
            );

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

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
