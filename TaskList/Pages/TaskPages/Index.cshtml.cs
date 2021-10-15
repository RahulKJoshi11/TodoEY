using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TaskList.Model;
using Task = System.Threading.Tasks.Task;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;

namespace TaskList.Pages.TaskPages
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _db;

        public IndexModel(ApplicationDbContext db)
        {
            _db = db;
        }

        public IEnumerable<Model.Task> Tasks { get; set; }

        public async Task OnGet()
        {
            string id = User.Identity.Name;
            Tasks = await _db.Task.Where(m=> m.UserId == id).ToListAsync();

        }

        public async Task<IActionResult> OnPostDelete(int id)
        {
            var taskAtId = await _db.Task.FindAsync(id);
            if(taskAtId == null)
            {
                return NotFound();
            }
            _db.Task.Remove(taskAtId);

            await _db.SaveChangesAsync();

           
            return RedirectToPage();
        }

        public async Task<IActionResult> OnGetLogout()
        {
            await HttpContext.SignOutAsync();
            return Redirect("/Index");
        }
    }
}
