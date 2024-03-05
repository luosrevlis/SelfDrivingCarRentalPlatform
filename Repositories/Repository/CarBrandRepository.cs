using BusinessObjects.Models;
using DAOs;
using Repositories.Interfaces;

namespace Repositories.Repository;

public class CarBrandRepository : ICarBrandRepository
{
    private CarBrandDAO _carbrandDAO;

    public CarBrandRepository(CarBrandDAO carbrandDAO)
    {
        _carbrandDAO = carbrandDAO;
    }

    public IEnumerable<CarBrand> GetAll()
    {
        return _carbrandDAO.GetAll();
    }

    public CarBrand GetById(int id)
    {
        return _carbrandDAO.GetById(id);
    }

    public void Add(CarBrand carBrand)
    {
        _carbrandDAO.Add(carBrand);
    }

    public void Update(CarBrand carBrand)
    {
        _carbrandDAO.Update(carBrand);
    }

    public bool Remove(CarBrand carBrand)
    {
        return _carbrandDAO.Delete(carBrand);
    }
}