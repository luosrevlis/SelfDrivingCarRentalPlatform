using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObjects.Models;
using DAOs;
using Microsoft.Identity.Client;
using Repositories.Interfaces;
using SelfDrivingCarRentalPlatform.Helper;

namespace SelfDrivingCarRentalPlatform.Pages.Profile
{
    public class DetailsModel : PageModel
    {
        private readonly IUserRepository _userRepository;

        public DetailsModel(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        } 
        [BindProperty]
        public User UserModel { get; set; }
        [BindProperty]
        public IFormFile Image { get; set; }
        [BindProperty]
      public string ImageBase64 { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            int userId = int.Parse(User.FindFirst("Id")!.Value);
            var user = _userRepository.GetById(userId);
            if (user != null)
            {
                UserModel = user;
                ImageBase64 = user.ImagePath;
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            int userId = int.Parse(User.FindFirst("Id")!.Value);
            var user = _userRepository.GetById(userId);
            Console.WriteLine(Image);
            user.Fullname = UserModel.Fullname == null ? user.Fullname : UserModel.Fullname;
            user.Address =  UserModel.Address == null ? user.Address : UserModel.Address;
            user.Email = UserModel.Email == null ? user.Email : UserModel.Email;
            user.Phone = UserModel.Phone == null ? user.Phone : UserModel.Phone;
            if (Image != null)
            {
                var imageBase64 = Base64Converter.ConvertToBase64(Image);
                user.ImagePath = imageBase64;
            }

            _userRepository.Update(user);
            return RedirectToPage("/Profile/Details");
        }
    }
}
