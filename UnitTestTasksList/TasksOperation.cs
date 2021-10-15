using Microsoft.VisualStudio.TestTools.UnitTesting;
using TaskList.Model;
using TaskList.Pages.TaskPages;

namespace UnitTestTasksList
{
    [TestClass]
    public class TasksOperations
    {
        private readonly ApplicationDbContext _db;

        public TasksOperations(ApplicationDbContext db)
        {
            _db = db;
        }

        string taskName = "Shopping";
        string description = "12:30 pm from Tesco";

        Task task1;
        [TestInitialize]
        public void SetValues()
        {
            task1 = new Task { TaskName=taskName, Description=description, IsDone = false};
        }

        [TestMethod]
        public void CreateTasks()
        {
            CreateModel createModel = new CreateModel(_db);
            createModel.SaveTask(task1);

            var TaskData =  _db.Task.Find(1);

            Assert.AreEqual(TaskData.TaskName, taskName);
            Assert.AreEqual(TaskData.Description, description);
        }
    }
}
