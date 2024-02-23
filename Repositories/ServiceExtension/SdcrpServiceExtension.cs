using DAOs;
using Microsoft.Extensions.DependencyInjection;
using Repositories.Interfaces;
using Repositories.Repository;

namespace Repositories.ServiceExtension
{
    public static class SdcrpServiceExtension
    {
        public static void InjectServices(this IServiceCollection services)
        {
            services.AddSingleton<CarBrandDAO>();
            services.AddSingleton<CarDAO>();
            services.AddSingleton<CarTypeDAO>();
            services.AddSingleton<ContractDAO>();
            services.AddSingleton<DrivingLicenseDAO>();
            services.AddSingleton<LocationDAO>();
            services.AddSingleton<UserDAO>();
            services.AddTransient<ICarBrandRepository, CarBrandRepository>();
            services.AddTransient<ICarRepository, CarRepository>();
            services.AddTransient<ICarTypeRepository, CarTypeRepository>();
            services.AddTransient<IContractRepository, ContractRepository>();
            services.AddTransient<IDrivingLicenseRepository, DrivingLicenseRepository>();
            services.AddTransient<ILocationRepository, LocationRepository>();
            services.AddTransient<IUserRepository, UserRepository>();
        }
    }
}
