using AutoMapper;
using TaskManagerApp.Model;
using TaskManagerApp.Model.Dto.WorkTables;
using TaskManagerApp.Repository.WorkTables;

namespace TaskManagerApp.Services.WorkTables;

public class WorkTableService : IWorkTableService
{
    private readonly IWorkTableRepository _workTableRepository;
    private readonly ILogger<WorkTableService> _logger;
    private readonly IMapper _mapper;
    public WorkTableService(IWorkTableRepository workTableRepository, ILogger<WorkTableService> logger, IMapper mapper)
    {
        _logger = logger;
        _workTableRepository = workTableRepository;
        _mapper = mapper;
    }
    public async Task<bool> AddWorkTableAsync(CreateWorkTableDTO createWorkTableDTO)
    {
            var workTableToCreate = _mapper.Map<WorkTable>(createWorkTableDTO);
            return await _workTableRepository.AddWorkTableAsync(workTableToCreate);
    }

    public async Task<bool> DeleteWorkTableAsync(int id)
    {
            var result = await _workTableRepository.GetWorkTableByIdAsync(id);
            return await _workTableRepository.DeleteWorkTableAsync(result);
    }

    public async Task<IEnumerable<WorkTable>> GetAllWorkTablesAsync()
    {
            return await _workTableRepository.GetAllWorkTablesAsync();
    }

    public async Task<WorkTable> GetWorkTableByIdAsync(int id)
    {
            return await _workTableRepository.GetWorkTableByIdAsync(id);
    }

    public async Task<bool> UpdateWorkTableAsync(int id, UpdateWorkTableDTO updateWorkTableDTO)
    {
            var workTableToUpdate = await _workTableRepository.GetWorkTableByIdAsync(id);
            var result = _mapper.Map(updateWorkTableDTO, workTableToUpdate);
            await _workTableRepository.UpdateWorkTableAsync(result);
            return true;
    }
}
