using System;
using Microsoft.EntityFrameworkCore;

namespace TaskList.Model
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Task> Task { get; set; }

        public DbSet<Users> Users { get; set; }

    }
}
