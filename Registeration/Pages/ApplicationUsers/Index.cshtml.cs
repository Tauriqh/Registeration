using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Registeration.Data;
using Registeration.Models;

namespace Registeration.Pages.ApplicationUsers
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly Registeration.Data.ApplicationUserContext _context;

        
        public IndexModel(Registeration.Data.ApplicationUserContext context)
        {
            _context = context;
        }

        public IList<ApplicationUser> ApplicationUser { get;set; }

        public async Task OnGetAsync()
        {
            ApplicationUser = await _context.ApplicationUser.ToListAsync();
        }
    }
}
