using BusinessObjects.Models;

namespace DAOs;

public class UserDAO : GenericDAO<User, int>
{
    public UserDAO(SdcrpDbContext context) : base(context)
    {
    }
}