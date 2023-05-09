using FinalWebProject.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FinalWebProject.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ManufacturerController : Controller
    {
        private readonly FinalWebProject.Data.FinalDbContext _dbContext;

        public ManufacturerController(FinalWebProject.Data.FinalDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        [Route("Search/{id:int}")]
        public async Task<IActionResult> Search(int id)
        {
            var manufacturer = await _dbContext.Phone.Include(p => p.Manufacturer).Where(p=>p.ManufacturerId == id).ToListAsync();
            if(manufacturer == null)
            {
                return StatusCode(500, Json(new { error = "Company not found" }));
            }
            else
            {
                return StatusCode(200, Json(manufacturer));
            }
        }
    }
}
