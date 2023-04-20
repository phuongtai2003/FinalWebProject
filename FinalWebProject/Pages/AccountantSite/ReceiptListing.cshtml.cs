using FinalWebProject.Data;
using FinalWebProject.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace FinalWebProject.Pages.AccountantSite
{
    public class ReceiptListingModel : PageModel
    {
        private readonly FinalWebProject.Data.FinalDbContext _dbContext;
        public ReceiptListingModel(FinalWebProject.Data.FinalDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IList<Receipt> Receipts { get; set; } = default!;
        public async Task<IActionResult> OnGetAsync()
        {
            Accountant accountant = SessionHelpers.GetObjectFromJson<Accountant>(HttpContext.Session, "accountantUser");
            if (accountant == null || SessionHelpers.GetObjectFromJson<Accountant>(HttpContext.Session, "accountantUser") == null)
            {
                return RedirectToPage("./Index");
            }
            else
            {
                Receipts = await _dbContext.Receipt.Include(r => r.Accountant).Where(r => r.AccountantId == accountant.AccountantID).ToListAsync();
                return Page();
            }
        }
    }
}
