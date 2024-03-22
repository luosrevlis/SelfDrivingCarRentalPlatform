using BusinessObjects.Models;

namespace Repositories.Interfaces;

public interface ICarBrandRepository
{
    IQueryable<CarBrand> GetAll();
    CarBrand GetById(int id);
    void Add(CarBrand carBrand);
    void Update(CarBrand carBrand);
    bool Remove(CarBrand carBrand);
}