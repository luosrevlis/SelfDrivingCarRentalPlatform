using BusinessObjects.Models;
using DAOs;
using Repositories.Interfaces;

namespace Repositories.Repository;

public class CarRepository : ICarRepository
{
    private CarDAO _carDAO;

    public CarRepository(CarDAO carDAO)
    {
        _carDAO = carDAO;
    }
    public IEnumerable<Car> GetAll()
    {
        return _carDAO.GetAll();
    }

    public Car GetById(int id)
    {
        return _carDAO.GetById(id);
    }

    public void Add(Car car)
    {
        _carDAO.Add(car);
    }

    public void Update(Car car)
    {
        _carDAO.Update(car);
    }

    public bool Remove(Car car)
    {
        return _carDAO.Delete(car);
    }
}