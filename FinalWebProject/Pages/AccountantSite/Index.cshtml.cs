using FinalWebProject.Data;
using FinalWebProject.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FinalWebProject.Pages.AccountantSite
{
    public class IndexModel : PageModel
    {
        public Accountant Accountant { get; set; } = default!; 
        public IActionResult OnGet()
        {
            Accountant accountant = SessionHelpers.GetObjectFromJson<Accountant>(HttpContext.Session, "accountantUser");
            if (accountant != null)
            {
                Accountant = accountant;
                return Page();
            }
            return RedirectToPage("../Index");
        }
    }
}
