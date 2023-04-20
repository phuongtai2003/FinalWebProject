using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using FinalWebProject.Data;

namespace FinalWebProject.Pages.Admin.PhoneManagement
{
    public class DeleteModel : PageModel
    {
        private readonly FinalWebProject.Data.FinalDbContext _context;

        public DeleteModel(FinalWebProject.Data.FinalDbContext context)
        {
            _context = context;
        }

        [BindProperty]
      public Phone Phone { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Phone == null)
            {
                return NotFound();
            }

            var phone = await _context.Phone.FirstOrDefaultAsync(m => m.PhoneId == id);

            if (phone == null)
            {
                return NotFound();
            }
            else 
            {
                Phone = phone;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Phone == null)
            {
                return NotFound();
            }
            var phone = await _context.Phone.FindAsync(id);

            if (phone != null)
            {
                Phone = phone;
                _context.Phone.Remove(Phone);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
