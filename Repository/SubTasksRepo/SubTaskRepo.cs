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
        try
        {
            await _dbContext.AddAsync(createSubTask);
            await _dbContext.SaveChangesAsync();
            return true;
        }catch(Exception ex)
        {
            _logger.LogError(ex.Message, ex);
            return false;
        }
    }

    public async Task<bool> DeleteSubTaskAsync(SubTask subTask)
    {
        try
        {
             _dbContext.SubTasks.Remove(subTask);
            await _dbContext.SaveChangesAsync();
            return true;
        }catch(Exception ex)
        {
            _logger.LogError(ex.Message, ex);
            return false;
        }
    }

    public IEnumerable<SubTask> GetAllSubTasksForParentTaskId(int id)
    {
        try
        {
            var result = _dbContext.SubTasks.Where(p => p.ParentTaskId == id).ToList();
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message, ex);
            return new List<SubTask>();
        }
    }



    public  IEnumerable<SubTask> GetSubTasksByUserId(string id)
    {
        try
        {
            return _dbContext.SubTasks.Where(s => s.CreatedByUserId == id).ToList();
        }catch(Exception ex)
        {
            _logger.LogError(ex.Message, ex);
            return new List<SubTask>();
        }
    }

    public async Task<SubTask> GetSubTaskByIdAsync(int subTaskId)
    {
        try
        {
            return await _dbContext.SubTasks.FirstOrDefaultAsync(s => s.Id == subTaskId);
        }catch(Exception ex)
        {
            _logger.LogError(ex.Message, ex);
            return null;
        }
    }

    public async Task<bool> UpdateSubTaskAsync(SubTask updateSubTask)
    {
        try
        {
            _dbContext.SubTasks.Update(updateSubTask);
            await _dbContext.SaveChangesAsync();
            return true;
        }catch(Exception ex)
        {
            _logger.LogError(ex.Message, ex);
            return false;
        }
    }
}
