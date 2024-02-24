using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObjects.Models;
using DAOs;

namespace SelfDrivingCarRentalPlatform.Pages.Contracts
{
    public class IndexModel : PageModel
    {
        private readonly DAOs.SdcrpDbContext _context;

        public IndexModel(DAOs.SdcrpDbContext context)
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
                .Include(c => c.Customer)
                .Include(c => c.Transaction).ToListAsync();
            }
        }
    }
}
