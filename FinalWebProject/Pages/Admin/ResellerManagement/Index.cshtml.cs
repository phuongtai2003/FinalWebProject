using FinalWebProject.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace FinalWebProject.Pages.Admin.ResellerManagement
{
    public class IndexModel : PageModel
    {
        private readonly FinalWebProject.Data.FinalDbContext _dbContext;
        public IndexModel(FinalWebProject.Data.FinalDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IList<Reseller> Resellers { get; set; } = default!;
        public async Task<IActionResult> OnGetAsync()
        {
            if(_dbContext.Reseller != null)
            {
                Resellers = await _dbContext.Reseller.ToListAsync();
                return Page();
            }
            return RedirectToPage("../Index");
        }
    }
}
