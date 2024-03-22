using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BusinessObjects.Models;
using Repositories.Interfaces;
using SelfDrivingCarRentalPlatform.Attributes;
using BusinessObjects.Enums;

namespace SelfDrivingCarRentalPlatform.Pages.DrivingLicenses
{
	[AuthorizeRole(UserRole.Customer, UserRole.CarOwner)]
    public class DetailsModel : PageModel
	{
		private readonly IDrivingLicenseRepository _drivingLicenseRepository;
		private readonly IUserRepository _userRepository;

		public DetailsModel(IDrivingLicenseRepository drivingLicenseRepository, IUserRepository userRepository)
		{
			_drivingLicenseRepository = drivingLicenseRepository;
			_userRepository = userRepository;
		}

		public DrivingLicense DrivingLicense { get; set; } = new();

		public IActionResult OnGet()
		{
			int userId = int.Parse(User.FindFirst("Id")!.Value.ToString());
            var drivinglicense = _drivingLicenseRepository.GetAll().FirstOrDefault(m => m.OwnerId == userId);
			if (drivinglicense == null)
			{
				return RedirectToPage("Create");
			}
            drivinglicense.Owner = _userRepository.GetById(userId);
            DrivingLicense = drivinglicense;
            return Page();
		}
	}
}