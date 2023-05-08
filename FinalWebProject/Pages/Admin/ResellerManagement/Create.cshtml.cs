using FinalWebProject.Data;
using FinalWebProject.ViewModel.Admin.ResellerManagement;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FinalWebProject.Pages.Admin.ResellerManagement
{
    public class CreateModel : PageModel
    {
        private readonly FinalWebProject.Data.FinalDbContext _dbContext;
        public CreateModel(FinalWebProject.Data.FinalDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        [BindProperty]
        public ResellerViewModelAdd Reseller { get; set; }
        public IActionResult OnGet()
        {
            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if(!ModelState.IsValid || _dbContext.Reseller == null || Reseller == null) {
                return Page();
            }
            var resellerModel = new Reseller
            {
                ResellerName = Reseller.ResellerName,
                ResellerEmail = Reseller.ResellerEmail,
                ResellerPassword = Reseller.ResellerPassword,
                ResellerLocation = Reseller.ResellerLocation,
            };
            _dbContext.Reseller.Add(resellerModel);
            await _dbContext.SaveChangesAsync();
            return RedirectToPage("./Index");
        }
    }
}
