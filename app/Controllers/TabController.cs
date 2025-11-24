using Microsoft.AspNetCore.Mvc;
using TaskManagerApp.Model;
using TaskManagerApp.Model.Dto.Tabs;
using TaskManagerApp.Services.Tabs;

[Route("api/[controller]")]
[ApiController]
/// <summary>
/// Provides CRUD operations for tab resources.
/// </summary>
public class TabController : ControllerBase
{
    private readonly ITabService _tabService;

    /// <summary>
    /// Initializes a new instance of the <see cref="TabController"/> class.
    /// </summary>
    /// <param name="tabService">Service used to manage tabs.</param>
    public TabController(ITabService tabService)
    {
        _tabService = tabService;
    }

    /// <summary>
    /// Gets a single tab by its identifier.
    /// </summary>
    /// <param name="id">The identifier of the tab.</param>
    /// <returns>
    /// Returns <see cref="Tab"/> when found,
    /// otherwise <see cref="NotFoundResult"/> if the tab does not exist.
    /// </returns>
    [HttpGet("{id:int}")]
    public async Task<ActionResult<Tab>> GetTabByIdAsync(int id)
    {
        var tab = await _tabService.GetTabByIdAsync(id);

        if (tab is null)
            return NotFound();

        return Ok(tab);
    }

    /// <summary>
    /// Gets all tabs.
    /// </summary>
    /// <returns>
    /// Returns a collection of <see cref="Tab"/> objects.
    /// </returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Tab>>> GetAllTabsAsync()
    {
        var tabs = await _tabService.GetAllTabsAsync();
        return Ok(tabs);
    }

    /// <summary>
    /// Creates a new tab.
    /// </summary>
    /// <param name="createTabDTO">The data used to create the tab.</param>
    /// <returns>
    /// Returns <c>true</c> if the tab was created successfully; otherwise <c>false</c>.
    /// </returns>
    [HttpPost]
    public async Task<ActionResult<bool>> CreateTabAsync([FromBody] TabDTO createTabDTO)
    {
        var result = await _tabService.CreateTabAsync(createTabDTO);
        return Ok(result);
    }

    /// <summary>
    /// Updates an existing tab.
    /// </summary>
    /// <param name="id">The identifier of the tab to update.</param>
    /// <param name="updateTabDTO">The new values for the tab.</param>
    /// <returns>
    /// Returns <c>true</c> if the tab was updated successfully; otherwise <c>false</c>.
    /// </returns>
    [HttpPut("{id:int}")]
    public async Task<ActionResult<bool>> UpdateTabAsync(int id, [FromBody] TabDTO updateTabDTO)
    {
        var result = await _tabService.UpdateTabAsync(id, updateTabDTO);
        return Ok(result);
    }

    /// <summary>
    /// Deletes an existing tab.
    /// </summary>
    /// <param name="id">The identifier of the tab to delete.</param>
    /// <returns>
    /// Returns <c>true</c> if the tab was deleted successfully; otherwise <c>false</c>.
    /// </returns>
    [HttpDelete("{id:int}")]
    public async Task<ActionResult<bool>> DeleteTabAsync(int id)
    {
        var result = await _tabService.DeleteTabAsync(id);
        return Ok(result);
    }
}
