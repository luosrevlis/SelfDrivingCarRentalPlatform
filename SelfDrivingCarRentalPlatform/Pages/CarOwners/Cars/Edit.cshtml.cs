using BusinessObjects.Enums;
using BusinessObjects.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Repositories.Interfaces;
using SelfDrivingCarRentalPlatform.Attributes;
using SelfDrivingCarRentalPlatform.Helper;

namespace SelfDrivingCarRentalPlatform.Pages.CarOwners.Cars
{
    [AuthorizeRole(UserRole.CarOwner)]
    public class EditModel : PageModel
    {
        private readonly ICarBrandRepository _carBrandRepository;
        private readonly ICarTypeRepository _carTypeRepository;
        private readonly ICarRepository _carRepository;

        public EditModel(
            ICarBrandRepository carBrandRepository,
            ICarTypeRepository carTypeRepository,
            ICarRepository carRepository)
        {
            _carBrandRepository = carBrandRepository;
            _carTypeRepository = carTypeRepository;
            _carRepository = carRepository;
        }

        public IActionResult OnGet(int id)
        {
            int userId = int.Parse(User.FindFirst("Id")!.Value.ToString());
            Car = _carRepository.GetById(id);
            if (Car.CarOwnerId != userId)
            {
                return BadRequest();
            }
            PreparePage();
            return Page();
        }

        [BindProperty]
        public Car Car { get; set; }

        [BindProperty]
        public IFormFile? Image { get; set; }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                PreparePage();
                return Page();
            }

            //check if the plate number already exists
            var listCar = _carRepository.GetAll().Any(c => c.Id != Car.Id && c.PlateNumber == Car.PlateNumber);
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

            Car carInDb = _carRepository.GetById(Car.Id);
            Update(Car, carInDb);
            _carRepository.Update(carInDb);

            return RedirectToPage("Index");
        }

        private void PreparePage()
        {
            ViewData["CarBrandId"] = new SelectList(_carBrandRepository.GetAll().ToList(), "Id", "BrandName");
            ViewData["CarTypeId"] = new SelectList(_carTypeRepository.GetAll(), "Id", "TypeName");
        }

        private void Update(Car source, Car target)
        {
            target.CarOwnerId = source.CarOwnerId;
            target.CarBrandId = source.CarBrandId;
            target.CarTypeId = source.CarTypeId;
            target.CarModel = source.CarModel;
            target.PlateNumber = source.PlateNumber;
            target.CarRegisterNumber = source.CarRegisterNumber;
            target.PricePerDay = source.PricePerDay;
            target.IsElectric = source.IsElectric;
            target.IsMortgageRequired = source.IsMortgageRequired;
            target.PickupLocation = source.PickupLocation;
            target.ImageBase64 = source.ImageBase64;
        }
    }
}
