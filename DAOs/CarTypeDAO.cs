using BusinessObjects.Models;
using Microsoft.EntityFrameworkCore;

namespace DAOs;

public class CarTypeDAO : GenericDAO<CarType, int>
{
    public CarTypeDAO(SdcrpDbContext context) : base(context)
    {
    }
}