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
            var user = await _dbContext.Users.FindAsync(task.CreatedByUserId);

            if (user == null)
            {
                _logger.LogError($"User with ID {task.CreatedByUserId} not found.");
                return false;
            }
            task.CreatedByUser = user;
            user.Tasks.Add(task);

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
            var result = await _dbContext.Tasks.Include(t => t.SubTasks).Include(t => t.CreatedByUser).ToListAsync();
            return result;
        }catch(Exception ex)
        {
            _logger.LogError(ex.Message);
            return new List<TaskModel>();
        }
    }

    public async Task<TaskModel> GetTaskByIdAsync(int id)
    {
        try
        {
            var result = await _dbContext.Tasks.FindAsync(id);
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
