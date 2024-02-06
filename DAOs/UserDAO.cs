using BusinessObjects.Models;
using Microsoft.EntityFrameworkCore;

namespace DAOs;

public class UserDAO : GenericDAO<User, int>
{
    public UserDAO(DbContext context) : base(context)
    {
    }
}