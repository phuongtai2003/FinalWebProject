using FinalWebProject.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace FinalWebProject.Pages.Admin.ResellerManagement
{
    public class DeleteModel : PageModel
    {
        private readonly FinalWebProject.Data.FinalDbContext _dbContext;
        public DeleteModel(FinalWebProject.Data.FinalDbContext context)
        {
            _dbContext = context;
        }
        public Reseller Reseller { get; set; } = default!;
        public async Task<IActionResult> OnGetAsync(int? id)
        { 
            if(id == null || _dbContext.Reseller == null)
            {
                return NotFound();
            }
            var reseller = await _dbContext.Reseller.FirstOrDefaultAsync(r => r.ResellerId == id);
            if(reseller == null)
            {
                return NotFound();
            }
            else
            {
                Reseller = reseller;
            }
            return Page();
        }
        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if(id == null || _dbContext.Reseller == null)
            {
                return NotFound();
            }
            var reseller = await _dbContext.Reseller.FindAsync(id);
            if(reseller != null)
            {
                Reseller = reseller;
                _dbContext.Reseller.Remove(Reseller);
                await _dbContext.SaveChangesAsync();
            }
            return RedirectToPage("./Index");
        }
    }
}
