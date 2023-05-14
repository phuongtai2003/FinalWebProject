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
    public class OrderController : Controller
    {
        private readonly FinalWebProject.Data.FinalDbContext _dbContext;
        public OrderController(FinalWebProject.Data.FinalDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        [HttpGet]
        [Route("GetOrder")]
        public async Task<IActionResult> GetOrderByUser()
        {
			Request.Headers.TryGetValue("X-Auth-Token", out StringValues headerValue);
			if (headerValue.IsNullOrEmpty())
			{
				return StatusCode(400, Json(new { msg = "Not Authenticated" }));
			}
            var customer = await _dbContext.Customer.FirstOrDefaultAsync(c => c.CustomerId == int.Parse(headerValue));
            if (customer == null)
            {
				return StatusCode(400, Json(new { msg = "Not Authenticated" }));
			}
			var items = await _dbContext.Order.Where(o => o.CustomerId == customer.CustomerId).ToListAsync();
            return StatusCode(200, Json(items));
        }
        [HttpPost]
        [Route("CreateOrder")]
        public async Task<IActionResult> CreateOrder(CreateOrderVM createOrderVM)
        {
			Request.Headers.TryGetValue("X-Auth-Token", out StringValues headerValue);
			if (headerValue.IsNullOrEmpty())
			{
				return StatusCode(400, Json(new { msg = "Not Authenticated" }));
			}

            var customer = _dbContext.Customer.FirstOrDefault(c => c.CustomerId == int.Parse(headerValue));
            if (customer == null)
            {
				return StatusCode(400, Json(new { msg = "Not Authenticated" }));
			}

			int total = 0;
            for(int i = 0; i<createOrderVM.Phones.Count; i++)
            {
                total = total + createOrderVM.Phones[i].Price * createOrderVM.Quantity[i];
            }
            var newOrder = new Order
            {
                TotalPrice = total,
                OrderedAt = DateTime.Now,
                CustomerId = customer.CustomerId,
            };
            _dbContext.Order.Add(newOrder);
            await _dbContext.SaveChangesAsync();

            for(int i = 0; i< createOrderVM.Phones.Count; i++)
            {
                int price = createOrderVM.Phones[i].Price * createOrderVM.Quantity[i];

				var newOrderDetails = new OrderDetails
                {
                    OrderId = newOrder.OrderId,
                    PhoneId = createOrderVM.Phones[i].PhoneId,
                    Price = price,
                    Quantity = createOrderVM.Quantity[i],
                };
                _dbContext.OrderDetails.Add(newOrderDetails);
                await _dbContext.SaveChangesAsync();
            }

            return StatusCode(200, Json(new { message = "Create Order Successfully" }));
        }

        [HttpGet]
        [Route("GetOrderDetails/{orderId:int}")]
        public async Task<IActionResult> GetOrderDetails(int orderId)
        {
			Request.Headers.TryGetValue("X-Auth-Token", out StringValues headerValue);
			if (headerValue.IsNullOrEmpty())
			{
				return StatusCode(400, Json(new { msg = "Not Authenticated" }));
			}
			var order = await _dbContext.Order.FirstOrDefaultAsync(o => o.OrderId == orderId);
            if (order == null)
            {
                return StatusCode(500, Json(new { error = "Order does not exist" }));
            }

            var orderDetails = await _dbContext.OrderDetails.Include(o => o.Phone).ThenInclude(p => p.Manufacturer).Where(o => o.OrderId == order.OrderId).ToListAsync();
            return StatusCode(200, Json(orderDetails));
        }
    }
}
