using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BusinessObjects.Models;
using Repositories.Interfaces;
using SelfDrivingCarRentalPlatform.Attributes;
using BusinessObjects.Enums;
using SelfDrivingCarRentalPlatform.Helper;

namespace SelfDrivingCarRentalPlatform.Pages.CarOwners.Cars
{
    [AuthorizeRole(UserRole.CarOwner)]
    public class CreateModel : PageModel
    {
        private readonly ICarBrandRepository _carBrandRepository;
        private readonly ICarTypeRepository _carTypeRepository;
        private readonly ICarRepository _carRepository;

        public CreateModel(
            ICarBrandRepository carBrandRepository,
            ICarTypeRepository carTypeRepository,
            ICarRepository carRepository)
        {
            _carBrandRepository = carBrandRepository;
            _carTypeRepository = carTypeRepository;
            _carRepository = carRepository;
        }

        public IActionResult OnGet()
        {
            PreparePage();
            return Page();
        }

        [BindProperty] 
        public Car Car { get; set; } = new();

        [BindProperty]
        public IFormFile? Image { get; set; }
        
        public IActionResult OnPost()
        {
            if (Image == null)
            {
                ModelState.AddModelError("Image", "You must upload an image for the car!");
            }

            if (ModelState.IsValid)
            {
                PreparePage();
                return Page();
            }

            //check if the plate number already exists
            var listCar = _carRepository.GetAll().Any(c => c.PlateNumber == Car.PlateNumber);
            if (listCar)
            {
                ModelState.AddModelError("Car.PlateNumber", "Plate number already exists");
                PreparePage();
                return Page();
            }

            //check if price per day is out of range
            if (Car.PricePerDay < 500000 || Car.PricePerDay > 10000000)
            {
                ModelState.AddModelError("Car.PricePerDay", "Price per day must be between 500000 and 10000000");
                PreparePage();
                return Page();
            }

            Car.CarOwnerId = int.Parse(User.FindFirst("Id")!.Value);

            if (Image != null)
            {
                Car.ImageBase64 = Base64Converter.ConvertToBase64(Image);
            }
            
            _carRepository.Add(Car);

            return RedirectToPage("Index");
        }

        private void PreparePage()
        {
            ViewData["CarBrandId"] = new SelectList(_carBrandRepository.GetAll().ToList(), "Id", "BrandName");
            ViewData["CarTypeId"] = new SelectList(_carTypeRepository.GetAll(), "Id", "TypeName");
        }
    }
}