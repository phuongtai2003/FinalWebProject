using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using FinalWebProject.Data;

namespace FinalWebProject.Pages.Admin.AccountantManagement
{
    public class DeleteModel : PageModel
    {
        private readonly FinalWebProject.Data.FinalDbContext _context;

        public DeleteModel(FinalWebProject.Data.FinalDbContext context)
        {
            _context = context;
        }

        [BindProperty]
      public Accountant Accountant { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Accountant == null)
            {
                return NotFound();
            }

            var accountant = await _context.Accountant.FirstOrDefaultAsync(m => m.AccountantID == id);

            if (accountant == null)
            {
                return NotFound();
            }
            else 
            {
                Accountant = accountant;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Accountant == null)
            {
                return NotFound();
            }
            var accountant = await _context.Accountant.FindAsync(id);

            if (accountant != null)
            {
                Accountant = accountant;
                _context.Accountant.Remove(Accountant);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
