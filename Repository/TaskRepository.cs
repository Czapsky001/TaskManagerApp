using Microsoft.EntityFrameworkCore;
using TaskManagerApp.Model;

namespace TaskManagerApp.Repository;

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
        }catch(Exception ex)
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
        }catch(Exception ex)
        {
            _logger.LogError(ex.Message);
            return false;
        }
    }

    public async Task<IEnumerable<TaskModel>> GetAllTasksAsync()
    {
        try
        {
            var result = await _dbContext.Tasks.Include(t => t.SubTasks).ToListAsync();
            return result;
        }catch(Exception ex)
        {
            _logger.LogError(ex.Message);
            return new List<TaskModel>();
        }
    }

    public Task<TaskModel> GetTaskByIdAsync(int id)
    {
        try
        {
            var result = _dbContext.Tasks.FirstOrDefaultAsync(t => t.Id == id);
            return result;
        }catch(Exception ex)
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
        }catch(Exception ex)
        {
            _logger.LogError(ex.Message);
            return false;
        }
    }
}
