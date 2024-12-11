using Microsoft.EntityFrameworkCore;

namespace Sodio_API_Task.Models
{
    public class TaskContext : DbContext
    {
        public TaskContext() { }

        public TaskContext(DbContextOptions<TaskContext> options) : base(options) { }

        public DbSet<Task> Tasks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Task>().HasData(
                new Task
                {
                    Id = 1,
                    Title = "Morning",
                    Description = "Good Morning",
                    Status = TaskStatus.Completed,
                    DueDate = DateTime.Now
                },
                new Task
                {
                    Id = 2,
                    Title = "Afternoon",
                    Description = "Good Afternoon",
                    Status = TaskStatus.InProgress,
                    DueDate = DateTime.Now
                },
                new Task
                {
                    Id = 3,
                    Title = "Evening",
                    Description = "Good Evening",
                    Status = TaskStatus.Pending,
                    DueDate = DateTime.Now
                },
                new Task
                {
                    Id = 4,
                    Title = "Night",
                    Description = "Good Night",
                    Status = TaskStatus.Pending,
                    DueDate = DateTime.Now
                });
        }
    }
}
