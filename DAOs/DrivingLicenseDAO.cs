using BusinessObjects.Models;
using Microsoft.EntityFrameworkCore;

namespace DAOs;

public class DrivingLicenseDAO : GenericDAO<DrivingLicense, int>
{
    public DrivingLicenseDAO(DbContext context) : base(context)
    {
    }
    
}