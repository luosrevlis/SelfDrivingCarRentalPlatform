using BusinessObjects.Models;
using DAOs;
using Microsoft.EntityFrameworkCore;
using Repositories.Interfaces;

namespace Repositories.Repository;

public class DrivingLicenseRepository : IDrivingLicenseRepository
{
	private DrivingLicenseDAO _drivingLicenseDAO;

	public DrivingLicenseRepository(DrivingLicenseDAO drivingLicenseDAO)
	{
		_drivingLicenseDAO = drivingLicenseDAO;
	}

	public IQueryable<DrivingLicense> GetAll()
	{
		return _drivingLicenseDAO.GetAll();
	}

	public void Add(DrivingLicense drivingLicense)
	{
		_drivingLicenseDAO.Add(drivingLicense);
	}

	public void Update(DrivingLicense drivingLicense)
	{
		_drivingLicenseDAO.Update(drivingLicense);
	}

	public bool Remove(DrivingLicense drivingLicense)
	{
		return _drivingLicenseDAO.Delete(drivingLicense);
	}
}