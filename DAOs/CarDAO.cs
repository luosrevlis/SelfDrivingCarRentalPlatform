using BusinessObjects.Models;

namespace DAOs;

public class CarDAO : GenericDAO<Car, int> 
{
    public CarDAO(SdcrpDbContext context) : base(context)
    {
    }
}