using System;
using ICDE.Data;
using ICDE.Data.Entities.Identity;
using ICDE.Data.Extensions;
using ICDE.Lib.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ICDE.Web;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddControllersWithViews();

        builder.Services
            .AddLib()
            .AddData();

        builder.Services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = "Identity.Application";
            options.DefaultSignInScheme = "Identity.Application";
            options.DefaultChallengeScheme = "Identity.Application";
        }).AddCookie("Identity.Application", options =>
        {
            options.LoginPath = "/auth/login"; // Redirect here if not authenticated
            options.LogoutPath = "/auth/logout";
            options.ExpireTimeSpan = TimeSpan.FromMinutes(30); // Cookie expiration
        });

        builder.Services
            .AddIdentityCore<User>()
            .AddRoles<Role>()
            .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders()
            .AddSignInManager();

        builder.Services.AddDbContext<AppDbContext>(opt => opt.UseSqlServer("Server=localhost;Database=ICDE;Trusted_Connection=True;Encrypt=False"));

        builder.Services.ConfigureApplicationCookie(options =>
        {
            options.LoginPath = "/Account/Login";
        });

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
