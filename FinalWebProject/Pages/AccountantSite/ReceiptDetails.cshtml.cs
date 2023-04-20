using FinalWebProject.Data;
using FinalWebProject.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace FinalWebProject.Pages.AccountantSite
{
    public class ReceiptDetailsModel : PageModel
    {
        private readonly FinalWebProject.Data.FinalDbContext _dbContext;
        public ReceiptDetailsModel(FinalWebProject.Data.FinalDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IList<ReceiptDetails> ReceiptDetails { get; set; } = default!;
        public Receipt Receipt { get; set; } = default!;
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return RedirectToPage("./Index");
            }
            Accountant accountant = SessionHelpers.GetObjectFromJson<Accountant>(HttpContext.Session, "accountantUser");
            if (accountant == null)
            {
                return RedirectToPage("./Index");
            }
			Receipt = await _dbContext.Receipt.FirstOrDefaultAsync(r => r.ReceiptId == id);
            if (Receipt == null)
            {
				return RedirectToPage("./Index");
			}
			ReceiptDetails = await _dbContext.ReceiptDetails.Where(rd=>rd.ReceiptId == id).Include(rd => rd.Phone).ToListAsync();
            return Page();
        }
    }
}