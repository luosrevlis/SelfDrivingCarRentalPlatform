using BusinessObjects.Models;

namespace DAOs;

public class DrivingLicenseDAO : GenericDAO<DrivingLicense, int>
{
    public DrivingLicenseDAO(SdcrpDbContext context) : base(context)
    {
    }
    
}