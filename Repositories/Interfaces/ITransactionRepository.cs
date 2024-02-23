using BusinessObjects.Models;

namespace Repositories.Interfaces;

public interface ITransactionRepository
{
    IEnumerable<Transaction> GetAll();
    void Add(Transaction transaction);
    void Update(Transaction transaction);
    bool Remove(Transaction transaction);
}