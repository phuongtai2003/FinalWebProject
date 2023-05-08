using FinalWebProject.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace FinalWebProject.Pages.Admin.ResellerManagement
{
    public class DetailsModel : PageModel
    {

        private readonly FinalWebProject.Data.FinalDbContext _context;
        public DetailsModel(FinalWebProject.Data.FinalDbContext context)
        {
            _context= context;
        }
        public Reseller Reseller { get; set; } = default!;
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if(id== null || _context.Reseller == null)
            {
                return NotFound();
            }
            var reseller=  await _context.Reseller.FirstOrDefaultAsync(r => r.ResellerId ==id);
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
    }
}
