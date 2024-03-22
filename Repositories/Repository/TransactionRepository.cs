using BusinessObjects.Models;
using DAOs;
using Microsoft.EntityFrameworkCore;
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

    public double GetLateReturnFee(int id)
    {
        var transaction = _transactionDAO
            .GetAll()
            .Where(t => t.Id == id)
            .Include(t => t.Contract)
            .FirstOrDefault();
        if (transaction == null)
        {
            return 0;
        }
        var timeSinceRentCar = DateTime.Now - transaction.Contract.SignDate;
        var totalHourSinceRentCar = timeSinceRentCar.TotalHours;
        var deposit = transaction.Deposit;
        double returnFee = 0;

        if (totalHourSinceRentCar <= 1)
        {
            return 0;
        }

        var timeUntilStartDate = transaction.Contract.RentStartDate - DateTime.Now;
        if (timeUntilStartDate.TotalDays >= 1)
        {
            returnFee = Math.Ceiling(deposit * 50 / 100);
            return returnFee;
        }
        return deposit;
    }
}