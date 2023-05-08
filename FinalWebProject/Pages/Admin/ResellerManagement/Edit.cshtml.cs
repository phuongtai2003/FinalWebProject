using FinalWebProject.Data;
using FinalWebProject.ViewModel.Admin.ResellerManagement;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace FinalWebProject.Pages.Admin.ResellerManagement
{
    public class EditModel : PageModel
    {
        private readonly FinalWebProject.Data.FinalDbContext _dbContext;
        public EditModel(FinalWebProject.Data.FinalDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        [BindProperty]
        public ResellerViewModelEdit ResellerVM { get; set; }
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if(id == null || _dbContext.Reseller == null)
            {
                return NotFound("Id is not found");
            }
            var reseller = await _dbContext.Reseller.FirstOrDefaultAsync(r => r.ResellerId == id);
            if(reseller == null)
            {
				return NotFound("Reseller is not found");
			}
			ResellerVM = new ResellerViewModelEdit
			{
                ResellerId = reseller.ResellerId,
                ResellerName = reseller.ResellerName,
                ResellerEmail = reseller.ResellerEmail,
                ResellerPassword = reseller.ResellerPassword,
                ResellerLocation = reseller.ResellerLocation,
            };
            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Reseller reseller = new Reseller
            {
                ResellerId = ResellerVM.ResellerId,
                ResellerEmail = ResellerVM.ResellerEmail,
                ResellerName = ResellerVM.ResellerName,
                ResellerPassword = ResellerVM.ResellerPassword,
                ResellerLocation = ResellerVM.ResellerLocation,
            };
            _dbContext.Attach(reseller).State = EntityState.Modified;
            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if(_dbContext.Reseller.FirstAsync(r => r.ResellerId == reseller.ResellerId) == null)
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToPage("./Index");
        }
    }
}
