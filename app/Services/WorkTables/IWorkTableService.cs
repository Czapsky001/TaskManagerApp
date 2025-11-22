using TaskManagerApp.Model;
using TaskManagerApp.Model.Dto.WorkTables;

namespace TaskManagerApp.Services.WorkTables;

public interface IWorkTableService
{
    Task<bool> AddWorkTableAsync(CreateWorkTableDTO createWorkTableDTO);
    Task<WorkTable> GetWorkTableByIdAsync(int id);
    Task<bool> DeleteWorkTableAsync(int id);
    Task<bool> UpdateWorkTableAsync(int id, UpdateWorkTableDTO updateWorkTableDTO);
    Task<IEnumerable<WorkTable>> GetAllWorkTablesAsync();
}
