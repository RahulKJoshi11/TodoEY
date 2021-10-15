using System;
using System.ComponentModel.DataAnnotations;

namespace TaskList.Model
{
    public class Task
    {
        public Task()
        {
        }

        [Key]
        public int PK_Id { get; set; }

        [Required(ErrorMessage = "Please enter a task")]
        [MaxLength(50)]
        [Display(Name = "Task Name")]
        public string TaskName { get; set; }

        [MaxLength(100)]
        public string Description { get; set; }

        //[DataType(DataType.Date)]
        public DateTime? Date { get; set; }

        [Display(Name = "Is Done")]
        public bool IsDone { get; set; }

        public string UserId { get; set; }

        [Display(Name = "Last Modified Date")]
        public DateTime? LastUpdatedDate { get; set; }

    }
}
