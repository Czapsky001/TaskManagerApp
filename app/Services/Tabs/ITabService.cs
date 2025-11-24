using TaskManagerApp.Model;
using TaskManagerApp.Model.Dto.Tabs;

namespace TaskManagerApp.Services.Tabs;

public interface ITabService
{
    Task<bool> CreateTabAsync(TabDTO createTabDTO);
    Task<bool> DeleteTabAsync(int id);
    Task<bool> UpdateTabAsync(int id, TabDTO updateTabDTO);
    Task<Tab> GetTabByIdAsync(int id);
    Task<IEnumerable<Tab>> GetAllTabsAsync();
}
