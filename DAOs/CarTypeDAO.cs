using BusinessObjects.Models;
using Microsoft.EntityFrameworkCore;

namespace DAOs;

public class CarTypeDAO : GenericDAO<CarType, int>
{
    public CarTypeDAO(DbContext context) : base(context)
    {
    }
}