﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Registeration.Data;
using Registeration.Models;

namespace Registeration.Pages.ApplicationUsers
{
    public class DetailsModel : PageModel
    {
        private readonly Registeration.Data.ApplicationUserContext _context;

        public DetailsModel(Registeration.Data.ApplicationUserContext context)
        {
            _context = context;
        }

        public ApplicationUser ApplicationUser { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ApplicationUser = await _context.ApplicationUser.FirstOrDefaultAsync(m => m.ID == id);

            if (ApplicationUser == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
