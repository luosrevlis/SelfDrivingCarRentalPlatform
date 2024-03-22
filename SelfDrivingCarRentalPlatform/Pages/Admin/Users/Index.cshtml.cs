using BusinessObjects.Enums;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BusinessObjects.Models;
using Microsoft.EntityFrameworkCore;
using Repositories.Interfaces;
using SelfDrivingCarRentalPlatform.Attributes;

namespace SelfDrivingCarRentalPlatform.Pages.Admin.Users
{
    [AuthorizeRole(UserRole.Admin)]
    public class IndexModel : PageModel
    {
        private readonly IUserRepository _userRepository;

        public IndexModel(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public IList<User> Users { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Users = _userRepository.GetAll()
                .Include(x => x.DrivingLicense)
                .Where(x => x.Role.Equals(UserRole.Customer) || x.Role.Equals(UserRole.CarOwner))
                .ToList();
        }
    }
}
