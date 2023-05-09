using FinalWebProject.API.ViewModel;
using FinalWebProject.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Primitives;
using Microsoft.IdentityModel.Tokens;

namespace FinalWebProject.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PhoneController : Controller
    {
        private readonly FinalWebProject.Data.FinalDbContext _dbContext;
        public PhoneController(FinalWebProject.Data.FinalDbContext dbContext) {
            _dbContext = dbContext;
        }
        [HttpGet]
        public IActionResult GetPhone()
        {
            var items = _dbContext.Phone.Include(p => p.Manufacturer).ToList();
            return Json(items);
        }
        [HttpGet]
        [Route("GetFive")]
        public async Task<IActionResult> GetFivePhone()
        {
            var phones = await _dbContext.Phone.Take(5).Include(p => p.Manufacturer).ToListAsync();
            return StatusCode(200, Json(phones));
        }
        [HttpGet]
        [Route("Search")]
        public async Task<IActionResult> Search([FromQuery] string name)
        { 
            var items = await _dbContext.Phone.Where(p => p.PhoneName.Contains(name)).Include(p=>p.Manufacturer).ToListAsync();
            return StatusCode(200, Json(items));
        }
        [HttpPost]
        [Route("Rate")]
        public async Task<IActionResult> RatePhone(RateProductViewModel rateProductViewModel)
        {
            Request.Headers.TryGetValue("X-Auth-Token", out StringValues headerValue);
            if (headerValue.IsNullOrEmpty())
            {
                return StatusCode(400, Json(new { msg = "Not Authenticated" }));
            }
            var customer = await _dbContext.Customer.FirstOrDefaultAsync(cus => cus.CustomerId == int.Parse(headerValue));
            if(customer == null)
            {
                return StatusCode(400, Json(new { msg = "User not found"}));
            }
            var phone = await _dbContext.Phone.FirstOrDefaultAsync(p => p.PhoneId == rateProductViewModel.PhoneId);
            if(phone == null)
            {
                return StatusCode(400, Json(new { msg = "Phone not exist" }));
            }
            else
            {
                var rating = await _dbContext.Rating.FirstOrDefaultAsync(r => r.PhoneId == phone.PhoneId && r.CustomerId == customer.CustomerId);
                var newRating = new Rating
                {
                    CustomerId = customer.CustomerId,
                    PhoneId = phone.PhoneId,
                    ReviewRating = rateProductViewModel.Rating,
                };
                if(rating == null)
                {
                    _dbContext.Rating.Add(newRating);
                    await _dbContext.SaveChangesAsync();
                }
                else
                {
                    _dbContext.Entry(rating).CurrentValues.SetValues(newRating);
                    await _dbContext.SaveChangesAsync();
                }
                return StatusCode(200, Json(new { message = "Review product successfully" }));
            }
        }
        [HttpGet]
        [Route("Average/{id:int}")]
        public async Task<IActionResult> GetAverage(int id)
        {
            var phone = _dbContext.Phone.FirstOrDefault(p => p.PhoneId == id);
            if(phone == null)
            {
                return StatusCode(400, Json(new {msg = "Phone does not exist"}));
            }
            else
            {
                var ratings = _dbContext.Rating.Where(r => r.PhoneId == id).GroupBy(r => r.PhoneId).Select(g => new {Average = g.Average(r => r.ReviewRating)});
                return StatusCode(200, Json(ratings));
            }
        }
    }
}
