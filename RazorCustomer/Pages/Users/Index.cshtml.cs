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
    public class IndexModel : PageModel
    {
        private readonly RazorCustomer.Data.RazorCustomerContext _context;

        public IndexModel(RazorCustomer.Data.RazorCustomerContext context)
        {
            _context = context;
        }

        public User User { get;set; }

        public IActionResult OnGet()
        {
            var id = HttpContext.Session.GetInt32("_id");
            User = _context.User.FirstOrDefault(m => m.ID == id);
            if(User == null)
            {
                return RedirectToPage("../Index");
            }
            return Page();
        }
    }
}
