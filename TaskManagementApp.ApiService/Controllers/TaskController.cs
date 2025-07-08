using Microsoft.AspNetCore.Mvc;
using TaskManagementApp.ApiService.Models;
using TaskManagementApp.ApiService.Services;

namespace TaskManagementApp.ApiService.Controllers
{
    [ApiController]
    [Route("tasks")]
    public class TaskController : ControllerBase
    {
        private readonly TaskService _taskService;
        public TaskController(TaskService taskService)
        {
            _taskService = taskService;
        }

        [HttpGet]
        public async Task<ActionResult<List<TaskItem>>> Get() => await _taskService.GetAsync();

        [HttpGet("{id}")]
        public async Task<ActionResult<TaskItem>> Get(string id)
        {
            var task = await _taskService.GetAsync(id);
            if (task == null) return NotFound();
            return task;
        }

        [HttpPost]
        public async Task<ActionResult> Post(TaskItem task)
        {
            try
            {
                Console.WriteLine($"Received task: Title={task.Title}, Description={task.Description}");
                
                task.CreatedDate = DateTime.UtcNow;
                await _taskService.CreateAsync(task);
                
                Console.WriteLine($"Task created successfully with ID: {task.Id}");
                return CreatedAtAction(nameof(Get), new { id = task.Id }, task);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in POST /tasks: {ex.Message}");
                return StatusCode(500, new { error = "Failed to create task", details = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(string id, TaskItem task)
        {
            var existing = await _taskService.GetAsync(id);
            if (existing == null) return NotFound();
            task.Id = id;
            await _taskService.UpdateAsync(id, task);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            var existing = await _taskService.GetAsync(id);
            if (existing == null) return NotFound();
            await _taskService.DeleteAsync(id);
            return NoContent();
        }
    }
} 