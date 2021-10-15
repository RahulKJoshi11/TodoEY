using System;
using System.ComponentModel.DataAnnotations;

namespace TaskList.Model
{
    public class Users
    {
        public Users()
        {
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name ="Username")]
        public string UserId { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
