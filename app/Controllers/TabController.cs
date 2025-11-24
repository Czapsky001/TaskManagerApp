using Microsoft.AspNetCore.Mvc;
using TaskManagerApp.Model;
using TaskManagerApp.Model.Dto.Tabs;
using TaskManagerApp.Services.Tabs;

[Route("api/[controller]")]
[ApiController]
public class TabController : ControllerBase
{
    private readonly ITabService _tabService;

    public TabController(ITabService tabService)
    {
        _tabService = tabService;
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Tab>> GetTabByIdAsync(int id)
    {
        var tab = await _tabService.GetTabByIdAsync(id);

        if (tab is null)
            return NotFound();

        return Ok(tab);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Tab>>> GetAllTabsAsync()
    {
        var tabs = await _tabService.GetAllTabsAsync();
        return Ok(tabs);
    }

    [HttpPost]
    public async Task<ActionResult<bool>> CreateTabAsync([FromBody] TabDTO createTabDTO)
    {
        var result = await _tabService.CreateTabAsync(createTabDTO);
        return Ok(result);
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<bool>> UpdateTabAsync(int id, [FromBody] TabDTO updateTabDTO)
    {
        var result = await _tabService.UpdateTabAsync(id, updateTabDTO);
        return Ok(result);
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult<bool>> DeleteTabAsync(int id)
    {
        var result = await _tabService.DeleteTabAsync(id);
        return Ok(result);
    }
}
