namespace TaskManagerApp.Repository.WorkTables;

using TaskManagerApp.Model;

public interface IWorkTableRepository
{
    Task<IEnumerable<WorkTable>> GetAllWorkTablesAsync();
    Task<WorkTable> GetWorkTableByIdAsync(int id);
    Task<bool> AddWorkTableAsync(WorkTable workTable);
    Task<bool> UpdateWorkTableAsync(WorkTable workTable);
    Task<bool> DeleteWorkTableAsync(WorkTable workTable);
}
