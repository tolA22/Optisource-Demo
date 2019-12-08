using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RazorCustomer.Models;

namespace RazorCustomer.Pages
{
    public class IndexModel : PageModel
    {
        private readonly RazorCustomer.Data.RazorCustomerContext _context;
        public string msg;

        public IndexModel(RazorCustomer.Data.RazorCustomerContext context)
        {
            _context = context;
        }

        [BindProperty]
        public User User { get; set; }

        public IActionResult OnGetAsync()
        {
            var id = HttpContext.Session.GetInt32("_id");
            if (id != null)
            {
                return RedirectToPage("./users/index");
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            
            if (User.Email == null || User.Password == null)
            {
                msg = "Email and Password are required";
                return Page();
            }
            var a = ModelState.GetValidationState("email") == Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Valid && ModelState.GetValidationState("password") == Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Valid;
            if (!a)
            {
                msg = "Invalid email or password";
                return Page();
            }


            var nUser = User;
            User = await _context.User.FirstOrDefaultAsync(m => m.Email == User.Email);


            if (User == null)
            {
                return NotFound();
            }
            else
            {
                if (new PasswordHasher<string>().VerifyHashedPassword("",User.Password, nUser.Password) == PasswordVerificationResult.Failed)
                {
                    return NotFound();
                }
            }
            //return Page();

            //_context.User.Add(User);
            //await _context.SaveChangesAsync();
            //Microsoft.AspNetCore.Session.ISessionStore sessionStore;
            //var a = sessionStore.Create("id");
            HttpContext.Session.SetInt32("_id",User.ID);

            //return RedirectToPage("./Users/Index" , new { id = User.ID });  

            return RedirectToPage("./Users/Index");
        }
    }
}
