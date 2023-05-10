using FinalWebProject.Data;
using FinalWebProject.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace FinalWebProject.Pages.AccountantSite
{
    public class ExportReceiptListingModel : PageModel
    {
        private readonly FinalWebProject.Data.FinalDbContext _dbContext;

        public ExportReceiptListingModel(FinalDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IList<ExportReceipt> ExportReceipts { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync()
        {
            Accountant accountant = SessionHelpers.GetObjectFromJson<Accountant>(HttpContext.Session, "accountantUser");
            if(accountant == null)
            {
                return RedirectToPage("./Index");
            }
            ExportReceipts = await _dbContext.ExportReceipt.Include(er => er.Accountant).Where(er => er.AccountantId == accountant.AccountantID).ToListAsync();
            return Page();
        }
    }
}
