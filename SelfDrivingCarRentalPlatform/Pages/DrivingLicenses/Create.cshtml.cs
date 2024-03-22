using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BusinessObjects.Models;
using Repositories.Interfaces;
using SelfDrivingCarRentalPlatform.Attributes;
using BusinessObjects.Enums;

namespace SelfDrivingCarRentalPlatform.Pages.DrivingLicenses
{
    [AuthorizeRole(UserRole.Customer, UserRole.CarOwner)]
	public class CreateModel : PageModel
	{
		private readonly IDrivingLicenseRepository _drivingLicenseRepository;
		private readonly IUserRepository _userRepository;

		public CreateModel(IDrivingLicenseRepository drivingLicenseRepository, IUserRepository userRepository)
		{
			_drivingLicenseRepository = drivingLicenseRepository;
			_userRepository = userRepository;
		}

		public IActionResult OnGet()
		{
			return Page();
		}

		[BindProperty]
		public DrivingLicense DrivingLicense { get; set; } = new();

		public IActionResult OnPost()
		{
			if (!ModelState.IsValid)
			{
				return Page();
			}

			if (_drivingLicenseRepository.GetAll().Any(d => d.DrivingLicenseNumber.Equals(DrivingLicense.DrivingLicenseNumber)))
			{
				ModelState.AddModelError(string.Empty, "Duplicated Driving License Number. Please try again.");
				return Page();
			}
			DrivingLicense.OwnerId = int.Parse(User.FindFirst("Id")!.Value.ToString());
			DrivingLicense.Owner = _userRepository.GetById(DrivingLicense.OwnerId);
			_drivingLicenseRepository.Add(DrivingLicense);

			return RedirectToPage("../Index");
		}
	}
}