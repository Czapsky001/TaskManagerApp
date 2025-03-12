using Marvin.JsonPatch;
using TaskManagerApp.Model;
using TaskManagerApp.Model.Dto;
using TaskManagerApp.Model.Dto.Tasks;

namespace TaskManagerApp.Services.Task;

public interface ITaskService
{
    Task<IEnumerable<GetTaskDTO>> GetAllTasksAsync();
    Task<TaskModel> GetTaskByIdAsync(int id);
    Task<bool> AddTaskAsync(CreateTaskDto task);
    Task<bool> DeleteTaskAsync(int id);
    Task<bool> UpdateTaskAsync(UpdateTaskDto updateTaskDto);
}