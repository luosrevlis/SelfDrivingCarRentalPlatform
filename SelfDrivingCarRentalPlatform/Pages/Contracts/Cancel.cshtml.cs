using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BusinessObjects.Models;
using Repositories.Interfaces;
using SelfDrivingCarRentalPlatform.Attributes;
using BusinessObjects.Enums;

namespace SelfDrivingCarRentalPlatform.Pages.Contracts
{
    [AuthorizeRole(UserRole.Customer, UserRole.CarOwner)]
    public class CancelModel : PageModel
    {
        private readonly IContractRepository _contractRepository;
        private readonly ITransactionRepository _transactionRepository;

        public CancelModel(IContractRepository contractRepository, ITransactionRepository transactionRepository)
        {
            _contractRepository = contractRepository;
            _transactionRepository = transactionRepository;
        }

        [BindProperty]
        public Contract Contract { get; set; } = new();
        [BindProperty]
        public string ErrorMsg { get; set; } 

        public IActionResult OnGet(int id)
        {
            int userId = int.Parse(User.FindFirst("Id")!.Value);
            var contract = _contractRepository
                .GetAll()
                .FirstOrDefault(contract => contract.Id == id && !contract.IsDeleted);
            if (contract == null || contract.CustomerId != userId)
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
                .FirstOrDefault(contract => contract.Id == id && !contract.IsDeleted);
            if (contract == null || contract.CustomerId != userId)
            {
                return NotFound();
            }
            contract.Transaction = _transactionRepository
                .GetAll()
                .FirstOrDefault(transaction => transaction.Id == contract.Id)!;
            contract.Transaction.CancelRentPenalty = _transactionRepository.GetLateReturnFee(contract.Id);
            if (checkTimeForCancel(contract))
            {
                ModelState.AddModelError(string.Empty, "Can not cancel the reting that the StartTime < Today");
                return OnGet(contract.Id);
            }
            contract.IsDeleted = true;
            _contractRepository.Update(contract);
            return RedirectToPage("Index");
        }

        // true means the startTime has started
        private bool checkTimeForCancel(Contract contract)
        {
            if (contract.RentStartDate <= DateTime.Now)
            {
                return true;
            }

            return false;
        }
    }
}
