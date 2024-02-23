using BusinessObjects.Models;
using Microsoft.EntityFrameworkCore;

namespace DAOs;

public class DrivingLicenseDAO : GenericDAO<DrivingLicense, int>
{
    public DrivingLicenseDAO(SdcrpDbContext context) : base(context)
    {
    }
    
}