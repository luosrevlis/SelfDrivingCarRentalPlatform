using BusinessObjects.Models;
using Microsoft.EntityFrameworkCore;

namespace DAOs;

public class UserDAO : GenericDAO<User, int>
{
    public UserDAO(SdcrpDbContext context) : base(context)
    {
    }
}