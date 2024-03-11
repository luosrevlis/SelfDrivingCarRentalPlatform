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

namespace SelfDrivingCarRentalPlatform.Pages.DrivingLicenses
{
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
		public DrivingLicense DrivingLicense { get; set; } = default!;

		// To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
		public async Task<IActionResult> OnPostAsync()
		{
			if (!ModelState.IsValid || _drivingLicenseRepository.GetAll() == null || DrivingLicense == null)
			{
				return Page();
			}

			if (_drivingLicenseRepository.GetAll().Any(d => d.DrivingLicenseNumber.Equals(DrivingLicense.DrivingLicenseNumber)))
			{
				ModelState.AddModelError(string.Empty, "Duplicated Driving License Number. Please try again.");

				return Page();
			}
			DrivingLicense.OwnerId = int.Parse(User.FindFirst("Id").Value.ToString());
			DrivingLicense.Owner = _userRepository.GetById(DrivingLicense.OwnerId);
			_drivingLicenseRepository.Add(DrivingLicense);

			return RedirectToPage("/Index");
		}
	}
}