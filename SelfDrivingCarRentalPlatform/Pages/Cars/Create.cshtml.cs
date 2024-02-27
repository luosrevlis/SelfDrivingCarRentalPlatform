using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BusinessObjects.Models;
using DAOs;

namespace SelfDrivingCarRentalPlatform.Pages.Cars
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
        ViewData["CarBrandId"] = new SelectList(_context.CarBrands, "Id", "BrandName");
        ViewData["CarOwnerId"] = new SelectList(_context.Users, "Id", "Email");
        ViewData["CarTypeId"] = new SelectList(_context.CarTypes, "Id", "TypeName");
            return Page();
        }

        [BindProperty]
        public Car Car { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.Cars == null || Car == null)
            {
                return Page();
            }

            _context.Cars.Add(Car);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
