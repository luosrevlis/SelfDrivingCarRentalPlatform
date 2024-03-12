using Microsoft.AspNetCore.Mvc.RazorPages;
using BusinessObjects.Models;
using SelfDrivingCarRentalPlatform.Attributes;
using BusinessObjects.Enums;
using Repositories.Interfaces;

namespace SelfDrivingCarRentalPlatform.Pages.CarOwners.Contracts
{
    [AuthorizeRole(UserRole.CarOwner)]
    public class RentedListModel : PageModel
    {
        private readonly IContractRepository _contractRepository;

        public RentedListModel(IContractRepository contractRepository)
        {
            _contractRepository = contractRepository;
        }

        public IList<Contract> RentedList { get; set; } = new List<Contract>();

        public void OnGet()
        {
            int userId = int.Parse(User.FindFirst("Id")!.Value);
            RentedList = _contractRepository.GetRentedHistory(userId);
        }
    }
}
