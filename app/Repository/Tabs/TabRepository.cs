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
        await _databaseContext.Tabs.AddAsync(tab);
        await _databaseContext.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteTabAsync(Tab tab)
    {
        _databaseContext.Tabs.Remove(tab);
        await _databaseContext.SaveChangesAsync();
        return true;
    }

    public async Task<IEnumerable<Tab>> GetAllTabsAsync()
    {
        return await _databaseContext.Tabs.ToListAsync();
    }

    public async Task<Tab> GetTabByIdAsync(int id)
    {
        return await _databaseContext.Tabs.FirstOrDefaultAsync(x => x.Id == id);
    }
    public async Task<bool> UpdateTabAsync(Tab tab)
    {
        _databaseContext.Tabs.Update(tab);
        await _databaseContext.SaveChangesAsync();
        return true;
    }
}