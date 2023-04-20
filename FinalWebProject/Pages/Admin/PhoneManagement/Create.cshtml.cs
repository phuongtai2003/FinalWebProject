using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using FinalWebProject.Data;
using FinalWebProject.ViewModel.Admin.PhoneManagement;
using Microsoft.IdentityModel.Tokens;
using FinalWebProject.Utils;

namespace FinalWebProject.Pages.Admin.PhoneManagement
{
    public class CreateModel : PageModel
    {
        private readonly FinalWebProject.Data.FinalDbContext _context;
        public SelectList ManufacturerList { get; set; }
        public CreateModel(FinalWebProject.Data.FinalDbContext context)
        {
            _context = context;
            ManufacturerList = new SelectList(_context.Manufacturer, "ManufacturerId", "ManufacturerName");
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public PhoneViewModelAdd Phone { get; set; }
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.Phone == null || Phone == null)
            {
                return Page();
            }
            var res = await CustomUtils.UploadFile(Phone.Image);
            var phone = new Phone
            {
                PhoneName = Phone.PhoneName,
                PhoneDescription = Phone.PhoneDescription,
                PhoneYear = Phone.PhoneYear,
                Image = res.ToString(),
                Price = Phone.Price,
                ManufacturerId = Phone.ManufacturerId,
            };

            _context.Phone.Add(phone);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
