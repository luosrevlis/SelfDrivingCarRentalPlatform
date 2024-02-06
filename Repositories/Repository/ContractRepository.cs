using BusinessObjects.Models;
using DAOs;
using Repositories.Interfaces;

namespace Repositories.Repository;

public class ContractRepository : IContractRepository
{
    private ContractDAO _contractDAO;

    public ContractRepository(ContractDAO contractDAO)
    {
        _contractDAO = contractDAO;
    }
    
    public IEnumerable<Contract> GetAll()
    {
        return _contractDAO.GetAll();
    }

    public void Add(Contract contract)
    {
        _contractDAO.Add(contract);
    }

    public void Update(Contract contract)
    {
        _contractDAO.Update(contract);
    }

    public bool Remove(Contract contract)
    {
        return _contractDAO.Delete(contract);
    }
}