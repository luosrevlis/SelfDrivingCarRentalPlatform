using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObjects.Models;
using DAOs;
using Repositories.Interfaces;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace SelfDrivingCarRentalPlatform.Pages.Cars
{
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
        public IList<Car> Car { get;set; } = default!;
        [BindProperty]
        public DateTime StartTime { get; set; }

        [BindProperty]
        public DateTime EndTime{ get; set; }
        [BindProperty]
        public double MinPrice { get; set; }

        [BindProperty]
        public double MaxPrice { get; set; }

        [BindProperty]
        public string ErrorMsg{ get; set; }
        [BindProperty]
        public int BrandId { get; set; }
        [BindProperty]
        public int TypeId { get; set; }
        [BindProperty]
        public bool IsElectric { get; set; }

        [BindProperty]
        public bool IsMortgageRequired { get; set; }

        public IActionResult OnGet()
        {
            Car = (IList<Car>)_carRepository.GetAll();
            var Brands = _carBrandRepository.GetAll();
            ViewData["BrandId"] = new SelectList(Brands, "Id", "BrandName");
            var Types = _carTypeRepository.GetAll();
            ViewData["TypeId"] = new SelectList(Types, "Id", "TypeName");
            return Page();
        }

        public IActionResult OnPost()
        {
            Car = (IList<Car>)_carRepository.GetAll();
            if (MinPrice > MaxPrice)
            {
                ErrorMsg = "Can not filter with min price > max price";
                return Page();
            }
            ICollection<Contract> contracts = null;
            string userId = null;
            string userCookie = Request.Cookies["LoginCookieAuth"];
            if (!userCookie.IsNullOrEmpty())
            {
                userId = ExactractClaim();
            }

            if (userId != null)
            {
                // check customer have booked in that time
                int customerId = int.Parse(userId);
                contracts = _contractRepository.GetAllByProperty(x => x.CustomerId == customerId && x.IsDeleted == false);
                if (!contracts.IsNullOrEmpty())
                {
                    foreach(var contract in contracts)
                    {
                        if (contract.RentStartDate <= EndTime && contract.RentEndDate >= StartTime)
                        {
                            ErrorMsg = "Can not book when you have booked one car in this time";
                            return Page();
                        }
                    }
                }
            }

            //After checking duplicateed booking
            IList<Car> cars = new List<Car>();

            cars = GetCarsAfterFilterTime(Car);
            cars = GetCarsAfterFilterPrice(Car, MinPrice, MaxPrice);
            cars = GetCarsAfterFilterBrand(Car, BrandId);
            cars = GetCarsAfterFilterType(Car, TypeId);
            cars = GetCarsAfterFilterIsElectric(Car, IsElectric);
            cars = GetCarsAfterFilterIsMortage(Car, IsMortgageRequired);

            Car = cars;
            return Page();
        }
        private string ExactractClaim()
        {
            string userId = null;
            var claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme));
            claimsPrincipal = this.User;

            var userIdClaim = claimsPrincipal.FindFirst("Id");
            if (userIdClaim != null)
            {
                userId = userIdClaim.Value;
            }
            return userId;
        }

        private IList<Car> GetCarsAfterFilterTime(IList<Car> cars)
        {
            ICollection<Contract> contracts = null;
            contracts = _contractRepository.GetAllByProperty(x => x.RentStartDate <= EndTime && x.RentEndDate >= StartTime);
            if (!contracts.IsNullOrEmpty())
            {
                foreach (var contract in contracts)
                {
                    cars.Remove(contract.Car);
                }
                if (cars.Count() == 0)
                {
                    return cars;
                }
            }

            return cars;
        }

        private IList<Car> GetCarsAfterFilterPrice(IList<Car> cars, double? minPrice, double? maxPrice)
        {
            if (minPrice.Value == 0 && maxPrice.Value == 0)
            {
                return cars;
            }
            else
            {
                foreach (var car in cars)
                {
                    if (car.PricePerDay < minPrice || car.PricePerDay > maxPrice)
                    {
                        cars.Remove(car);
                    }
                    if (cars.Count() == 0)
                    {
                        return cars;
                    }
                }
                return cars;
            }
        }
        private IList<Car> GetCarsAfterFilterBrand(IList<Car> cars, int? brandId)
        {
            if (brandId.HasValue && brandId.Value != 0)
            {
                foreach(var car in cars)
                {
                    if (car.CarBrandId != brandId)
                    {
                        cars.Remove(car);
                    }

                    if (cars.Count() == 0)
                    {
                        return cars;
                    }
                }
            }
            return cars;
        }
        private IList<Car> GetCarsAfterFilterType(IList<Car> cars, int? typeId)
        {
            if (typeId.HasValue && typeId.Value != 0)
            {
                foreach (var car in cars)
                {
                    if (car.CarTypeId != typeId)
                    {
                        cars.Remove(car);
                    }
                    if (cars.Count() == 0)
                    {
                        return cars;
                    }
                }
            }
            
            return cars;
        }

        private IList<Car> GetCarsAfterFilterIsMortage(IList<Car> cars, bool? isMoratage)
        {
            if (isMoratage.HasValue)
            {
                if (isMoratage.Value)
                {
                    foreach (var car in cars)
                    {
                        if (!car.IsMortgageRequired)
                        {
                            cars.Remove(car);
                        }
                        if (cars.Count() == 0)
                        {
                            return cars;
                        }
                    }
                }
            }

            return cars;
        }

        private IList<Car> GetCarsAfterFilterIsElectric(IList<Car> cars, bool? isElectric)
        {
            if (isElectric.HasValue)
            {
                if (isElectric.Value)
                {
                    foreach (var car in cars)
                    {
                        if (!car.IsElectric)
                        {
                            cars.Remove(car);
                        }
                        if (cars.Count() == 0)
                        {
                            return cars;
                        }
                    }
                }
            }

            return cars;
        }
    }
}
