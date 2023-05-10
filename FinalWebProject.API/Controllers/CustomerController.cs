using FinalWebProject.API.ViewModel;
using FinalWebProject.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Primitives;
using Microsoft.IdentityModel.Tokens;
using System.Diagnostics;

namespace FinalWebProject.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : Controller
    {
        private readonly FinalWebProject.Data.FinalDbContext _dbContext;
        public CustomerController(FinalWebProject.Data.FinalDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register(CustomerRegisterVM customerRegister)
        {
            if (_dbContext.Customer.FirstOrDefault(c => c.CustomerEmail == customerRegister.CustomerEmail) == null)
            {
                var newCustomer = new Customer
                {
                    CustomerEmail = customerRegister.CustomerEmail,
                    CustomerName = customerRegister.CustomerName,
                    CustomerPassword = customerRegister.CustomerPassword,
                    CustomerAddress = "",
                };
                _dbContext.Customer.Add(newCustomer);
                await _dbContext.SaveChangesAsync();
                return Ok(Json(new { message = "Account created successfully, login with the same credentials" }));
            }
            else
            {
                return StatusCode(400, Json(new { msg = "Email has already been registered" }));
            }
        }
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login(CustomerLoginVM customerLoginVM)
        {
            var customer = await _dbContext.Customer.FirstOrDefaultAsync(c => c.CustomerEmail == customerLoginVM.Email && c.CustomerPassword == customerLoginVM.Password);
            if(customer == null)
            {
                return StatusCode(400, Json(new {msg = "Wrong credentials, try again!"}));
            }
            else
            {
                return Ok(Json(new {Customer = customer}));
            }
        }
        [HttpGet]
        [Route("GetData")]
        public async Task<IActionResult> GetData()
        {
            Request.Headers.TryGetValue("X-Auth-Token", out StringValues headerValue);
            if(headerValue.IsNullOrEmpty())
            {
                return StatusCode(400, Json(new { msg = "Not Authenticated" }));
            }
            var customer = await _dbContext.Customer.FirstOrDefaultAsync(cus => cus.CustomerId == int.Parse(headerValue));
            if(customer != null)
            {
                return Ok(Json(new { Customer = customer }));
            }
            else
            {
                return StatusCode(400, Json(new { msg = "Something went wrong" }));
            }
        }
        [HttpPut]
        [Route("UpdateAddress")]
        public async Task<IActionResult> UpdateAddress(CustomerAddress customerAddress)
        {
            Request.Headers.TryGetValue("X-Auth-Token", out StringValues headerValue);
            if (headerValue.IsNullOrEmpty())
            {
                return StatusCode(400, Json(new { msg = "Not Authenticated" }));
            }
            var customer = await _dbContext.Customer.FirstOrDefaultAsync(cus => cus.CustomerId == int.Parse(headerValue));
            if(customer != null) {
                var newCustomer = new Customer { CustomerId = customer.CustomerId , CustomerEmail = customer.CustomerEmail, CustomerAddress = customerAddress.Address , CustomerName = customer.CustomerName, CustomerPassword = customer.CustomerPassword};
                _dbContext.Entry(customer).CurrentValues.SetValues(newCustomer);
                await _dbContext.SaveChangesAsync();
                return StatusCode(200, Json(new { message = "Update address success" }));
            }
            else
            {
                return StatusCode(500, Json(new { error = "User does not exist" }));
            }
        }
    }
}
