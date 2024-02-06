using BusinessObjects.Models;

namespace Repositories.Interfaces;

public interface ICarBrandRepository
{
    IEnumerable<CarBrand> GetAll();
    void Add(CarBrand carBrand);
    void Update(CarBrand carBrand);
    bool Remove(CarBrand carBrand);
}