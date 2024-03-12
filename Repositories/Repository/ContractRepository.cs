using BusinessObjects.Models;
using DAOs;
using Repositories.Interfaces;
using System.Linq.Expressions;
using BusinessObjects.Enums;
using Microsoft.EntityFrameworkCore;

namespace Repositories.Repository;

public class ContractRepository : IContractRepository
{
    private ContractDAO _contractDAO;

    public ContractRepository(ContractDAO contractDAO)
    {
        _contractDAO = contractDAO;
    }
    
    public IQueryable<Contract> GetAll()
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

    public ICollection<Contract> GetAllByProperty(Expression<Func<Contract, bool>> condition)
    {
         return _contractDAO.GetListByProperty(condition);
    }

    public Contract? FindContractForCarReturn(int id, int carOwnerId)
    {
        return _contractDAO.GetAll()
            .Include(contract => contract.Car)
            .Include(contract => contract.Transaction)
            .FirstOrDefault(contract => contract.Id == id
            && contract.Car.CarOwnerId == carOwnerId
            && !contract.IsDeleted
            && contract.ContractStatus == ContractStatus.Received);
    }

    public Contract? FindContractForCarReceive(int id, int customerId)
    {
        return _contractDAO.GetAll()
            .FirstOrDefault(contract => contract.Id == id
            && contract.CustomerId == customerId
            && !contract.IsDeleted
            && contract.ContractStatus == ContractStatus.Signed);
    }

    public List<Contract> GetRentedHistory(int carOwnerId)
    {
        return _contractDAO
            .GetAll()
            .Include(contract => contract.Car)
            .Where(contract => contract.Car.CarOwnerId == carOwnerId && !contract.IsDeleted)
            .Include(contract => contract.Customer)
            .OrderByDescending(contract => contract.SignDate)
            .ToList();
    }

    public List<Contract> GetRentingHistory(int customerId)
    {
        return _contractDAO
            .GetAll()
            .Where(contract => contract.CustomerId == customerId && !contract.IsDeleted)
            .Include(contract => contract.Car)
            .ThenInclude(car => car.CarOwner)
            .OrderByDescending(contract => contract.SignDate)
            .ToList();
    }
}