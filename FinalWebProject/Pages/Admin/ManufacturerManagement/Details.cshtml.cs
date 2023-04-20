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
    public class DetailsModel : PageModel
    {
        private readonly FinalWebProject.Data.FinalDbContext _context;

        public DetailsModel(FinalWebProject.Data.FinalDbContext context)
        {
            _context = context;
        }

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
    }
}
