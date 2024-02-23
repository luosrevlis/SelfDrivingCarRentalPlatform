using BusinessObjects.Models;
using Microsoft.EntityFrameworkCore;

namespace DAOs;

public class CarBrandDAO : GenericDAO<CarBrand, int>
{
    public CarBrandDAO(SdcrpDbContext context) : base(context)
    {
    }
}