using FinalWebProject.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace FinalWebProject.Pages.AccountantSite
{
    public class ExportReceiptDetailsModel : PageModel
    {
        private readonly FinalWebProject.Data.FinalDbContext _dbContext;
        public ExportReceiptDetailsModel(FinalWebProject.Data.FinalDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public ExportReceipt ExportReceipt { get; set; } = default!;
        public IList<ExportReceiptDetails> ExportReceiptDetails { get; set; } = default!;
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }
            var exportReceipt = await _dbContext.ExportReceipt.FirstOrDefaultAsync(er => er.ExportReceiptId == id.Value);
            if(exportReceipt == null)
            {
                return RedirectToPage("./ExportReceiptListing");
            }
            ExportReceipt = exportReceipt;
            ExportReceiptDetails = await _dbContext.ExportReceiptDetails.Where(er => er.ExportReceiptId == ExportReceipt.ExportReceiptId).Include(er=>er.Phone).Include(er => er.Reseller).ToListAsync();
            return Page();
        }
    }
}
