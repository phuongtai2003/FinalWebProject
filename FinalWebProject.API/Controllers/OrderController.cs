using Microsoft.AspNetCore.Mvc;

namespace FinalWebProject.API.Controllers
{
    public class OrderController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
