using BusinessObjects.Models;
using Microsoft.EntityFrameworkCore;

namespace DAOs;

public class CarDAO : GenericDAO<Car, int> 
{
    public CarDAO(SdcrpDbContext context) : base(context)
    {
    }
}