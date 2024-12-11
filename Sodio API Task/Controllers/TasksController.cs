using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sodio_API_Task.Models;
using Task = Sodio_API_Task.Models.Task;
using TaskStatus = Sodio_API_Task.Models.TaskStatus;

namespace Sodio_API_Task.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly TaskContext _context;
        private readonly ILogger<TasksController> _logger;

        public TasksController(ILogger<TasksController> logger, TaskContext context)
        {
            _logger = logger;
            _context = context;
        }
               

        [HttpGet]
        public IActionResult GetTasks(string? status, DateTime? dueDate, int pageNumber = 1, int pageSize = 10)
        {
            var query = _context.Tasks.AsQueryable();

            if (!string.IsNullOrEmpty(status) && Enum.TryParse(typeof(TaskStatus), status, true, out var parsedStatus))
            {
                query = query.Where(t => t.Status == (TaskStatus)parsedStatus);
            }

            if (dueDate.HasValue)
            {
                query = query.Where(t => t.DueDate <= dueDate.Value);
            }

            var paginatedTasks = query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            _logger.LogInformation("Getting all Tasks");
            return Ok(paginatedTasks);
        }

        [HttpGet("{id:int}", Name = "GetTaskById")]
        public IActionResult GetTaskById(int id)
        {
            var task = _context.Tasks.Find(id);

            if (task == null)
            {
                return NotFound(new { Message = "Task not found." });
            }

            _logger.LogInformation("Found task with the id" + id);
            return Ok(task);
        }

        [HttpPost]
        public IActionResult CreateTask(Task task)
        {
            if (string.IsNullOrEmpty(task.Title))
            {
                return BadRequest(new { Message = "Title is required." });
            }

            _context.Tasks.Add(task);
            _context.SaveChanges();

            _logger.LogInformation($"Created task: {task.Title}");
            return CreatedAtAction(nameof(GetTaskById), new { id = task.Id }, task);
        }

        [HttpPut("{id:int}")]
        public IActionResult UpdateTask(int id, Task updatedTask)
        {
            var task = _context.Tasks.Find(id);

            if (task == null)
            {
                return NotFound(new { Message = "Task not found." });
            }

            _context.Tasks.Update(updatedTask);
            //task.Title = updatedTask.Title;
            //task.Description = updatedTask.Description;
            //task.Status = updatedTask.Status;
            //task.DueDate = updatedTask.DueDate;

            _context.SaveChanges();

            _logger.LogInformation($"Task Updated: {updatedTask.Id}");
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public IActionResult DeleteTask(int id)
        {
            var task = _context.Tasks.Find(id);

            if (task == null)
            {
                return NotFound(new { Message = "Task not found." });
            }

            _context.Tasks.Remove(task);
            _context.SaveChanges();

            _logger.LogInformation($"{task.Id} deleted");
            return NoContent();
        }
    }
}
