using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using RazorCustomer.Data;
using RazorCustomer.Models;

namespace RazorCustomer.Pages.Users
{
    public class LogoutModel : PageModel
    {
        private readonly RazorCustomer.Data.RazorCustomerContext _context;

        public LogoutModel(RazorCustomer.Data.RazorCustomerContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            HttpContext.Session.Clear();
            return RedirectToPage("./index");
        }

        [BindProperty]
        public User User { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.User.Add(User);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}