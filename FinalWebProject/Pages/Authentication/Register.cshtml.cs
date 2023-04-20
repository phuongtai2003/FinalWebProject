using FinalWebProject.Data;
using FinalWebProject.ViewModel.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FinalWebProject.Pages.Authetication
{
    public class RegisterModel : PageModel
    {
        private readonly FinalWebProject.Data.FinalDbContext _context;
        public SelectList WarehouseList { get; set; }
        public RegisterModel(FinalWebProject.Data.FinalDbContext context)
        {
            _context = context;
            WarehouseList = new SelectList(_context.Warehouse, "WarehouseId", "WarehouseName");
        }
        public void OnGet()
        {
            
        }
        [BindProperty]
        public AccountantRegisterViewModel AccountantRegisterViewModel { get; set; }
        public async Task<IActionResult> OnPostAsync()
        {
            if(!ModelState.IsValid) {
                return Page();
            }
            var exist = _context.Accountant.FirstOrDefault(a => a.AccountantEmail.Equals(AccountantRegisterViewModel.AccountantEmail));
            if(exist == null && !AccountantRegisterViewModel.AccountantEmail.Equals("admin@gmail.com"))
            {
                Accountant account = new Accountant
                {
                    AcccountantName = AccountantRegisterViewModel.AcccountantName,
                    AccountantEmail = AccountantRegisterViewModel.AccountantEmail,
                    AccountantPassword = AccountantRegisterViewModel.AccountantPassword,
                    WarehouseID = AccountantRegisterViewModel.WarehouseID,
                };
                _context.Accountant.Add(account);
                await _context.SaveChangesAsync();
                return RedirectToPage("../Index");
            }
            return Page();
        }
    }
}
