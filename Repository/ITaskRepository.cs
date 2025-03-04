using TaskManagerApp.Model;

namespace TaskManagerApp.Repository;

public interface ITaskRepository
{
    Task<IEnumerable<TaskModel>> GetAllTasksAsync();
    Task<TaskModel> GetTaskByIdAsync(int id);
    Task<bool> AddTaskAsync(TaskModel task);
    Task<bool> UpdateTaskAsync(TaskModel task);
    Task<bool> DeleteTaskAsync(TaskModel task);
}
