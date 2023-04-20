using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FinalWebProject.Data;
using FinalWebProject.ViewModel.Admin.ManufacturerManagement;

namespace FinalWebProject.Pages.Admin.ManufacturerManagement
{
    public class EditModel : PageModel
    {
        private readonly FinalWebProject.Data.FinalDbContext _context;

        public EditModel(FinalWebProject.Data.FinalDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public ManufacturerViewModelEdit Manufacturer { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Manufacturer == null)
            {
                return NotFound();
            }

            var manufacturer =  await _context.Manufacturer.FirstOrDefaultAsync(m => m.ManufacturerId == id);
            if (manufacturer == null)
            {
                return NotFound();
            }
            Manufacturer = new ManufacturerViewModelEdit
            {
                ManufacturerId = manufacturer.ManufacturerId,
                ManufacturerName = manufacturer.ManufacturerName,
                ManufacturerYear = manufacturer.ManufacturerYear,
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
            Manufacturer manufacturer = new Manufacturer
            {
                ManufacturerId = Manufacturer.ManufacturerId,
                ManufacturerName = Manufacturer.ManufacturerName,
                ManufacturerYear = Manufacturer.ManufacturerYear,
            };
            _context.Attach(manufacturer).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ManufacturerExists(manufacturer.ManufacturerId))
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

        private bool ManufacturerExists(int id)
        {
          return (_context.Manufacturer?.Any(e => e.ManufacturerId == id)).GetValueOrDefault();
        }
    }
}
