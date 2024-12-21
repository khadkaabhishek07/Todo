using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Todo.Models;

namespace Todo.Services
{
    public class TodoService : ITodoService
    {
        private readonly string tasksFilePath = Path.Combine(AppContext.BaseDirectory, "TodoTasks.json");

        public async Task AddTaskAsync(TodoTask task)
        {
            try
            {
                var tasks = await LoadTasksAsync();
                task.Id = tasks.Count > 0 ? tasks.Max(t => t.Id) + 1 : 1;
                tasks.Add(task);
                await SaveTasksAsync(tasks);
            }
            catch (Exception ex)
            {
                // Log the exception (you can use a logging framework here)
                Console.WriteLine($"Error adding task: {ex.Message}");
                throw; // Rethrow or handle as needed
            }
        }

        public async Task<List<TodoTask>> GetTasksByUserIdAsync(int userId)
        {
            try
            {
                var tasks = await LoadTasksAsync();
                // Return tasks filtered by userId or an empty list if tasks is null
                return (tasks ?? new List<TodoTask>()).Where(t => t.UserId == userId).ToList();
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine($"Error retrieving tasks for user {userId}: {ex.Message}");
                return new List<TodoTask>(); // Return an empty list in case of an exception
            }
        }




        private async Task<List<TodoTask>> LoadTasksAsync()
        {
            try
            {
                if (!File.Exists(tasksFilePath))
                {
                    // If the file does not exist, create it with an empty list
                    var emptyList = new List<TodoTask>();
                    await SaveTasksAsync(emptyList);
                    return emptyList;
                }

                var json = await File.ReadAllTextAsync(tasksFilePath);
                return JsonSerializer.Deserialize<List<TodoTask>>(json) ?? new List<TodoTask>();
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine($"Error loading tasks: {ex.Message}");
                throw; // Rethrow or handle as needed
            }
        }

        private async Task SaveTasksAsync(List<TodoTask> tasks)
        {
            try
            {
                var json = JsonSerializer.Serialize(tasks, new JsonSerializerOptions { WriteIndented = true });
                await File.WriteAllTextAsync(tasksFilePath, json);
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine($"Error saving tasks: {ex.Message}");
                throw; // Rethrow or handle as needed
            }
        }
    }
}
