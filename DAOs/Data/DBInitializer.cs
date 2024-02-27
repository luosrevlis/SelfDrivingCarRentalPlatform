using BusinessObjects.Enums;
using BusinessObjects.Models;

namespace DAOs.Data;

public static class DBInitializer
{
    public static async Task Initialize(SdcrpDbContext context)
    {
        if (!context.CarBrands.Any())
        {
            var carBrands = new List<CarBrand>
            {
                new CarBrand { BrandName = "Toyota", IsDeleted = false },
                new CarBrand { BrandName = "Honda", IsDeleted = false },
                new CarBrand { BrandName = "Ford", IsDeleted = false },
                new CarBrand { BrandName = "BMW", IsDeleted = false },
                new CarBrand { BrandName = "Mercedes", IsDeleted = false }
            };

            foreach (var carBrand in carBrands)
            {
                await context.CarBrands.AddAsync(carBrand);
            }
        }

        await context.SaveChangesAsync();

        if (!context.Locations.Any())
        {
            var locations = new List<Location>
            {
                new Location { LocationName = "Hanoi", IsDeleted = false},
                new Location { LocationName = "HCM", IsDeleted = false },
                new Location { LocationName = "Da Nang", IsDeleted = false }
            };
            
            foreach (var location in locations)
            {
                await context.Locations.AddAsync(location);
            }
        }

        await context.SaveChangesAsync();

        if (!context.CarTypes.Any())
        {
            var carTypes = new List<CarType>
            {
                new CarType { TypeName = "Xe bốn chỗ", IsDeleted = false },
                new CarType { TypeName = "Xe bảy chỗ", IsDeleted = false },
            };

            foreach (var carType in carTypes)
            {
                await context.CarTypes.AddAsync(carType);
            }
        }
        
        await context.SaveChangesAsync();

        if (!context.Users.Any())
        {
            var users = new List<User>()
            {
                new User
                {
                    LocationId = 2,
                    Fullname = "admin",
                    Email = "admin@gmail.com",
                    Password = "admin", 
                    Phone = "0987654321",
                    Address = "Quận 1, TPHCM",
                    Role = UserRole.Admin,
                    IsDeleted = false,
                    DrivingLicense = new DrivingLicense()
                    {
                        DrivingLicenseNumber = "123456789",
                        ExpiryDate = new DateTime(2026,1,1), 
                        IsDeleted = false 
                    }
                        
                },
                
                new User
                {
                    LocationId = 2,
                    Fullname = "John Smith",
                    Email = "customer1@gmail.com",
                    Password = "aaa", 
                    Phone = "0987654322",
                    Address = "Quận 2, TPHCM", 
                    Role = UserRole.Customer,
                    IsDeleted = false,
                    DrivingLicense = new DrivingLicense()
                    {
                        DrivingLicenseNumber = "987654321",
                        ExpiryDate = new DateTime(2026,1,1),
                        IsDeleted = false
                    }
                },
                
                
            };

            foreach (var user in users)
            {
                await context.Users.AddAsync(user);
            }
            
        }
        
        await context.SaveChangesAsync();

        if (!context.Cars.Any())
            {
                var cars = new List<Car>()
                {
                    new Car
                    {
                        CarBrandId = 1,
                        CarTypeId = 1,
                        CarOwnerId = 2,
                        CarModel = "Toyota Vios",
                        PlateNumber = "51A-12345",
                        CarRegisterNumber = "012345",
                        PricePerDay = 2000,
                        IsElectric = false,
                        IsMortgageRequired = true,
                        DepositRatio = 15,
                        IsDeleted = false
                    },
                    new Car
                    {
                        CarBrandId = 2,
                        CarTypeId = 2,
                        CarOwnerId = 2,
                        CarModel = "Honda CRV",
                        PlateNumber = "51A-54321",
                        CarRegisterNumber = "123456",
                        PricePerDay = 1500,
                        IsElectric = false,
                        IsMortgageRequired = false,
                        DepositRatio = 20,
                        IsDeleted = false
                    }
                };
    
                foreach (var car in cars)
                {
                    await context.Cars.AddAsync(car);
                }
            }
        
        await context.SaveChangesAsync();
    }
    
        
}