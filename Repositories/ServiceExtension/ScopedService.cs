// Purpose: To add singleton services to the service collection.

using DAOs;
using Microsoft.Extensions.DependencyInjection;
using Repositories.Interfaces;
using Repositories.Repository;

namespace Repositories.ServiceExtension;

public static class ScopedService
{
    public static void AddScopedService(this IServiceCollection services)
    {
        services.AddScoped<CarBrandDAO>();
        services.AddScoped<CarDAO>();
        services.AddScoped<CarTypeDAO>();
        services.AddScoped<ContractDAO>();
        services.AddScoped<DrivingLicenseDAO>();
        services.AddScoped<LocationDAO>();
        services.AddScoped<UserDAO>();
        services.AddScoped<ICarBrandRepository, CarBrandRepository>();
        services.AddScoped<ICarRepository, CarRepository>();
        services.AddScoped<ICarTypeRepository, CarTypeRepository>();
        services.AddScoped<IContractRepository, ContractRepository>();
        services.AddScoped<IDrivingLicenseRepository, DrivingLicenseRepository>();
        services.AddScoped<ILocationRepository, LocationRepository>();
        services.AddScoped<IUserRepository, UserRepository>();

    }
}