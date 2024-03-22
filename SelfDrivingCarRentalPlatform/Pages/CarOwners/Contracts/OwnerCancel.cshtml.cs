using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BusinessObjects.Models;
using Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace SelfDrivingCarRentalPlatform.Pages.CarOwners.Contracts
{
    public class OwnerCancelModel : PageModel
    {
        private readonly IContractRepository _contractRepository;
        private readonly ITransactionRepository _transactionRepository;

        public OwnerCancelModel(IContractRepository contractRepository, ITransactionRepository transactionRepository)
        {
            _contractRepository = contractRepository;
            _transactionRepository = transactionRepository;
        }

        [BindProperty]
        public Contract Contract { get; set; } = new();

        public IActionResult OnGet(int id)
        {
            int userId = int.Parse(User.FindFirst("Id")!.Value);
            var contract = _contractRepository
                .GetAll()
                .Include(contract => contract.Car)
                .FirstOrDefault(contract => contract.Id == id && !contract.IsDeleted);
            if (contract == null || contract.Car.CarOwnerId != userId)
            {
                return NotFound();
            }
            contract.Transaction = _transactionRepository
                .GetAll()
                .FirstOrDefault(transaction => transaction.Id == contract.Id)!;
            contract.Transaction.CancelRentPenalty = _transactionRepository.GetLateReturnFee(contract.Id);
            Contract = contract;
            return Page();
        }

        public IActionResult OnPost(int id)
        {
            int userId = int.Parse(User.FindFirst("Id")!.Value);
            var contract = _contractRepository
                .GetAll()
                .Include(contract => contract.Car)
                .FirstOrDefault(contract => contract.Id == id && !contract.IsDeleted);
            if (contract == null || contract.Car.CarOwnerId != userId)
            {
                return NotFound();
            }
            contract.Transaction = _transactionRepository
                .GetAll()
                .FirstOrDefault(transaction => transaction.Id == contract.Id)!;
            contract.Transaction.CancelRentPenalty = -_transactionRepository.GetLateReturnFee(contract.Id);
            contract.IsDeleted = true;
            _contractRepository.Update(contract);

            return RedirectToPage("./RentedList");
        }
    }
}
