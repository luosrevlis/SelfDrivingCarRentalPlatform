using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessObjects.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObjects.Models;
using DAOs;
using Repositories.Interfaces;
using SelfDrivingCarRentalPlatform.Attributes;

namespace SelfDrivingCarRentalPlatform.Pages.Contracts
{
    [AuthorizeRole(UserRole.Customer, UserRole.CarOwner)]
    public class DetailsModel : PageModel
    {
        private readonly IContractRepository _contractRepository;

        public DetailsModel(IContractRepository contractRepository)
        {
            _contractRepository = contractRepository;
        }

      public Contract Contract { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var contract = _contractRepository.GetById(id);
            Contract = contract;
            return Page();
        }
    }
}
