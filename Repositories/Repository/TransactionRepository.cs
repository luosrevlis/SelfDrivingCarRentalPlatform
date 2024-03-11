using BusinessObjects.Models;
using DAOs;
using Repositories.Interfaces;

namespace Repositories.Repository;

public class TransactionRepository : ITransactionRepository
{
    private TransactionDAO _transactionDAO;

    public TransactionRepository(TransactionDAO transactionDao)
    {
        _transactionDAO = transactionDao;
    }
    public IQueryable<Transaction> GetAll()
    {
        return _transactionDAO.GetAll();
    }

    public void Add(Transaction transaction)
    {
        _transactionDAO.Add(transaction);
    }

    public void Update(Transaction transaction)
    {
        _transactionDAO.Update(transaction);
    }

    public bool Remove(Transaction transaction)
    {
        return _transactionDAO.Delete(transaction);
    }
}