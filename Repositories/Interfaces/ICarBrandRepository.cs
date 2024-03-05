using BusinessObjects.Models;

namespace Repositories.Interfaces;

public interface ICarBrandRepository
{
    IEnumerable<CarBrand> GetAll();
    CarBrand GetById(int id);
    void Add(CarBrand carBrand);
    void Update(CarBrand carBrand);
    bool Remove(CarBrand carBrand);
}