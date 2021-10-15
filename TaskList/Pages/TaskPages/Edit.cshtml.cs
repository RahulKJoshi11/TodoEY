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
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _db;

        public EditModel(ApplicationDbContext db)
        {
            _db = db;
        }

        [BindProperty]
        public Model.Task TaskProp { get; set; }

        public async Task OnGet(int id)
        {
            TaskProp = await _db.Task.FindAsync(id);
        }

        public async Task<IActionResult> OnPost()
        {
            if(ModelState.IsValid)
            {
                var TaskAtId = await _db.Task.FindAsync(TaskProp.PK_Id);
                TaskAtId.TaskName = TaskProp.TaskName;
                TaskAtId.Date = TaskProp.Date;
                TaskAtId.Description = TaskProp.Description;
                TaskAtId.IsDone = TaskProp.IsDone;
                TaskAtId.LastUpdatedDate = System.DateTime.Now;

                await _db.SaveChangesAsync();

                return RedirectToPage("Index");
            }

            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostLogout()
        {
            await HttpContext.SignOutAsync();
            return Redirect("/Index");
        }
    }
}
