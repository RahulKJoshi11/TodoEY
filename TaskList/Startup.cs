using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TaskList.Model;

namespace TaskList
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
            //services.AddDbContext<ApplicationDbContext>(option => option.UseSqlite(Configuration.GetConnectionString("DefaultConnection")));

            var keepAliveConnection = new SqliteConnection("DataSource=:memory:");
            keepAliveConnection.Open();

            SqliteCommand sqliteCommand = new SqliteCommand();
            sqliteCommand.Connection = keepAliveConnection;
            sqliteCommand.CommandText = "Create table users (id integer primary key autoincrement,UserId string, Password string)";

            sqliteCommand.ExecuteNonQuery();

            SqliteCommand sqliteCommand1 = new SqliteCommand();
            sqliteCommand1.Connection = keepAliveConnection;
            sqliteCommand1.CommandText = "Create table Task (pk_id integer PRIMARY KEY AUTOINCREMENT,TaskName string, Description string, Date Datetime, IsDone int, LastUpdatedDate Datetime, UserId string)";

            sqliteCommand1.ExecuteNonQuery();

            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlite(keepAliveConnection);
            });

            //services.AddRazorPages();

            services.AddRazorPages(options =>
            {
                options.Conventions.AuthorizePage("/TaskPages");
            });

            services.AddDistributedMemoryCache();
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options => {
                options.LoginPath = "/Login";
                options.LogoutPath = "/../Index";
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });
        }
    }
}
