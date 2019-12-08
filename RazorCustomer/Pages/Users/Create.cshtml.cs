using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RazorCustomer.Data;
using RazorCustomer.Models;

namespace RazorCustomer.Models.Users
{
    public class CreateModel : PageModel
    {
        private readonly RazorCustomer.Data.RazorCustomerContext _context;
        public string msg { get; set; }
        public CreateModel(RazorCustomer.Data.RazorCustomerContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public User User { get; set; }

        public  void check(User User)
        {
            User.Email = User.Email.ToLower();
            User.FirstName = User.FirstName.ToUpper();
            User.Lastname = User.Lastname.ToUpper();
            
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            check(User);
            
            
            var User2 = await _context.User.FirstOrDefaultAsync(m => m.Email == User.Email);
            if(User2 != null)
            {
                msg = "An account already exists with this email";
                return Page();
            }

            var hp = new PasswordHasher<string>();
            User.Password = hp.HashPassword("", User.Password); ;
            _context.User.Add(User);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}