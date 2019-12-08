using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace RazorCustomer.Models
{
    public class User
    {
         public int ID { get; set; }

        [Required]
        [RegularExpression(@"^[A-Za-z]+", ErrorMessage = "First Name should consist of letters only")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [RegularExpression(@"^[A-Za-z]+", ErrorMessage = "Last Name should consist of letters only")]
        [Display(Name = "Last Name")]
        public string Lastname { get; set; }

        [Required]
        public string Password { get; set; }

        [EmailAddress]
        [Required]
        [Display(Name = "E-mail")]
        public string Email { get; set; }

    }
}
