using BusinessObjects.Models;

namespace DAOs;

public class TransactionDAO : GenericDAO<Transaction, int>
{
    public TransactionDAO(SdcrpDbContext context) : base(context)
    {
    }
}