using BusinessObjects.Models;
using DAOs;
using Repositories.Interfaces;

namespace Repositories.Repository;

public class UserRepository : IUserRepository
{
    private UserDAO _userDAO;
    
    public UserRepository(UserDAO userDAO)
    {
        _userDAO = userDAO;
    }
    
    public IQueryable<User> GetAll()
    {
        return _userDAO.GetAll();
    }

    public User GetById(int id)
    {
        return _userDAO.GetById(id);
    }

    public void Add(User user)
    {
        _userDAO.Add(user);
    }

    public void Update(User user)
    {
        _userDAO.Update(user);
    }

    public bool Remove(User user)
    {
        return _userDAO.Delete(user);
    }

    public void Ban(User user)
    {
        user.IsDeleted = true;
        _userDAO.Update(user);
    }

    public void Unban(User user)
    {
        user.IsDeleted = false;
        _userDAO.Update(user);
    }
}