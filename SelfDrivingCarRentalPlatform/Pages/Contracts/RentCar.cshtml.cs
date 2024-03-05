using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BusinessObjects.Models;
using Repositories.Interfaces;
using BusinessObjects.Enums;
using SelfDrivingCarRentalPlatform.Attributes;

namespace SelfDrivingCarRentalPlatform.Pages.Contracts
{
    [AuthorizeRole(UserRole.Customer)]
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

        public IActionResult OnGet(int carId, DateTime rentStartDate, DateTime rentEndDate)
        {
            Contract.Customer = _userRepository.GetById(int.Parse(User.FindFirst("Id")!.Value));
            if (Contract.Customer.DrivingLicenseId == null)
            {
                return BadRequest(); //redirect to driving license page
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
                return BadRequest(); //redirect to driving license page
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
            Contract.Car.CarOwner = _userRepository.GetById(carId);
            Contract.Car.CarOwner.Location = _locationRepository.GetById(Contract.Car.CarOwner.LocationId);
            Contract.Car.CarBrand = _carBrandRepository.GetById(Contract.Car.CarBrandId);
            Contract.Car.CarType = _carTypeRepository.GetById(Contract.Car.CarTypeId);
            Contract.RentStartDate = rentStartDate;
            Contract.RentEndDate = rentEndDate;

            double rentTotal = (int)(rentEndDate - rentStartDate).TotalDays * Contract.Car.PricePerDay;
            Transaction.TotalPrice = rentTotal;
            Transaction.Deposit = rentTotal * Contract.Car.DepositRatio / 100;
            Transaction.MortgageFee = Contract.Car.IsMortgageRequired ? 15000000 : 0;
            Transaction.InsuranceFee = 90000;
            Transaction.Contract = Contract;
        }
    }
}
