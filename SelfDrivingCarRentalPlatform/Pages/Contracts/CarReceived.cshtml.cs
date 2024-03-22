using BusinessObjects.Enums;
using BusinessObjects.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repositories.Interfaces;

namespace SelfDrivingCarRentalPlatform.Pages.Contracts
{
    public class CarReceivedModel : PageModel
    {
        private readonly IContractRepository _contractRepository;

        public CarReceivedModel(IContractRepository contractRepository)
        {
            _contractRepository = contractRepository;
        }

        [BindProperty]
        public string ErrorMsg { get; set; } 
        public IActionResult OnGet(int id)
        {
            int userId = int.Parse(User.FindFirst("Id")!.Value);
            Contract? contract = _contractRepository.FindContractForCarReceive(id, userId);
            if (contract == null)
            {
                return BadRequest();
            }
            if (!checkTimeForCancel(contract))
            {
                ModelState.AddModelError(string.Empty, "Can not receive the car when not in the right day");
                return Page();
            }
            contract.ContractStatus = ContractStatus.Received;
            _contractRepository.Update(contract);
            return RedirectToPage("Index");
        }
        
        // true means the startTime is today
        private bool checkTimeForCancel(Contract contract)
        {
            if (contract.RentStartDate == DateTime.Now)
            {
                return true;
            }

            return false;
        }
    }
}
