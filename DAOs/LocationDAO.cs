using BusinessObjects.Models;

namespace DAOs;

public class LocationDAO : GenericDAO<Location, int>
{
    public LocationDAO(SdcrpDbContext context) : base(context)
    {
    }
}