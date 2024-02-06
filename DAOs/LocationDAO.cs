using BusinessObjects.Models;
using Microsoft.EntityFrameworkCore;

namespace DAOs;

public class LocationDAO : GenericDAO<Location, int>
{
    public LocationDAO(DbContext context) : base(context)
    {
    }
}