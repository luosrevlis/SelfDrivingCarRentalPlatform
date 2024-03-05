using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BusinessObjects.Models;
using DAOs;
using Repositories.Interfaces;

namespace SelfDrivingCarRentalPlatform.Pages.Account
{
    public class CreateModel : PageModel
    {
        private readonly IUserRepository _userRepository;
        private readonly ILocationRepository _locationRepository;

        public CreateModel(IUserRepository userRepository, ILocationRepository locationRepository)
        {
            _userRepository = userRepository;
            _locationRepository = locationRepository;
        }

        public IActionResult OnGet()
        {
            ViewData["LocationId"] = new SelectList(_locationRepository.GetAll(), "Id", "LocationName");
            return Page();
        }

        [BindProperty]
        public User User { get; set; } = default!;
        
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || User == null)
            {
                return Page();
            }

            _userRepository.Add(User);

            return RedirectToPage("/Index");
        }
    }
}
