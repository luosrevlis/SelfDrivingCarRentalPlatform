using Microsoft.AspNetCore.Mvc.RazorPages;
using BusinessObjects.Models;
using Repositories.Interfaces;
using SelfDrivingCarRentalPlatform.Attributes;
using BusinessObjects.Enums;

namespace SelfDrivingCarRentalPlatform.Pages.CarOwners.Cars
{
    [AuthorizeRole(UserRole.CarOwner)]
    public class IndexModel : PageModel
    {
        private readonly ICarRepository _repository;

        public IndexModel(ICarRepository repository)
        {
            _repository = repository;
        }

        public IList<Car> CarList { get;set; } = new List<Car>();

        public void OnGet()
        {
            string? userId = User.FindFirst("Id")?.Value;
            if (userId != null)
            {
                CarList = _repository.GetAll().Where(c => c.CarOwnerId == int.Parse(userId)).ToList();
            }
        }
    }
}
