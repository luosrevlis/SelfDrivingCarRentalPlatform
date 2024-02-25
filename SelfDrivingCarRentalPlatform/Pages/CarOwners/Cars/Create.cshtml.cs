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

namespace SelfDrivingCarRentalPlatform.Pages.CarOwners.Cars
{
    public class CreateModel : PageModel
    {
        private readonly ICarBrandRepository _carBrandRepository;
        private readonly IUserRepository _userRepository;
        private readonly ICarTypeRepository _carTypeRepository;
        private readonly ICarRepository _carRepository;

        public CreateModel(ICarBrandRepository carBrandRepository,
             ICarTypeRepository carTypeRepository, ICarRepository carRepository)
        {
            _carBrandRepository = carBrandRepository;
            _carTypeRepository = carTypeRepository;
            _carRepository = carRepository;
        }

        public IActionResult OnGet()
        {
        ViewData["CarBrandId"] = new SelectList(_carBrandRepository.GetAll().ToList(), "Id", "BrandName");
        ViewData["CarTypeId"] = new SelectList(_carTypeRepository.GetAll(), "Id", "TypeName");
            return Page();
        }

        [BindProperty]
        public Car Car { get; set; } = default!;
        
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _carRepository.GetAll() == null || Car == null)
            {
                ViewData["CarBrandId"] = new SelectList(_carBrandRepository.GetAll().ToList(), "Id", "BrandName");
                ViewData["CarTypeId"] = new SelectList(_carTypeRepository.GetAll(), "Id", "TypeName");
                return Page();
            }
          
            //check if the plate number already exists
            var listCar = _carRepository.GetAll().Any(c => c.PlateNumber == Car.PlateNumber);
            if (listCar)
            {
                ModelState.AddModelError("Car.PlateNumber", "Plate number already exists");
                ViewData["CarBrandId"] = new SelectList(_carBrandRepository.GetAll().ToList(), "Id", "BrandName");
                ViewData["CarTypeId"] = new SelectList(_carTypeRepository.GetAll(), "Id", "TypeName");
                return Page();
            }
            
            //check if price per day is out of range
            if (Car.PricePerDay < 500 || Car.PricePerDay > 10000)
            {
                ModelState.AddModelError("Car.PricePerDay", "Price per day must be between 500 and 10000");
                ViewData["CarBrandId"] = new SelectList(_carBrandRepository.GetAll().ToList(), "Id", "BrandName");
                ViewData["CarTypeId"] = new SelectList(_carTypeRepository.GetAll(), "Id", "TypeName");
                return Page();
            }
          
            var carOwnerId = int.Parse(User.FindFirst("Id").Value);
            Car.CarOwnerId = carOwnerId;

            _carRepository.Add(Car);

            return RedirectToPage("./Index");
        }
    }
}
