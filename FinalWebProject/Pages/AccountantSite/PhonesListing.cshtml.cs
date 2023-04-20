using FinalWebProject.Data;
using FinalWebProject.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace FinalWebProject.Pages.AccountantSite.PhoneList
{
    public class IndexModel : PageModel
    {
        private readonly FinalWebProject.Data.FinalDbContext _context;
        public IndexModel(FinalWebProject.Data.FinalDbContext context)
        {
            _context = context;
        }
        public ICollection<Phone> Phones { get; set; } = default!;
        public async Task<IActionResult> OnGetAsync()
        {
            Accountant accountant = SessionHelpers.GetObjectFromJson<Accountant>(HttpContext.Session, "accountantUser");
            if (accountant == null)
            {
                return RedirectToPage("../Index");
            }
            if (_context.Phone != null)
            {
                Phones = await _context.Phone.Include(p => p.Manufacturer).ToListAsync();
            }
            return Page();
        }
    }
}
