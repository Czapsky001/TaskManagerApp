using Marvin.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using TaskManagerApp.Model;
using TaskManagerApp.Model.Dto;
using TaskManagerApp.Model.Dto.SubTasks;
using TaskManagerApp.Model.Dto.Tasks;

namespace TaskManagerApp.Services.Task;

public interface ITaskService
{
    Task<IEnumerable<GetTaskDTO>> GetAllTasksAsync();
    Task<TaskModel> GetTaskByIdAsync(int id);
    Task<bool> AddTaskAsync(CreateSubTaskDto task);
    Task<bool> DeleteTaskAsync(int id);
    Task<bool> UpdateTaskAsync(int id, UpdateTaskDTO updateTaskDto);
    Task<IEnumerable<GetTaskDTO>> GetTasksForUserIdAsync(string userId);
}