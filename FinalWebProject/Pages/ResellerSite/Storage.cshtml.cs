using FinalWebProject.Data;
using FinalWebProject.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace FinalWebProject.Pages.ResellerSite
{
    public class StorageModel : PageModel
    {
        private readonly FinalWebProject.Data.FinalDbContext _dbContext;
        public StorageModel(FinalWebProject.Data.FinalDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IList<ResellerStorage> Storages { get; set; } = default!;
        public Reseller Reseller { get; set; } = default!;
        public async Task<IActionResult> OnGetAsync()
        {
            var reseller = SessionHelpers.GetObjectFromJson<Reseller>(HttpContext.Session, "resellerUser");
            if(reseller == null)
            {
                return RedirectToPage("./Index");
            }
            Reseller = reseller;
            Storages = await _dbContext.ResellerStorage.Where(rs => rs.ResellerId == reseller.ResellerId).Include(rs=>rs.Phone).ThenInclude(p=>p.Manufacturer).ToListAsync();
            return Page();
        }
    }
}
