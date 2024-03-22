using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObjects.Models;
using DAOs;
using Repositories.Interfaces;

namespace SelfDrivingCarRentalPlatform.Pages.Admin.Users
{
    public class DeleteModel : PageModel
    {
        private readonly IUserRepository _userRepository;

        public DeleteModel(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [BindProperty]
      public User User { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int id)
        {

            var user = _userRepository.GetById(id);

            if (user == null)
            {
                return NotFound();
            }
            else 
            {
                User = user;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            var user = _userRepository.GetById(id);
            if (user != null && user.IsDeleted)
            {
                User = user;
                _userRepository.Unban(User);
                return RedirectToPage("./Index");
            }
            if (user != null && !(user.IsDeleted))
            {
                User = user;
                _userRepository.Ban(User);
            }

            return RedirectToPage("./Index");
        }
    }
}
