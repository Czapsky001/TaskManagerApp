using TaskManagerApp.Model;

namespace TaskManagerApp.Repository.Tabs;

public interface ITabRepository
{
    Task<IEnumerable<Tab>> GetAllTabsAsync();
    Task<Tab> GetTabByIdAsync(int id);
    Task<bool> AddTabAsync(Tab tab);
    Task<bool> UpdateTabAsync(Tab tab);
    Task<bool> DeleteTabAsync(Tab tab);
}
