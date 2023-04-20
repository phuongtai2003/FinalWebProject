using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FinalWebProject.Data;
using FinalWebProject.ViewModel.Admin.WarehouseManagement;

namespace FinalWebProject.Pages.Admin.WarehouseManagement
{
    public class EditModel : PageModel
    {
        private readonly FinalWebProject.Data.FinalDbContext _context;

        public EditModel(FinalWebProject.Data.FinalDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public WarehouseViewModelEdit Warehouse { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Warehouse == null)
            {
                return NotFound();
            }

            var warehouse =  await _context.Warehouse.FirstOrDefaultAsync(m => m.WarehouseId == id);
            if (warehouse == null)
            {
                return NotFound();
            }
            Warehouse = new WarehouseViewModelEdit
            {
                WarehouseId = warehouse.WarehouseId,
                WarehouseName = warehouse.WarehouseName,
                WarehouseLocation = warehouse.WarehouseLocation,
            };
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Warehouse warehouse = new Warehouse
            {
                WarehouseId = Warehouse.WarehouseId,
                WarehouseName = Warehouse.WarehouseName,
                WarehouseLocation = Warehouse.WarehouseLocation,
            };

            _context.Attach(warehouse).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WarehouseExists(warehouse.WarehouseId))
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

        private bool WarehouseExists(int id)
        {
          return (_context.Warehouse?.Any(e => e.WarehouseId == id)).GetValueOrDefault();
        }
    }
}
