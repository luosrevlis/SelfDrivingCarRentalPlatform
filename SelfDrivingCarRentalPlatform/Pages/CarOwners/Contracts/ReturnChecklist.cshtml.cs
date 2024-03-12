using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BusinessObjects.Models;
using System.ComponentModel.DataAnnotations;
using SelfDrivingCarRentalPlatform.Constants;
using Repositories.Interfaces;
using SelfDrivingCarRentalPlatform.Attributes;
using BusinessObjects.Enums;

namespace SelfDrivingCarRentalPlatform.Pages.CarOwners.Contracts
{
    [AuthorizeRole(UserRole.CarOwner)]
    public class ReturnChecklistModel : PageModel
    {
        private readonly IContractRepository _contractRepository;

        public ReturnChecklistModel(IContractRepository contractRepository)
        {
            _contractRepository = contractRepository;
        }

        public IActionResult OnGet(int contractId)
        {
            int userId = int.Parse(User.FindFirst("Id")!.Value);
            Contract? contract = _contractRepository.FindContractForCarReturn(contractId, userId);
            if (contract == null)
            {
                return BadRequest();
            }
            Checklist.ContractId = contractId;
            return Page();
        }

        [BindProperty]
        public ChecklistItems Checklist { get; set; } = new();

        public IActionResult OnPost()
        {
            int userId = int.Parse(User.FindFirst("Id")!.Value);
            Contract? contract = _contractRepository.FindContractForCarReturn(Checklist.ContractId, userId);
            if (contract == null)
            {
                return BadRequest();
            }
            // record fees
            contract.Transaction.DamageFee = Checklist.IsDamaged ? Checklist.DamageFee : 0;
            contract.Transaction.LateReturnPenalty = (Checklist.IsLate && Checklist.TimeExceeded > CommonConst.MaxTimeExceeded)
                ? contract.Car.PricePerDay
                : Checklist.TimeExceeded * CommonConst.TimeExceededUnitFee;
            contract.Transaction.OtherFees =
                (Checklist.IsOverused ? Checklist.DistanceExceeded * CommonConst.DistanceExceededUnitFee : 0)
                + (Checklist.NeedsCleaning ? CommonConst.CleaningFee : 0)
                + (Checklist.NeedsDeodorizing ? CommonConst.DeodorizeFee : 0);
            // change status
            contract.ContractStatus = ContractStatus.Returned;

            _contractRepository.Update(contract);
            return RedirectToPage("RentedList");
        }

        public class ChecklistItems
        {
            public int ContractId { get; set; }

            public bool IsDamaged { get; set; }

            [Range(0, int.MaxValue)]
            public int DamageFee { get; set; }

            public bool IsOverused { get; set; }

            [Range(0, 1000)]
            public int DistanceExceeded { get; set; }

            public bool IsLate { get; set; }

            [Range(0, 24)]
            public int TimeExceeded { get; set; }

            public bool NeedsCleaning { get; set; }

            public bool NeedsDeodorizing { get; set; }
        }
    }
}
