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
    public class DetailsModel : PageModel
    {
        private readonly FinalWebProject.Data.FinalDbContext _context;

        public DetailsModel(FinalWebProject.Data.FinalDbContext context)
        {
            _context = context;
        }

      public Phone Phone { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Phone == null)
            {
                return NotFound();
            }

            var phone = await _context.Phone.Include(phone => phone.Manufacturer).FirstOrDefaultAsync(m => m.PhoneId == id);
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
    }
}
