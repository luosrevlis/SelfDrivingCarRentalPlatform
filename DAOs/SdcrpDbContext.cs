using BusinessObjects.Models;
using Microsoft.EntityFrameworkCore;

namespace DAOs
{
    public class SdcrpDbContext : DbContext
    {
        public SdcrpDbContext(DbContextOptions<SdcrpDbContext> options) : base(options) { }

        public DbSet<Car> Cars { get; set; }

        public DbSet<CarBrand> CarBrands { get; set; }

        public DbSet<CarType> CarTypes { get; set; }

        public DbSet<Contract> Contracts { get; set; }

        public DbSet<DrivingLicense> DrivingLicenses { get; set; }

        public DbSet<Location> Locations { get; set; }

        public DbSet<User> Users { get; set; }
    }
}
