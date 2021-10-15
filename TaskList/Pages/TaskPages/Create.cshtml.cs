using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TaskList.Model;
using Task = System.Threading.Tasks.Task;

namespace TaskList.Pages.TaskPages
{
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _db;

        public CreateModel(ApplicationDbContext db)
        {
            _db = db;
        }

        [BindProperty]
        public Model.Task TaskProp { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost()
        {
            if(ModelState.IsValid)
            {
                await SaveTask(TaskProp);
                return RedirectToPage("Index");
            }
            else
            {
                return Page();
            }
        }

        public async Task SaveTask(Model.Task task)
        {
            task.UserId = User.Identity.Name;
            await _db.Task.AddAsync(task);
            await _db.SaveChangesAsync();
        }

        public async Task<IActionResult> OnGetLogout()
        {
            await HttpContext.SignOutAsync();
            return Redirect("/Index");
        }
    }
}
