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

		public DrivingLicense DrivingLicense { get; set; } = default!;

		public async Task<IActionResult> OnGetAsync(int? id)
		{
			if (id == null || _drivingLicenseRepository.GetAll() == null)
			{
				return NotFound();
			}

			var drivinglicense = _drivingLicenseRepository.GetAll().FirstOrDefault(m => m.OwnerId == id);
			if (drivinglicense == null)
			{
				return RedirectToPage("./Create");
			}
			else
			{
				drivinglicense.Owner = _userRepository.GetById(id ?? 0);
				DrivingLicense = drivinglicense;
			}
			return Page();
		}
	}
}