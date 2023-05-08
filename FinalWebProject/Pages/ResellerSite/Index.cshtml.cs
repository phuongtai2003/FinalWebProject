using FinalWebProject.Data;
using FinalWebProject.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FinalWebProject.Pages.ResellerSite
{
    public class IndexModel : PageModel
    {
        public IActionResult OnGet()
        {
			Reseller reseller = SessionHelpers.GetObjectFromJson<Reseller>(HttpContext.Session, "resellerUser");
			if (reseller != null)
			{
				return Page();
			}
			return RedirectToPage("../Index");
		}
    }
}
