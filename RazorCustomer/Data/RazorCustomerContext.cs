using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RazorCustomer.Models;

namespace RazorCustomer.Data
{
    public class RazorCustomerContext : DbContext
    {
        public RazorCustomerContext (DbContextOptions<RazorCustomerContext> options)
            : base(options)
        {
        }

        public DbSet<RazorCustomer.Models.User> User { get; set; }
    }
}
