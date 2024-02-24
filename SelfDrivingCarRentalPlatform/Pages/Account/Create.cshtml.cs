using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BusinessObjects.Models;
using DAOs;

namespace SelfDrivingCarRentalPlatform.Pages.Account
{
    public class CreateModel : PageModel
    {
        private readonly DAOs.SdcrpDbContext _context;

        public CreateModel(DAOs.SdcrpDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            ViewData["LocationId"] = new SelectList(_context.Locations, "Id", "LocationName");
            return Page();
        }

        [BindProperty]
        public User User { get; set; } = default!;
        
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.Users == null || User == null)
            {
                return Page();
            }

            _context.Users.Add(User);
            await _context.SaveChangesAsync();

            return RedirectToPage("/Index");
        }
    }
}
