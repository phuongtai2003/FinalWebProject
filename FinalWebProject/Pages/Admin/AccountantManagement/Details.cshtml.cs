﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using FinalWebProject.Data;

namespace FinalWebProject.Pages.Admin.AccountantManagement
{
    public class DetailsModel : PageModel
    {
        private readonly FinalWebProject.Data.FinalDbContext _context;

        public DetailsModel(FinalWebProject.Data.FinalDbContext context)
        {
            _context = context;
        }

      public Accountant Accountant { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Accountant == null)
            {
                return NotFound();
            }

            var accountant = await _context.Accountant.FirstOrDefaultAsync(m => m.AccountantID == id);
            if (accountant == null)
            {
                return NotFound();
            }
            else 
            {
                Accountant = accountant;
            }
            return Page();
        }
    }
}
