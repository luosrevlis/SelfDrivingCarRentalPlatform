using BusinessObjects.Models;
using Microsoft.EntityFrameworkCore;

namespace DAOs;

public class CarDAO : GenericDAO<Car, int> 
{
    public CarDAO(DbContext context) : base(context)
    {
    }
}