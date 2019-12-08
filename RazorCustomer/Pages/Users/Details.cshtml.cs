using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RazorCustomer.Data;
using RazorCustomer.Models;

namespace RazorCustomer.Models.Users
{
    public class DetailsModel : PageModel
    {
        private readonly RazorCustomer.Data.RazorCustomerContext _context;

        public DetailsModel(RazorCustomer.Data.RazorCustomerContext context)
        {
            _context = context;
        }

        public User User { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var id = HttpContext.Session.GetInt32("_id");
            if (id == null)
            {
                return RedirectToPage("../Index");
            }

            User = await _context.User.FirstOrDefaultAsync(m => m.ID == id);

            if (User == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
