using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using FinalWebProject.Data;
using Microsoft.EntityFrameworkCore;

namespace FinalWebProject.Pages.Admin.AccountantManagement
{
    public class CreateModel : PageModel
    {
        private readonly FinalWebProject.Data.FinalDbContext _context;
        public SelectList WarehouseList { get; set; }

        public CreateModel(FinalWebProject.Data.FinalDbContext context)
        {
            _context = context;
            WarehouseList = new SelectList(_context.Warehouse, "WarehouseId", "WarehouseName");
		}

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Accountant Accountant { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.Accountant == null || Accountant == null)
            {
                return Page();
            }
            var accountant = await _context.Accountant.FirstOrDefaultAsync(a => a.AccountantEmail == Accountant.AccountantEmail);
            var reseller = await _context.Reseller.FirstOrDefaultAsync(r => r.ResellerEmail == Accountant.AccountantEmail);
            if(accountant != null || reseller != null || Accountant.AccountantEmail.Equals("admin@gmail.com"))
            {
                ViewData["Message"] = "Email has already exist";
                return Page();
            }
            _context.Accountant.Add(Accountant);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
