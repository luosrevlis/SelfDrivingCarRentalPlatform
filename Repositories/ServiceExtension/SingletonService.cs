// Purpose: To add singleton services to the service collection.
using Microsoft.Extensions.DependencyInjection;
using Repositories.Interfaces;
using Repositories.Repository;

namespace Repositories.ServiceExtension;

public static class SingletonService
{
    public static void AddSingletonService(this IServiceCollection services)
    {
        services.AddSingleton<ICarBrandRepository, CarBrandRepository>();
        services.AddSingleton<ICarRepository, CarRepository>();
        services.AddSingleton<ICarTypeRepository, CarTypeRepository>();
        services.AddSingleton<IContractRepository, ContractRepository>();
        services.AddSingleton<IDrivingLicenseRepository, DrivingLicenseRepository>();
        services.AddSingleton<ILocationRepository, LocationRepository>();
        services.AddSingleton<IUserRepository, UserRepository>();

    }
}