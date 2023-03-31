using Microsoft.Extensions.DependencyInjection;
using Aplication.Interfaces;
using Aplication.Services;
using Domain.Interfaces;
using Data.Repository;
using Aplication.Intarfaces;
using Domain.Interfeces;

namespace IoC
{
    public class DependencyContainer
    {
        public static void RegisterServices(IServiceCollection services)
        {

            //Car
            services.AddScoped<ICarServices, CarServices>();
            services.AddScoped<ICarRepository, CarRepository>();
            //User
            services.AddScoped<IUserServices, UserServices>();
            services.AddScoped<IUserRepository, UserRepository>();
            //Picture
            services.AddScoped<IPictureServices, PictureServices>();
            services.AddScoped<IPictureRepository, PictureRepository>();


        }
    }
}
