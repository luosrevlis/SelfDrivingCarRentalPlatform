using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SelfDrivingCarRentalPlatform.Pages.Account
{
	public class LogoutModel : PageModel
	{
		public void OnGet()
		{
		}

		public async Task<IActionResult> OnPostAsync()
		{
			await HttpContext.SignOutAsync("LoginCookieAuth");
			return RedirectToPage("/Index");
		}
	}
}