using Microsoft.EntityFrameworkCore;
using Data.Context;
using IoC;
using PresentationMVC.Data;
using Microsoft.AspNetCore.Identity;
using PresentationMVC.Models;
using Microsoft.IdentityModel.Tokens;
using PresentationMVC.AuthificationOptions;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddControllersWithViews();


        var connectionStringDbCar = builder.Configuration.GetConnectionString("TestConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
        builder.Services.AddDbContext<ShopDbContext>(options => options.UseSqlServer(connectionStringDbCar));

        var connectionStringPresantationMVCDbContext = builder.Configuration.GetConnectionString("IdentityConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
        builder.Services.AddDbContext<PresantationMVCDbContext>(options => options.UseSqlServer(connectionStringPresantationMVCDbContext));
        /*        builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true);*/
        builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<PresantationMVCDbContext>();

        builder.Services.AddAuthentication("Bearer").AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = AuthenticationOptions.ISSUER,
                ValidateAudience = true,
                ValidAudience = AuthenticationOptions.ISSUER,
                ValidateLifetime = true,
                IssuerSigningKey = AuthenticationOptions.GetSecurityKey(),
                ValidateIssuerSigningKey = true,
            };
        });
        builder.Services.AddAuthorization();


        RegisterServices(builder.Services);
       
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
    private static void RegisterServices(IServiceCollection services)
    {
        DependencyContainer.RegisterServices(services);
    }
}