using Microsoft.EntityFrameworkCore;
using TaskManagerApp.Model;
using TaskManagerApp.Model.Dto.Tasks;

namespace TaskManagerApp.Repository.Task;

public class TaskRepository : ITaskRepository
{
    private readonly DatabaseContext _dbContext;
    private readonly ILogger<TaskRepository> _logger;
    public TaskRepository(DatabaseContext dbContext, ILogger<TaskRepository> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }
    public async Task<bool> AddTaskAsync(TaskModel task)
    {
        try
        {
            await _dbContext.Tasks.AddAsync(task);
            await _dbContext.SaveChangesAsync();

            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return false;
        }
    }


    public async Task<bool> DeleteTaskAsync(TaskModel task)
    {
        try
        {
            _dbContext.Remove(task);
            await _dbContext.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return false;
        }
    }

    public async Task<IEnumerable<TaskModel>> GetAllTasksAsync()
    {
        try
        {
            var result = await _dbContext.Tasks.Include(t => t.SubTasks).Include(t => t.CreatedByUser).ToListAsync();
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return new List<TaskModel>();
        }
    }

    public async Task<TaskModel> GetTaskByIdAsync(int id)
    {
        try
        {
            return await _dbContext.Tasks.FirstOrDefaultAsync(t => t.Id == id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            throw;
        }
    }

    public async Task<bool> UpdateTaskAsync(TaskModel task)
    {
        try
        {
            _dbContext.Tasks.Update(task);
            await _dbContext.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return false;
        }
    }
    public async Task<IEnumerable<TaskModel>> GetTasksForUserId(string userId)
    {
        try
        {
            return await _dbContext.Tasks.Where(t => t.CreatedByUserId == userId).Include(u => u.CreatedByUser).Include(s => s.SubTasks).ToListAsync();

        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return new List<TaskModel>();
        }
    }
}
