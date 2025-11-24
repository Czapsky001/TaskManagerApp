
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
            await _dbContext.WorkTables.AddAsync(workTable);
            await _dbContext.SaveChangesAsync();
            return true;
    }

    public async Task<bool> DeleteWorkTableAsync(WorkTable workTable)
    {
            _dbContext.WorkTables.Remove(workTable);
            await _dbContext.SaveChangesAsync();
            return true;
    }

    public async Task<IEnumerable<WorkTable>> GetAllWorkTablesAsync()
    {
            var result = await _dbContext.WorkTables.Include(t => t.Tabs).ToListAsync();
            return result;
    }

    public async Task<WorkTable> GetWorkTableByIdAsync(int id)
    {
            var result = await _dbContext.WorkTables.Include(w => w.Tabs).Include(c => c.Company).FirstOrDefaultAsync(i => i.Id == id);
            return result;
    }

    public async Task<bool> UpdateWorkTableAsync(WorkTable workTable)
    {
            _dbContext.WorkTables.Update(workTable);
            await _dbContext.SaveChangesAsync();
            return true;
    }
}
