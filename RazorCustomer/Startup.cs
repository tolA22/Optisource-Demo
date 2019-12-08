using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RazorCustomer.Data;

namespace RazorCustomer
{
    public class Startup
    {
        //static string connectionstring;
        //static SqlConnection cnn;
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            //Startup.connectionstring = @"Server=localhost\MSSQLSERVER01;Database=master;Trusted_Connection=True;";
            //Startup.cnn = new SqlConnection(Startup.connectionstring);
            //Startup.cnn.Open();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddDbContext<RazorCustomerContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("RazorCustomerContext")));

            services.AddDistributedMemoryCache();
            services.AddSession(options =>
            {
                options.Cookie.Name = "Razor.Session";
                options.IdleTimeout = TimeSpan.FromSeconds(500);
                options.Cookie.IsEssential = true;
            });

           
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseSession();
            app.UseCookiePolicy();

            app.UseMvc();

           
        }
    }
}
