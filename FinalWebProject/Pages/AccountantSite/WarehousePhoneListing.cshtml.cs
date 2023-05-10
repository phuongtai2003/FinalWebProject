using FinalWebProject.Data;
using FinalWebProject.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace FinalWebProject.Pages.AccountantSite
{
    public class WarehousePhoneListingModel : PageModel
    {
        private readonly FinalWebProject.Data.FinalDbContext _dbContext;
        public WarehousePhoneListingModel(FinalWebProject.Data.FinalDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IList<WarehouseProducts> WarehouseProducts { get; set; } = default!;
        public async Task<IActionResult> OnGetAsync()
        {
            var accountant = SessionHelpers.GetObjectFromJson<Accountant>(HttpContext.Session, "accountantUser");
            if(accountant == null)
            {
                return RedirectToPage("./Index");
            }
            WarehouseProducts = await _dbContext.WarehouseProducts.Include(wp => wp.Phone).ThenInclude(p => p.Manufacturer).Include(wp => wp.Warehouse).Where(wp => wp.WarehouseId == accountant.WarehouseID).ToListAsync();
            return Page();
        }
    }
}
