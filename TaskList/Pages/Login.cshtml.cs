using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TaskList.Model;
using TaskList.Pages.TaskPages;
using Task = System.Threading.Tasks.Task;

namespace TaskList.Pages
{
    public class LoginModel : PageModel
    {
        private readonly ApplicationDbContext _db;

        public LoginModel(ApplicationDbContext db)
        {
            _db = db;
            AddTestData addTestData = new AddTestData(_db);
        }

        [BindProperty]
        public Users LoginUser { get; set; }

        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPost()
        {
            var LoginUserAtId = _db.Users.SingleOrDefault(x => x.UserId == LoginUser.UserId && x.Password == LoginUser.Password);

            if (LoginUserAtId == null)
            {
                ViewData["LoginFailed"] = "Invalid Username or incorrect password.";
                return Page();
            }

            var claims = new List<Claim>();
            claims.Add(new Claim("username", LoginUser.UserId));
            claims.Add(new Claim(ClaimTypes.NameIdentifier, LoginUser.UserId));
            claims.Add(new Claim(ClaimTypes.Name, LoginUser.UserId));
            var claimsIdentiy = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var claimsPrinciple = new ClaimsPrincipal(claimsIdentiy);
            await HttpContext.SignInAsync(claimsPrinciple);

            return RedirectToPage("TaskPages/Index");
        }
    }
}
