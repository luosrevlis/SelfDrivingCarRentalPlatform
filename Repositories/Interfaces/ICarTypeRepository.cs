using BusinessObjects.Models;

namespace Repositories.Interfaces;

public interface ICarTypeRepository
{
    IQueryable<CarType> GetAll();
    CarType GetById(int id);
    void Add(CarType carType);
    void Update(CarType carType);
    bool Remove(CarType carType);
}