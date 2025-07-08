using Microsoft.Extensions.Options;
using MongoDB.Driver;
using TaskManagementApp.ApiService.Models;

namespace TaskManagementApp.ApiService.Services
{
    public class TaskService
    {
        private readonly IMongoCollection<TaskItem> _tasks;

        public TaskService(IMongoClient mongoClient)
        {
            var database = mongoClient.GetDatabase("taskDb");
            _tasks = database.GetCollection<TaskItem>("tasks");
        }

        public async Task<List<TaskItem>> GetAsync() => await _tasks.Find(_ => true).ToListAsync();
        public async Task<TaskItem?> GetAsync(string id) => await _tasks.Find(t => t.Id == id).FirstOrDefaultAsync();
        
        public async Task CreateAsync(TaskItem task)
        {
            try
            {
                // Ensure the task has an ID if not provided
                if (string.IsNullOrEmpty(task.Id))
                {
                    task.Id = Guid.NewGuid().ToString();
                }
                
                await _tasks.InsertOneAsync(task);
            }
            catch (Exception ex)
            {
                // Log the error (in a real app, use ILogger)
                Console.WriteLine($"Error creating task: {ex.Message}");
                throw;
            }
        }
        
        public async Task UpdateAsync(string id, TaskItem task) => await _tasks.ReplaceOneAsync(t => t.Id == id, task);
        public async Task DeleteAsync(string id) => await _tasks.DeleteOneAsync(t => t.Id == id);
    }
} 