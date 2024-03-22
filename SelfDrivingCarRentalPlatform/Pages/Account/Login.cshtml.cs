using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repositories.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace SelfDrivingCarRentalPlatform.Pages.Account
{
	public class LoginModel : PageModel
	{
		private readonly IUserRepository _userRepository;

		public LoginModel(IUserRepository userRepository)
		{
			_userRepository = userRepository;
		}

		[BindProperty]
		public Credential Credential { get; set; }

		public void OnGet()
		{
		}

		public async Task<IActionResult> OnPostAsync()
		{
			if (!ModelState.IsValid)
			{
				return Page();
			}

			var user = _userRepository.GetAll().FirstOrDefault(user =>
				user.Email.Equals(Credential.Email)
				&& user.Password.Equals(Credential.Password));

			if (user != null)
			{
				var claims = new List<Claim>
				{
					new Claim(ClaimTypes.Name, user.Fullname),
					new Claim(ClaimTypes.Role, user.Role.ToString()),
					new Claim("Id", user.Id.ToString())
				};

				var identity = new ClaimsIdentity(claims, "LoginCookieAuth");
				ClaimsPrincipal principal = new(identity);

				await HttpContext.SignInAsync("LoginCookieAuth", principal);

				return RedirectToPage("/Index");
			}
			else
			{
				ModelState.AddModelError(string.Empty, "Invalid email or password. Please try again.");

				return Page();
			}
		}
	}

	public class Credential
	{
		[Required]
		[DataType(DataType.EmailAddress)]
		public string Email { get; set; }

		[Required]
		[DataType(DataType.Password)]
		public string Password { get; set; }
	}
}