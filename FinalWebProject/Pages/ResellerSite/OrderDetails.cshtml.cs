using FinalWebProject.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace FinalWebProject.Pages.ResellerSite
{
    public class OrderDetailsModel : PageModel
    {
        private readonly FinalWebProject.Data.FinalDbContext _dbContext;
        public OrderDetailsModel(FinalWebProject.Data.FinalDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IList<ResellerImportReceiptDetails> OrderDetails { get; set; } = default!;
        public ResellerImportReceipt Receipt { get; set; } = default!;
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }
            Receipt = await _dbContext.ResellerImportReceipt.FirstOrDefaultAsync(r => r.ResellerImportReceiptId == id.Value);
            if(Receipt == null) {
                return RedirectToPage("./Order");
            }
            OrderDetails = await _dbContext.ResellerImportReceiptDetail.Where(rd => rd.ResellerImportReceiptId == Receipt.ResellerImportReceiptId).Include(rd => rd.Phone).ThenInclude(p=>p.Manufacturer).Include(rd=>rd.Warehouse).ToListAsync();
            return Page();
        }
    }
}
