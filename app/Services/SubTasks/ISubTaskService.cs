using TaskManagerApp.Model.Dto;
using TaskManagerApp.Model.Dto.SubTasks;

namespace TaskManagerApp.Services.SubTasks;

public interface ISubTaskService
{
    IEnumerable<SubTaskDTO> GetAllSubTasksForParentTaskId(int id);
    Task<GetSubTaskDTO> GetSubTaskByIdAsync(int id);
    Task<bool> AddSubTaskAsync(CreateSubTaskDTO task);
    Task<bool> DeleteSubTaskAsync(int id);
    Task<bool> UpdateSubTaskAsync(int id, UpdateSubTaskDTO updateTaskDto);
    IEnumerable<SubTaskDTO> GetSubTasksForUserId(string userId);
}
