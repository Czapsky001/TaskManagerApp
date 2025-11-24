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
            await _dbContext.Tasks.AddAsync(task);
            await _dbContext.SaveChangesAsync();

            return true;
    }


    public async Task<bool> DeleteTaskAsync(TaskModel task)
    {
            var subtasks = await _dbContext.SubTasks.Where(st => st.ParentTaskId == task.Id).ToListAsync();
            _dbContext.SubTasks.RemoveRange(subtasks);
            _dbContext.Remove(task);
            await _dbContext.SaveChangesAsync();
            return true;
    }

    public async Task<IEnumerable<TaskModel>> GetAllTasksAsync()
    {
            var result = await _dbContext.Tasks.Include(t => t.SubTasks).Include(t => t.CreatedByUser).ToListAsync();
            return result;
    }

    public async Task<TaskModel> GetTaskByIdAsync(int id)
    {
            return await _dbContext.Tasks.FirstOrDefaultAsync(t => t.Id == id);
    }

    public async Task<bool> UpdateTaskAsync(TaskModel task)
    {
            _dbContext.Tasks.Update(task);
            await _dbContext.SaveChangesAsync();
            return true;
    }
    public async Task<IEnumerable<TaskModel>> GetTasksForUserId(string userId)
    {
            return await _dbContext.Tasks.Where(t => t.CreatedByUserId == userId).Include(u => u.CreatedByUser).Include(s => s.SubTasks).ToListAsync();
    }
}
