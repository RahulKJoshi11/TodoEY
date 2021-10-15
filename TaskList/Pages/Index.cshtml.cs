using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using TaskList.Model;
using TaskList.Pages.TaskPages;

namespace TaskList.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _db;

        public IndexModel(ApplicationDbContext db)
        {
            _db = db;
        }

        public void OnGet()
        {
            AddTestData addTest = new AddTestData(_db);

            var dbUser = _db.Users.ToList();

            if(!dbUser.Any())
            {
                var testUser = new Users
                {
                    Id = 1,
                    UserId = "bob@og.com",
                    Password = "12345"
                };

                _db.Users.Add(testUser);

                _db.SaveChanges();
            }
        }

        public async Task<IActionResult> OnGetLogout()
        {
            await HttpContext.SignOutAsync();
            return Redirect("/Index");
        }
    }
}
