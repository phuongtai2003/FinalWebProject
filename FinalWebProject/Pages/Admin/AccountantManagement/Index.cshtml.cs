using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using FinalWebProject.Data;

namespace FinalWebProject.Pages.Admin.AccountantManagement
{
    public class IndexModel : PageModel
    {
        private readonly FinalWebProject.Data.FinalDbContext _context;

        public IndexModel(FinalWebProject.Data.FinalDbContext context)
        {
            _context = context;
        }

        public IList<Accountant> Accountant { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Accountant != null)
            {
                Accountant = await _context.Accountant
                .Include(a => a.Warehouse).ToListAsync();
            }
        }
    }
}
