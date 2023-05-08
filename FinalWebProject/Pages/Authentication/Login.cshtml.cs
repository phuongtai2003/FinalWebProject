using FinalWebProject.Utils;
using FinalWebProject.ViewModel.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FinalWebProject.Pages.Authetication
{
    public class LoginModel : PageModel
    {
        private readonly FinalWebProject.Data.FinalDbContext _context;
        public LoginModel(FinalWebProject.Data.FinalDbContext context)
        {
            _context= context;
        }
        [BindProperty]
        public LoginViewModel LoginViewModel { get; set; }
        public void OnGet()
        {
        }
        public IActionResult OnPost() {
            if (ModelState.IsValid)
            {
                var isAccountantExist = _context.Accountant.FirstOrDefault(a => a.AccountantEmail.Equals(LoginViewModel.Email) && a.AccountantPassword.Equals(LoginViewModel.Password));
                var isResellerExist = _context.Reseller.FirstOrDefault(r => r.ResellerEmail.Equals(LoginViewModel.Email) && r.ResellerPassword.Equals(LoginViewModel.Password));
				if (LoginViewModel.Email.Equals("admin@gmail.com") && LoginViewModel.Password.Equals("admin123456")) {
                    HttpContext.Session.SetString("Username", "Admin");
                    HttpContext.Session.SetString("Type", "Admin");
                    return RedirectToPage("/Admin/Index");
                }
                else if (isAccountantExist != null)
                {
					HttpContext.Session.SetString("Username", isAccountantExist.AcccountantName);
					HttpContext.Session.SetString("Type", "Accountant");
                    SessionHelpers.SetObjectAsJson(HttpContext.Session, "accountantUser", isAccountantExist);
                    return RedirectToPage("/AccountantSite/Index");
				}
                else if(isResellerExist != null)
                {
					HttpContext.Session.SetString("Username", isResellerExist.ResellerName);
					HttpContext.Session.SetString("Type", "Reseller");
					SessionHelpers.SetObjectAsJson(HttpContext.Session, "resellerUser", isResellerExist);
                    return RedirectToPage("/ResellerSite/Index");
				}
                else
                {
                    ViewData["Error"] = "Either Email or Password are not correct";
                    return Page();
                }
            }
            return Page();
        }
    }
}
