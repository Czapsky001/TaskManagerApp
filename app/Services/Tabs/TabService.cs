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
        var tabToCreate = _mapper.Map<Tab>(createTabDTO);
        return await _tabRepository.AddTabAsync(tabToCreate);
    }

    public async Task<bool> DeleteTabAsync(int id)
    {
        var result = await _tabRepository.GetTabByIdAsync(id);
        return await _tabRepository.DeleteTabAsync(result);
    }

    public async Task<IEnumerable<Tab>> GetAllTabsAsync()
    {
        return await _tabRepository.GetAllTabsAsync();
    }

    public async Task<Tab> GetTabByIdAsync(int id)
    {
        return await _tabRepository.GetTabByIdAsync(id);
    }

    public async Task<bool> UpdateTabAsync(int id, TabDTO updateTabDTO)
    {

        var tabToUpdate = await _tabRepository.GetTabByIdAsync(id);
        var result = _mapper.Map(updateTabDTO, tabToUpdate);
        return await _tabRepository.UpdateTabAsync(result);
    }

}