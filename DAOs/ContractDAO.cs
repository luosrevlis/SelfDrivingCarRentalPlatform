using BusinessObjects.Models;
using Microsoft.EntityFrameworkCore;

namespace DAOs;

public class ContractDAO : GenericDAO<Contract, int>
{
    public ContractDAO(DbContext context) : base(context)
    {
    }
}