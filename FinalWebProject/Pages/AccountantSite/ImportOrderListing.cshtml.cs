using FinalWebProject.Data;
using FinalWebProject.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace FinalWebProject.Pages.AccountantSite
{
    public class ImportOrderListingModel : PageModel
    {
        private readonly FinalWebProject.Data.FinalDbContext _dbContext;
        public ImportOrderListingModel(FinalWebProject.Data.FinalDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IList<ResellerImportReceipt> ResellerImportReceipts { get; set; } = default!;
        public async Task<IActionResult> OnGetAsync()
        {
            var accountant = SessionHelpers.GetObjectFromJson<Accountant>(HttpContext.Session, "accountantUser");
            if (accountant == null || SessionHelpers.GetObjectFromJson<Accountant>(HttpContext.Session, "accountantUser") == null)
            {
                return RedirectToPage("./Index");
            }
            ResellerImportReceipts = await _dbContext.ResellerImportReceipt.Where(r => r.PaymentStatus == 0).Include(r => r.Reseller).Include(r => r.DeliveryStatus).ToListAsync();
            return Page();
        }
    }
}
