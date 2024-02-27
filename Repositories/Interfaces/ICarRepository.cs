using BusinessObjects.Models;
using System.Linq.Expressions;

namespace Repositories.Interfaces;

public interface ICarRepository
{
    IEnumerable<Car> GetAll();
    Car GetById(int id);
    void Add(Car car);
    void Update(Car car);
    bool Remove(Car car);
    ICollection<Car> GetAllByProperty(Expression<Func<Car, bool>> condition);
}