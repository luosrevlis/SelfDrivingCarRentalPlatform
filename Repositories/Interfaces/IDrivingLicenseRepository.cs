using BusinessObjects.Models;

namespace Repositories.Interfaces;

public interface IDrivingLicenseRepository
{
    IEnumerable<DrivingLicense> GetAll();
    void Add(DrivingLicense drivingLicense);
    void Update(DrivingLicense drivingLicense);
    bool Remove(DrivingLicense drivingLicense);
}