using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using FinalWebProject.Data;
using FinalWebProject.ViewModel.Admin.WarehouseManagement;

namespace FinalWebProject.Pages.Admin.WarehouseManagement
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
        public WarehouseViewModelAdd Warehouse { get; set; }
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.Warehouse == null || Warehouse == null)
            {
                return Page();
            }
            var warehouseModel = new Warehouse
            {
                WarehouseName = Warehouse.WarehouseName,
                WarehouseLocation = Warehouse.WarehouseLocation,
            };

            _context.Warehouse.Add(warehouseModel);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
