using BusinessObjects.Models;
using DAOs;
using Microsoft.EntityFrameworkCore;
using Repositories.Interfaces;
using System.Linq.Expressions;

namespace Repositories.Repository;

public class CarRepository : ICarRepository
{
    private CarDAO _carDAO;

    public CarRepository(CarDAO carDAO)
    {
        _carDAO = carDAO;
    }
    public IQueryable<Car> GetAll()
    {
        return _carDAO.GetAll()
            .Include(c => c.CarBrand)
            .Include(c => c.CarOwner)
            .Include(c => c.CarType);
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

    public ICollection<Car> GetAllByProperty(Expression<Func<Car, bool>> condition)
    {
        return _carDAO.GetListByProperty(condition);
    }
}