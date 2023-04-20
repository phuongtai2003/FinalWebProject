using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using FinalWebProject.Data;

namespace FinalWebProject.Pages.Admin.WarehouseManagement
{
    public class DeleteModel : PageModel
    {
        private readonly FinalWebProject.Data.FinalDbContext _context;

        public DeleteModel(FinalWebProject.Data.FinalDbContext context)
        {
            _context = context;
        }

        [BindProperty]
      public Warehouse Warehouse { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Warehouse == null)
            {
                return NotFound();
            }

            var warehouse = await _context.Warehouse.FirstOrDefaultAsync(m => m.WarehouseId == id);

            if (warehouse == null)
            {
                return NotFound();
            }
            else 
            {
                Warehouse = warehouse;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Warehouse == null)
            {
                return NotFound();
            }
            var warehouse = await _context.Warehouse.FindAsync(id);

            if (warehouse != null)
            {
                Warehouse = warehouse;
                _context.Warehouse.Remove(Warehouse);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
