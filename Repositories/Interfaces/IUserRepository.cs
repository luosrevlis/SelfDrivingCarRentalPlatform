using BusinessObjects.Models;

namespace Repositories.Interfaces;

public interface IUserRepository
{
    IEnumerable<User> GetAll();
    void Add(User user);
    void Update(User user);
    bool Remove(User user);
}