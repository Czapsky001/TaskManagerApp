using AutoMapper;
using TaskManagerApp.Model;
using TaskManagerApp.Model.Dto.Tabs;
using TaskManagerApp.Repository.Tabs;

namespace TaskManagerApp.Services.Tabs;

public class TabService : ITabService
{
    private readonly ILogger<TabService> _logger;
    private readonly ITabRepository _tabRepository;
    private readonly IMapper _mapper;
    public TabService(ILogger<TabService> logger, ITabRepository tabRepository, IMapper mapper)
    {
        _logger = logger;
        _tabRepository = tabRepository;
        _mapper = mapper;
    }
    public async Task<bool> CreateTabAsync(TabDTO createTabDTO)
    {
        try
        {
            var tabToCreate = _mapper.Map<Tab>(createTabDTO);
            return await _tabRepository.AddTabAsync(tabToCreate);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message, ex);
            return false;
        }
    }

    public async Task<bool> DeleteTabAsync(int id)
    {
        try
        {
            var result = await _tabRepository.GetTabByIdAsync(id); 
            return await _tabRepository.DeleteTabAsync(result);
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
            return await _tabRepository.GetAllTabsAsync();
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
            return await _tabRepository.GetTabByIdAsync(id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message, ex);
            return null;
        }
    }

    public async Task<bool> UpdateTabAsync(int id, TabDTO updateTabDTO)
    {
        try
        {
            var tabToUpdate = await _tabRepository.GetTabByIdAsync(id);
            var result = _mapper.Map(updateTabDTO, tabToUpdate);
            return await _tabRepository.UpdateTabAsync(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message, ex);
            return false;
        }
    }
}
