using FinalWebProject.Data;
using FinalWebProject.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace FinalWebProject.Pages.ResellerSite
{
    public class OrderListModel : PageModel
    {
        private readonly FinalWebProject.Data.FinalDbContext _dbContext;
        public OrderListModel(FinalWebProject.Data.FinalDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IList<ResellerImportReceipt> ImportReceipts { get; set; }
        public async Task<IActionResult> OnGetAsync()
        {
            Reseller reseller = SessionHelpers.GetObjectFromJson<Reseller>(HttpContext.Session, "resellerUser");
            if(reseller == null || SessionHelpers.GetObjectFromJson<Reseller>(HttpContext.Session, "resellerUser") == null) {
                return RedirectToPage("./Index");
            }
            var resellerId = reseller.ResellerId;
            
            ImportReceipts = await _dbContext.ResellerImportReceipt.Include(r => r.Reseller).Include(r=> r.DeliveryStatus).Where(r => r.ResellerId == resellerId).ToListAsync();
            return Page();
        }
    }
}
