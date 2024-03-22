using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BusinessObjects.Models;
using Repositories.Interfaces;
using BusinessObjects.Enums;
using SelfDrivingCarRentalPlatform.Attributes;
using SelfDrivingCarRentalPlatform.Constants;

namespace SelfDrivingCarRentalPlatform.Pages.Contracts
{
    [AuthorizeRole(UserRole.Customer, UserRole.CarOwner)]
    public class RentCarModel : PageModel
    {
        private readonly ICarRepository _carRepository;
        private readonly ICarBrandRepository _carBrandRepository;
        private readonly ICarTypeRepository _carTypeRepository;
        private readonly IContractRepository _contractRepository;
        private readonly ILocationRepository _locationRepository;
        private readonly ITransactionRepository _transactionRepository;
        private readonly IUserRepository _userRepository;

        public RentCarModel(
            ICarRepository carRepository,
            ICarBrandRepository carBrandRepository,
            ICarTypeRepository carTypeRepository,
            IContractRepository contractRepository,
            ILocationRepository locationRepository,
            ITransactionRepository transactionRepository,
            IUserRepository userRepository)
        {
            _carRepository = carRepository;
            _carBrandRepository = carBrandRepository;
            _carTypeRepository = carTypeRepository;
            _contractRepository = contractRepository;
            _locationRepository = locationRepository;
            _transactionRepository = transactionRepository;
            _userRepository = userRepository;
        }

        public IActionResult OnGet(int carId, string start, string end)
        {
            DateTime rentStartDate = DateTime.ParseExact(start, CommonConst.DateFormatYmdHyphen, null);
            DateTime rentEndDate = DateTime.ParseExact(end, CommonConst.DateFormatYmdHyphen, null);
            if (!CheckCar(carId, rentStartDate, rentEndDate))
            {
                return BadRequest();
            }
            Contract.Customer = _userRepository.GetById(int.Parse(User.FindFirst("Id")!.Value));
            if (Contract.Customer.DrivingLicenseId == null)
            {
                return RedirectToPage("../DrivingLicenses/Create");
            }
            InitContract(carId, rentStartDate, rentEndDate);

            return Page();
        }

        [BindProperty]
        public Contract Contract { get; set; } = new();

        [BindProperty]
        public Transaction Transaction { get; set; } = new();

        public IActionResult OnPost()
        {
            Contract.Customer = _userRepository.GetById(int.Parse(User.FindFirst("Id")!.Value));
            if (Contract.Customer.DrivingLicenseId == null)
            {
                return RedirectToPage("../DrivingLicenses/Create");
            }
            InitContract(Contract.CarId, Contract.RentStartDate, Contract.RentEndDate);
            Contract.SignDate = DateTime.Now;

            _contractRepository.Add(Contract);
            _transactionRepository.Add(Transaction);
            return RedirectToPage("Index");
        }

        private void InitContract(int carId, DateTime rentStartDate, DateTime rentEndDate)
        {
            Contract.CarId = carId;
            Contract.Car = _carRepository.GetById(carId);
            Contract.Car.CarOwner = _userRepository.GetById(Contract.Car.CarOwnerId);
            Contract.Car.CarOwner.Location = _locationRepository.GetById(Contract.Car.CarOwner.LocationId);
            Contract.Car.CarBrand = _carBrandRepository.GetById(Contract.Car.CarBrandId);
            Contract.Car.CarType = _carTypeRepository.GetById(Contract.Car.CarTypeId);
            Contract.RentStartDate = rentStartDate;
            Contract.RentEndDate = rentEndDate;

            int rentDays = (int)(rentEndDate - rentStartDate).TotalDays + 1;
            double rentTotal = rentDays * Contract.Car.PricePerDay;
            Transaction.TotalPrice = rentTotal;
            Transaction.Deposit = rentTotal * Contract.Car.DepositRatio / 100;
            Transaction.MortgageFee = Contract.Car.IsMortgageRequired ? CommonConst.MortgageFee : 0;
            Transaction.InsuranceFee = CommonConst.InsuranceFee;
            Transaction.Contract = Contract;
        }

        private bool CheckCar(int carId, DateTime rentStartDate, DateTime rentEndDate)
        {
            // Date validation
            if (rentStartDate <= DateTime.Now || rentStartDate > DateTime.Now.AddDays(CommonConst.UBAdjust))
            {
                return false;
            }
            if (rentEndDate <= DateTime.Now || rentEndDate > DateTime.Now.AddDays(CommonConst.UBAdjust))
            {
                return false;
            }
            if (rentStartDate > rentEndDate || rentStartDate.AddDays(CommonConst.MaxRentPeriod) < rentEndDate)
            {
                return false;
            }
            // Car validation
            Car car = _carRepository.GetById(carId);
            if (car == null || car.IsDeleted)
            {
                return false;
            }
            // Check overlaps
            var overlappedContracts = _contractRepository
                .GetAllByProperty(contract => contract.CarId == carId
                && contract.RentStartDate <= rentEndDate
                && rentStartDate <= contract.RentEndDate);

            return !overlappedContracts.Any();
        }
    }
}
