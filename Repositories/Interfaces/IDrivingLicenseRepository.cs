using BusinessObjects.Models;

namespace Repositories.Interfaces;

public interface IDrivingLicenseRepository
{
    IQueryable<DrivingLicense> GetAll();
    void Add(DrivingLicense drivingLicense);
    void Update(DrivingLicense drivingLicense);
    bool Remove(DrivingLicense drivingLicense);
}