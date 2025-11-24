using System.Data.Entity;
using TaskManagerApp.Model;
using TaskManagerApp.Model.Dto.SubTasks;

namespace TaskManagerApp.Repository.SubTasksRepo;

public class SubTaskRepo : ISubTaskRepo
{
    private readonly ILogger<SubTaskRepo> _logger;
    private readonly DatabaseContext _dbContext;

    public SubTaskRepo(ILogger<SubTaskRepo> logger, DatabaseContext dbContext)
    {
        _logger = logger;
        _dbContext = dbContext;
    }
    public async Task<bool> CreateSubTaskAsync(SubTask createSubTask)
    {
            await _dbContext.AddAsync(createSubTask);
            await _dbContext.SaveChangesAsync();
            return true;
    }

    public async Task<bool> DeleteSubTaskAsync(SubTask subTask)
    {
             _dbContext.SubTasks.Remove(subTask);
            await _dbContext.SaveChangesAsync();
            return true;
    }

    public IEnumerable<SubTask> GetAllSubTasksForParentTaskId(int id)
    {
            var result = _dbContext.SubTasks.Where(p => p.ParentTaskId == id).ToList();
            return result;
    }



    public IEnumerable<SubTask> GetSubTasksByUserId(string id)
    {
            return _dbContext.SubTasks.Where(s => s.CreatedByUserId == id).ToList();
    }

    public async Task<SubTask> GetSubTaskByIdAsync(int subTaskId)
    {
            return await _dbContext.SubTasks.FindAsync(subTaskId);
    }

    public async Task<bool> UpdateSubTaskAsync(SubTask updateSubTask)
    {
            _dbContext.SubTasks.Update(updateSubTask);
            await _dbContext.SaveChangesAsync();
            return true;
    }
}
