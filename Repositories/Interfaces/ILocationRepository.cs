using BusinessObjects.Models;

namespace Repositories.Interfaces;

public interface ILocationRepository
{
    IQueryable<Location> GetAll();
    Location GetById(int id);
    void Add(Location location);
    void Update(Location location);
    bool Remove(Location location);
}