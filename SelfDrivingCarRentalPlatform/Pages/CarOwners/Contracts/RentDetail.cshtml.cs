using BusinessObjects.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Repositories.Interfaces;

namespace SelfDrivingCarRentalPlatform.Pages.CarOwners.Contracts;

public class RentDetail : PageModel
{
    private readonly IContractRepository _contractRepository;

    public RentDetail(IContractRepository contractRepository)
    {
        _contractRepository = contractRepository;
    }
    
    public Contract Contract { get; set; } = new Contract();
    public void OnGet(int contractId)
    {
        Contract = _contractRepository.GetAll()
            .Include(c => c.Car)
            .ThenInclude(car => car.CarOwner).ThenInclude(owner => owner.Location)
            .Include(c => c.Car)
            .ThenInclude(car => car.CarType)
            .Include(c => c.Car)
            .ThenInclude(car => car.CarBrand)
            .Include(c => c.Transaction)
            .FirstOrDefault(c => c.Id == contractId);
    }
}