using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BusinessObjects.Models;
using Repositories.Interfaces;
using SelfDrivingCarRentalPlatform.Attributes;
using BusinessObjects.Enums;

namespace SelfDrivingCarRentalPlatform.Pages.CarOwners.Cars
{
    [AuthorizeRole(UserRole.CarOwner)]
    public class DetailsModel : PageModel
    {
        private readonly ICarRepository _carRepository;
        private readonly ICarBrandRepository _carBrandRepository;
        private readonly ICarTypeRepository _carTypeRepository;

        public DetailsModel(
            ICarRepository repository,
            ICarBrandRepository carBrandRepository,
            ICarTypeRepository carTypeRepository)
        {
            _carRepository = repository;
            _carBrandRepository = carBrandRepository;
            _carTypeRepository = carTypeRepository;
        }

        public Car Car { get; set; } = new();

        public IActionResult OnGet(int? id)
        {
            Car = _carRepository.GetById(id ?? -1);
            if (Car == null)
            {
                return NotFound();
            }
            Car.CarBrand = _carBrandRepository.GetById(Car.CarBrandId);
            Car.CarType = _carTypeRepository.GetById(Car.CarTypeId);
            return Page();
        }
    }
}
