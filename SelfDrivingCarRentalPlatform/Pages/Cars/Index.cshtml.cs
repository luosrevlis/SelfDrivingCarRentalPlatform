using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObjects.Models;
using Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;
using SelfDrivingCarRentalPlatform.Constants;
using SelfDrivingCarRentalPlatform.Attributes;
using BusinessObjects.Enums;

namespace SelfDrivingCarRentalPlatform.Pages.Cars
{
    [AuthorizeRole(UserRole.Customer, UserRole.CarOwner)]
    public class IndexModel : PageModel
    {
        private readonly ICarRepository _carRepository;
        private readonly IContractRepository _contractRepository;
        private readonly ICarBrandRepository _carBrandRepository;
        private readonly ICarTypeRepository _carTypeRepository;

        public IndexModel(ICarRepository carRepository, IContractRepository contractRepository,
            ICarBrandRepository carBrandRepository, ICarTypeRepository carTypeRepository)
        {
            _carRepository = carRepository;
            _contractRepository = contractRepository;
            _carBrandRepository = carBrandRepository;
            _carTypeRepository = carTypeRepository;
        }

        [BindProperty]
        public IList<Car> CarList { get; set; } = new List<Car>();

        [BindProperty]
        public DateTime StartTime { get; set; }

        [BindProperty]
        public DateTime EndTime{ get; set; }

        [BindProperty]
        public bool IsAdvancedSearch { get; set; }

        [BindProperty]
        public double? MinPrice { get; set; }

        [BindProperty]
        public double? MaxPrice { get; set; }

        [BindProperty]
        public string? ErrorMsg{ get; set; }

        [BindProperty]
        public int BrandId { get; set; }

        [BindProperty]
        public int TypeId { get; set; }

        [BindProperty]
        public bool IsElectric { get; set; }

        [BindProperty]
        public bool NoMortgageRequired { get; set; }

        public IActionResult OnGet()
        {
            StartTime = DateTime.Now.AddDays(CommonConst.LBAdjust);
            EndTime = DateTime.Now.AddDays(CommonConst.LBAdjust);
            PreparePage();
            return Page();
        }

        public IActionResult OnPost()
        {
            if (!CheckDates(StartTime, EndTime, out string error))
            {
                ErrorMsg = error;
                PreparePage();
                return Page();
            }
            if (MinPrice > MaxPrice)
            {
                ErrorMsg = "Minimum price cannot be higher than maximum price!";
                PreparePage();
                return Page();
            }
            string? userId = User.FindFirst("Id")?.Value;
            if (userId != null)
            {
                // check customer have booked in that time
                int customerId = int.Parse(userId);
                ICollection<Contract> contracts =
                    _contractRepository.GetAllByProperty(x => x.CustomerId == customerId && x.IsDeleted == false);
                foreach (var contract in contracts)
                {
                    if (contract.RentStartDate <= EndTime && contract.RentEndDate >= StartTime)
                    {
                        ErrorMsg = "You are currently renting a car in this time period. Please choose another period!";
                        PreparePage();
                        return Page();
                    }
                }
            }

			//After checking duplicateed booking
			List<Car> cars = userId != null
				? _carRepository.GetAll().Where(c => c.CarOwnerId != int.Parse(userId)).ToList()
				: _carRepository.GetAll().ToList();

			cars = GetCarsAfterFilterTime(cars, StartTime, EndTime);
            if (IsAdvancedSearch)
            {
                cars = GetCarsAfterFilterPrice(cars, MinPrice, MaxPrice);
                cars = GetCarsAfterFilterBrand(cars, BrandId);
                cars = GetCarsAfterFilterType(cars, TypeId);
                cars = GetCarsAfterFilterElectric(cars, IsElectric);
                cars = GetCarsAfterFilterMortage(cars, NoMortgageRequired);
            }

            CarList = cars;
            PreparePage();
            return Page();
        }

        private void PreparePage()
        {
            var Brands = _carBrandRepository.GetAll();
            ViewData["BrandId"] = new SelectList(Brands, "Id", "BrandName");
            var Types = _carTypeRepository.GetAll();
            ViewData["TypeId"] = new SelectList(Types, "Id", "TypeName");
        }

        private static bool CheckDates(DateTime rentStartDate, DateTime rentEndDate, out string error)
        {
            if (rentStartDate <= DateTime.Now || rentStartDate > DateTime.Now.AddDays(CommonConst.UBAdjust))
            {
                error = "Rent start date must be within 90 days from now!";
                return false;
            }
            if (rentEndDate <= DateTime.Now || rentEndDate > DateTime.Now.AddDays(CommonConst.UBAdjust))
            {
                error = "Rent end date must be within 90 days from now!";
                return false;
            }
            if (rentStartDate > rentEndDate)
            {
                error = "Rent start date cannot be before rent end date!";
                return false;
            }
            if (rentStartDate.AddDays(CommonConst.MaxRentPeriod) < rentEndDate)
            {
                error = "Rent period must not exceed 7 days!";
                return false;
            }
            error = string.Empty;
            return true;
        }

        private List<Car> GetCarsAfterFilterTime(List<Car> cars, DateTime startTime, DateTime endTime)
        {
            var contractIds = _contractRepository
                .GetAllByProperty(x => x.RentStartDate <= endTime && x.RentEndDate >= startTime && !x.IsDeleted)
                .Select(x => x.CarId)
                .Distinct();
            cars.RemoveAll(car => contractIds.Contains(car.Id));
            return cars;
        }

        private static List<Car> GetCarsAfterFilterPrice(List<Car> cars, double? minPrice, double? maxPrice)
        {
            if (minPrice.HasValue)
            {
                cars.RemoveAll(car => car.PricePerDay < minPrice.Value);
            }
            if (maxPrice.HasValue)
            {
                cars.RemoveAll(car => car.PricePerDay > maxPrice.Value);
            }
            return cars;
        }

        private static List<Car> GetCarsAfterFilterBrand(List<Car> cars, int brandId)
        {
            if (brandId != 0)
            {
                cars.RemoveAll(car => car.CarBrandId != brandId);
            }
            return cars;
        }

        private static List<Car> GetCarsAfterFilterType(List<Car> cars, int typeId)
        {
            if (typeId != 0)
            {
                cars.RemoveAll(car => car.CarTypeId != typeId);
            }
            return cars;
        }

        private static List<Car> GetCarsAfterFilterMortage(List<Car> cars, bool noMortgage)
        {
            if (noMortgage)
            {
                cars.RemoveAll(car => car.IsMortgageRequired);
            }
            return cars;
        }

        private static List<Car> GetCarsAfterFilterElectric(List<Car> cars, bool isElectric)
        {
            if (isElectric)
            {
                cars.RemoveAll(car => !car.IsElectric);
            }
            return cars;
        }
    }
}
