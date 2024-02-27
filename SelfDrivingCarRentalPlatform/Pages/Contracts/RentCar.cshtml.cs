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
        private readonly IContractRepository _contractRepository;
        private readonly ITransactionRepository _transactionRepository;
        private readonly IUserRepository _userRepository;

        public RentCarModel(
            ICarRepository carRepository,
            IContractRepository contractRepository,
            ITransactionRepository transactionRepository,
            IUserRepository userRepository)
        {
            _carRepository = carRepository;
            _contractRepository = contractRepository;
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
