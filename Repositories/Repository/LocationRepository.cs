using BusinessObjects.Models;
using DAOs;
using Repositories.Interfaces;

namespace Repositories.Repository;

public class LocationRepository : ILocationRepository
{
    private LocationDAO _locationDAO;
    
    public LocationRepository(LocationDAO locationDAO)
    {
        _locationDAO = locationDAO;
    }
    
    public IEnumerable<Location> GetAll()
    {
        return _locationDAO.GetAll();
    }

    public void Add(Location location)
    {
        _locationDAO.Add(location);
    }

    public void Update(Location location)
    {
        _locationDAO.Update(location);
    }

    public bool Remove(Location location)
    {
        return _locationDAO.Delete(location);
    }
}