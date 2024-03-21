using BusinessObjects.Models;
using System.Linq.Expressions;

namespace Repositories.Interfaces;

public interface IContractRepository
{
    IQueryable<Contract> GetAll();
    void Add(Contract contract);
    void Update(Contract contract);
    bool Remove(Contract contract);
    ICollection<Contract> GetAllByProperty(Expression<Func<Contract, bool>> condition);
    Contract? FindContractForCarReturn(int id, int carOwnerId);
    Contract? FindContractForCarReceive(int id, int customerId);
    List<Contract> GetRentedHistory(int carOwnerId);
    List<Contract> GetRentingHistory(int customerId);
    Contract GetById(int contractId);
}