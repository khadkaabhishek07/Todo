using System.Collections.Generic;
using System.Threading.Tasks;
using Todo.Models;

namespace Todo.Services
{
    public interface ITodoService
    {

        Task AddTaskAsync(TodoTask task);

        
        Task<List<TodoTask>> GetTasksByUserIdAsync(int userId);
    }
}
