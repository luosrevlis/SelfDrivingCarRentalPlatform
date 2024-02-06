using BusinessObjects.Models;

namespace Repositories.Interfaces;

public interface ICarRepository
{
    IEnumerable<Car> GetAll();
    void Add(Car car);
    void Update(Car car);
    bool Remove(Car car);
}