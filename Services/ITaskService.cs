using TaskManagerApp.Model;
using TaskManagerApp.Model.Dto;

namespace TaskManagerApp.Services;

public interface ITaskService
{
    Task<IEnumerable<TaskModel>> GetAllTasksAsync();
    Task<TaskModel> GetTaskByIdAsync(int id);
    Task<bool> AddTaskAsync(CreateTaskDto task);
    Task<bool> DeleteTaskAsync(int id);
    Task<bool> UpdateTaskAsync(UpdateTaskDto updateTask);
}