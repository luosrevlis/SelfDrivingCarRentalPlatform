using BusinessObjects.Models;

namespace Repositories.Interfaces;

public interface IUserRepository
{
    IQueryable<User> GetAll();
    User GetById(int id);
    void Add(User user);
    void Update(User user);
    bool Remove(User user);
}