using FinalWebProject.Data;
using FinalWebProject.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FinalWebProject.Pages.ResellerSite
{
    public class IndexModel : PageModel
    {
		public Reseller Reseller { get; set; } = default!;
        public IActionResult OnGet()
        {
			Reseller reseller = SessionHelpers.GetObjectFromJson<Reseller>(HttpContext.Session, "resellerUser");
			if (reseller != null)
			{
				Reseller = reseller;
				return Page();
			}
			return RedirectToPage("../Index");
		}
    }
}
