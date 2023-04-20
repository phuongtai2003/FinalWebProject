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
    public class IndexModel : PageModel
    {
        private readonly FinalWebProject.Data.FinalDbContext _context;

        public IndexModel(FinalWebProject.Data.FinalDbContext context)
        {
            _context = context;
        }

        public IList<Warehouse> Warehouse { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Warehouse != null)
            {
                Warehouse = await _context.Warehouse.ToListAsync();
            }
        }
    }
}
