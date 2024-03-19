using Microsoft.AspNetCore.Mvc.RazorPages;
using BusinessObjects.Models;
using Repositories.Interfaces;

namespace SelfDrivingCarRentalPlatform.Pages.Profile
{
    public class IndexModel : PageModel
    {
        private readonly IUserRepository _userRepository;
        public IndexModel(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public IList<User> User { get;set; } = default!;

        public async Task OnGetAsync()
        {
            
        }
    }
}
