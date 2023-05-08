using FinalWebProject.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace FinalWebProject.Pages.ResellerSite
{
    public class PhoneListingModel : PageModel
    {
        private readonly FinalWebProject.Data.FinalDbContext _dbContext;
		public SelectList WarehouseList { get; set; }
		public PhoneListingModel(FinalWebProject.Data.FinalDbContext dbContext)
        {
            _dbContext= dbContext;
			WarehouseList = new SelectList(_dbContext.Warehouse, "WarehouseId", "WarehouseName");
		}
        public IList<WarehouseProducts> WarehouseProducts { get; set; } = default!;
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if(id == null)
            {
                WarehouseProducts = await _dbContext.WarehouseProducts.Include(x => x.Warehouse).Include(x=>x.Phone).ThenInclude(p=>p.Manufacturer).Where(x => x.WarehouseId == _dbContext.Warehouse.First().WarehouseId).ToListAsync();
                return Page();
            }
			WarehouseProducts = await _dbContext.WarehouseProducts.Include(x => x.Warehouse).Include(x => x.Phone).ThenInclude(p => p.Manufacturer).Where(x => x.WarehouseId == id.Value).ToListAsync();
			return Page();
        }
    }
}
