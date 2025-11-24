using TaskManagerApp.Model;
using Microsoft.EntityFrameworkCore;

namespace TaskManagerApp.Repository.Tabs;

public class TabRepository : ITabRepository
{
    private readonly ILogger<TabRepository> _logger;
    private readonly DatabaseContext _databaseContext;
    public TabRepository(ILogger<TabRepository> logger, DatabaseContext databaseContext)
    {
        _logger = logger;
        _databaseContext = databaseContext;
    }
    public async Task<bool> AddTabAsync(Tab tab)
    {
        try
        {
            await _databaseContext.Tabs.AddAsync(tab);
            await _databaseContext.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message, ex);
            return false;
        }
    }

    public async Task<bool> DeleteTabAsync(Tab tab)
    {
        try
        {
            _databaseContext.Tabs.Remove(tab);
            await _databaseContext.SaveChangesAsync();
            return true;

        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message, ex);
            return false;
        }
    }
   public async Task<IEnumerable<Tab>> GetAllTabsAsync()
    {
        try
        {
            return await _databaseContext.Tabs.ToListAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message, ex);
            return new List<Tab>();
        }
    }

    public async Task<Tab> GetTabByIdAsync(int id)
    {
        try
        {
            return await _databaseContext.Tabs.FirstOrDefaultAsync(x => x.Id == id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message, ex);
            return null;
        }
    }

    public async Task<bool> UpdateTabAsync(Tab tab)
    {
        try
        {
            _databaseContext.Tabs.Update(tab);
            await _databaseContext.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message, ex);
            return false;
        }
    }
}
