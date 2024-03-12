using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BusinessObjects.Models;
using Repositories.Interfaces;
using BusinessObjects.Enums;

namespace SelfDrivingCarRentalPlatform.Pages.Contracts
{
    public class IndexModel : PageModel
    {
        private readonly IContractRepository _contractRepository;

        public IndexModel(IContractRepository contractRepository)
        {
            _contractRepository = contractRepository;
        }

        public IList<Contract> RentingList { get; set; } = new List<Contract>();

        public void OnGet()
        {
            int userId = int.Parse(User.FindFirst("Id")!.Value);
            RentingList = _contractRepository.GetRentingHistory(userId);
        }

        public IActionResult OnPost(int id)
        {
            int userId = int.Parse(User.FindFirst("Id")!.Value);
            Contract? contract = _contractRepository.FindContractForCarReceive(id, userId);
            if (contract == null)
            {
                return BadRequest();
            }
            contract.ContractStatus = ContractStatus.Received;
            _contractRepository.Update(contract);
            RentingList = _contractRepository.GetRentingHistory(userId);
            return Page();
        }
    }
}
