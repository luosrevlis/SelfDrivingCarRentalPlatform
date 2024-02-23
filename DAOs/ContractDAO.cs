using BusinessObjects.Models;

namespace DAOs;

public class ContractDAO : GenericDAO<Contract, int>
{
    public ContractDAO(SdcrpDbContext context) : base(context)
    {
    }
}