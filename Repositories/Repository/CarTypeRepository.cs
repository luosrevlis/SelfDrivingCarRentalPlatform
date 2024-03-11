using BusinessObjects.Models;
using DAOs;
using Repositories.Interfaces;

namespace Repositories.Repository;

public class CarTypeRepository : ICarTypeRepository
{
    private CarTypeDAO _carTypeDAO;
    
    public CarTypeRepository(CarTypeDAO carTypeDao)
    {
        _carTypeDAO = carTypeDao;
    }
    
    public IEnumerable<CarType> GetAll()
    {
        return _carTypeDAO.GetAll();
    }

    public CarType GetById(int id)
    {
        return _carTypeDAO.GetById(id);
    }

    public void Add(CarType carType)
    {
        _carTypeDAO.Add(carType);
    }

    public void Update(CarType carType)
    {
        _carTypeDAO.Update(carType);
    }

    public bool Remove(CarType carType)
    {
        return _carTypeDAO.Delete(carType);
    }
}