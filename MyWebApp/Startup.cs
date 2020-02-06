using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Session;
using Microsoft.Extensions.Caching.Memory;
using MyWebApp.Models;

namespace MyWebApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDistributedMemoryCache();
            services.AddSession(options =>
            {
                options.Cookie.Name = "session";
            });
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddDbContext<MyContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DatabaseConnectionString")));
            services.AddDbContext<MyContext>(options =>
                options.UseSqlServer(Helpers.GetRDSConnectionString()));

            //services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services
                .AddMvc(options => options.Filters.Add(new MyWebApp.Filters.LogFilter()))
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddDistributedSqlServerCache(options =>
            {
                options.ConnectionString = Helpers.GetRDSConnectionString();
                //options.ConnectionString = Configuration.GetConnectionString("DatabaseConnectionString");
                options.SchemaName = "dbo";
                options.TableName = "Cache";
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
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            app.UseSession();
            app.UseHttpsRedirection();
            app.UseStaticFiles();


            //app.UseMvc(routes =>
            //{
            //    routes.MapRoute(
            //        name: "default",
            //        template: "{controller=Home}/{action=Index}/{id?}");
            //});
            app.UseMvc(routes =>
            {
                routes.MapAreaRoute(
                    name: "adminArea",
                    areaName: "Admin",
                    template: "Admin/{controller=Default}/{action=Index}/{id?}");

                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
            app.UseCookiePolicy();
        }
    }
}
