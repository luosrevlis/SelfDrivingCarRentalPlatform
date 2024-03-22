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

        public IActionResult OnGet(int id)
        {
            int userId = int.Parse(User.FindFirst("Id")!.Value);
            Contract? contract = _contractRepository.FindContractForCarReceive(id, userId);
            if (contract == null)
            {
                return BadRequest();
            }
            contract.ContractStatus = ContractStatus.Received;
            _contractRepository.Update(contract);
            return RedirectToPage("Index");
        }
    }
}
