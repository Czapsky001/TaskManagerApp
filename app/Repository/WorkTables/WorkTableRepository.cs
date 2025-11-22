
namespace TaskManagerApp.Repository.WorkTable;

using TaskManagerApp.Repository.WorkTables;
using TaskManagerApp.Model;
using Microsoft.EntityFrameworkCore;

public class WorkTableRepository : IWorkTableRepository
{
    private readonly DatabaseContext _dbContext;
    private readonly ILogger<WorkTableRepository> _logger;
    public WorkTableRepository(DatabaseContext databaseContext, ILogger<WorkTableRepository> logger)
    {
        _dbContext = databaseContext;
        _logger = logger;
    }
    public async Task<bool> AddWorkTableAsync(Model.WorkTable workTable)
    {
        try
        {
            await _dbContext.WorkTables.AddAsync(workTable);
            await _dbContext.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message, ex);
            return false;
        }

    }

    public async Task<bool> DeleteWorkTableAsync(WorkTable workTable)
    {
        try
        {
            _dbContext.WorkTables.Remove(workTable);
            await _dbContext.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message, ex);
            return false;
        }
    }

    public async Task<IEnumerable<WorkTable>> GetAllWorkTablesAsync()
    {
        try
        {
            var result = await _dbContext.WorkTables.Include(t => t.Tabs).ToListAsync();
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message, ex);
            return new List<WorkTable>();
        }
    }

    public async Task<WorkTable> GetWorkTableByIdAsync(int id)
    {
        try
        {
            var result = await _dbContext.WorkTables.Include(w => w.Tabs).Include(c => c.Company).FirstOrDefaultAsync(i => i.Id == id);
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message, ex);
            return null;
        }
    }

    public async Task<bool> UpdateWorkTableAsync(WorkTable workTable)
    {
        try
        {
            _dbContext.WorkTables.Update(workTable);
            await _dbContext.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message, ex);
            return false;
        }
    }
}
