using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObjects.Models;
using DAOs;

namespace SelfDrivingCarRentalPlatform.Pages.CarOwners.Contracts
{
    public class RentedListModel : PageModel
    {
        private readonly DAOs.SdcrpDbContext _context;

        public RentedListModel(DAOs.SdcrpDbContext context)
        {
            _context = context;
        }

        public IList<Contract> Contract { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Contracts != null)
            {
                Contract = await _context.Contracts
                .Include(c => c.Car)
                .Include(c => c.Customer).ToListAsync();
            }
        }
    }
}
