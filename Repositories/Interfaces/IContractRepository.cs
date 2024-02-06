using BusinessObjects.Models;

namespace Repositories.Interfaces;

public interface IContractRepository
{
    IEnumerable<Contract> GetAll();
    void Add(Contract contract);
    void Update(Contract contract);
    bool Remove(Contract contract);
}