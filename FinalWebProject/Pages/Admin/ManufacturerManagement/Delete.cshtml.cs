using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using FinalWebProject.Data;

namespace FinalWebProject.Pages.Admin.ManufacturerManagement
{
    public class DeleteModel : PageModel
    {
        private readonly FinalWebProject.Data.FinalDbContext _context;

        public DeleteModel(FinalWebProject.Data.FinalDbContext context)
        {
            _context = context;
        }

        [BindProperty]
      public Manufacturer Manufacturer { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Manufacturer == null)
            {
                return NotFound();
            }

            var manufacturer = await _context.Manufacturer.FirstOrDefaultAsync(m => m.ManufacturerId == id);

            if (manufacturer == null)
            {
                return NotFound();
            }
            else 
            {
                Manufacturer = manufacturer;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Manufacturer == null)
            {
                return NotFound();
            }
            var manufacturer = await _context.Manufacturer.FindAsync(id);

            if (manufacturer != null)
            {
                Manufacturer = manufacturer;
                _context.Manufacturer.Remove(Manufacturer);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
