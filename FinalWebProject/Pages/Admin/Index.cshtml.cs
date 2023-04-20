using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FinalWebProject.Pages.Admin
{
    public class IndexModel : PageModel
    {
        public IActionResult OnGet()
        {
            string username = HttpContext.Session.GetString("Username");
            string userType = HttpContext.Session.GetString("Type");
            if(username != null && userType != null)
            {
                if (username.Equals("Admin") && userType.Equals("Admin"))
                {
                    return Page();
                }
                else
                {
                    return RedirectToPage("../Index");
                }
            }
			return RedirectToPage("../Index");
		}
    }
}
