using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BusinessObjects.Models;
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
        public User NewUser { get; set; } = new();

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid || NewUser == null)
            {
                return OnGet();
            }

            _userRepository.Add(NewUser);

            return RedirectToPage("../Index");
        }
    }
}
