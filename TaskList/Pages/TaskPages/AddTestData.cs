using System;
using System.Linq;
using TaskList.Model;

namespace TaskList.Pages.TaskPages
{
    public class AddTestData
    {
        private readonly ApplicationDbContext _db;

        public AddTestData(ApplicationDbContext db)
        {
            _db = db;
        }

        public IQueryable<Users> users => _db.Users;
        public IQueryable<Task> tasks => _db.Task;

        public void Add<EntityType>(EntityType entity) => _db.Add(entity);

        public void SaveChanges() => _db.SaveChanges();

    }
}
