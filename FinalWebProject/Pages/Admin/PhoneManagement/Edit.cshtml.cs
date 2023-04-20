using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FinalWebProject.Data;
using FinalWebProject.ViewModel.Admin.PhoneManagement;
using FinalWebProject.Utils;

namespace FinalWebProject.Pages.Admin.PhoneManagement
{
    public class EditModel : PageModel
    {
        private readonly FinalWebProject.Data.FinalDbContext _context;
        public SelectList ManufacturerList { get; set; }

        public EditModel(FinalWebProject.Data.FinalDbContext context)
        {
            _context = context;
            ManufacturerList = new SelectList(_context.Manufacturer, "ManufacturerId", "ManufacturerName");
        }

        [BindProperty]
        public PhoneViewModelEdit Phone { get; set; }

        [BindProperty]
        public IFormFile ImageFile { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Phone == null)
            {
                return NotFound();
            }

            var phone =  await _context.Phone.FirstOrDefaultAsync(m => m.PhoneId == id);
            if (phone == null)
            {
                return NotFound();
            }
            Phone = new PhoneViewModelEdit
            {
                PhoneId = phone.PhoneId,
                PhoneName = phone.PhoneName,
                PhoneDescription = phone.PhoneDescription,
                PhoneYear= phone.PhoneYear,
                Price = phone.Price,
                Image = phone.Image,
                ManufacturerId = phone.ManufacturerId,
            };
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            ModelState.Remove("Phone.Image");
            if (!ModelState.IsValid)
            {
                return Page();
            }
            Phone phone;
            var uri = await CustomUtils.UploadFile(ImageFile);
            phone = new Phone
            {
                PhoneId = Phone.PhoneId,
                PhoneName = Phone.PhoneName,
                PhoneDescription = Phone.PhoneDescription,
                PhoneYear = Phone.PhoneYear,
                Price = Phone.Price,
                Image = uri.ToString(),
                ManufacturerId = Phone.ManufacturerId,
            };
            _context.Attach(phone).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PhoneExists(phone.PhoneId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool PhoneExists(int id)
        {
          return (_context.Phone?.Any(e => e.PhoneId == id)).GetValueOrDefault();
        }
    }
}
