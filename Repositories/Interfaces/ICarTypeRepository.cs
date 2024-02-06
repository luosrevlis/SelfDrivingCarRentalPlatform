using BusinessObjects.Models;

namespace Repositories.Interfaces;

public interface ICarTypeRepository
{
    IEnumerable<CarType> GetAll();
    void Add(CarType carType);
    void Update(CarType carType);
    bool Remove(CarType carType);
}