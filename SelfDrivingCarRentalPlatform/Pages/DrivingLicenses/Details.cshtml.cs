using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObjects.Models;
using DAOs;
using Repositories.Repository;
using Repositories.Interfaces;

namespace SelfDrivingCarRentalPlatform.Pages.DrivingLicenses
{
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