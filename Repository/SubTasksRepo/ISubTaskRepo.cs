using TaskManagerApp.Model.Dto.SubTasks;
using TaskManagerApp.Model;

namespace TaskManagerApp.Repository.SubTasksRepo;

public interface ISubTaskRepo
{
    Task<bool> CreateSubTaskAsync(SubTask createSubTask);
    Task<bool> UpdateSubTaskAsync(SubTask updateSubTask);
    Task<bool> DeleteSubTaskAsync(SubTask subTask);
    IEnumerable<SubTask> GetAllSubTasksForParentTaskId(int id);
    Task<SubTask> GetSubTaskByIdAsync(int subTaskId);
    IEnumerable<SubTask> GetSubTasksByUserId(string id);
}
