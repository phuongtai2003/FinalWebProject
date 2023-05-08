using FinalWebProject.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace FinalWebProject.Pages.AccountantSite
{
    public class OrderDetailsModel : PageModel
    {
        private readonly FinalWebProject.Data.FinalDbContext _dbContext;
        public OrderDetailsModel(FinalWebProject.Data.FinalDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IList<ResellerImportReceiptDetails> ResellerImportReceiptDetails { get; set; } = default!;
        public ResellerImportReceipt ResellerImportReceipt { get; set; } = default!;
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }
            var resellerOrder = await _dbContext.ResellerImportReceipt.FirstOrDefaultAsync(r => r.ResellerImportReceiptId == id.Value);
            if(resellerOrder == null)
            {
                return RedirectToPage("./Index");
            }
            ResellerImportReceiptDetails = await _dbContext.ResellerImportReceiptDetail.Where(rd => rd.ResellerImportReceiptId == resellerOrder.ResellerImportReceiptId).Include(rd => rd.Phone).ThenInclude(p => p.Manufacturer).ToListAsync();
            return Page();
        }
    }
}
