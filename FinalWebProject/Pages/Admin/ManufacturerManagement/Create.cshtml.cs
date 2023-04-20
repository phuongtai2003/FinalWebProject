using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using FinalWebProject.Data;
using FinalWebProject.ViewModel.Admin.ManufacturerManagement;

namespace FinalWebProject.Pages.Admin.ManufacturerManagement
{
    public class CreateModel : PageModel
    {
        private readonly FinalWebProject.Data.FinalDbContext _context;

        public CreateModel(FinalWebProject.Data.FinalDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public ManufacturerViewModelAdd Manufacturer { get; set; }
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.Manufacturer == null || Manufacturer == null)
            {
                return Page();
            }
            var manufacturer = new Manufacturer
            {
                ManufacturerName = Manufacturer.ManufacturerName,
                ManufacturerYear = Manufacturer.ManufacturerYear,
            };

            _context.Manufacturer.Add(manufacturer);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
