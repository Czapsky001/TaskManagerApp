using Microsoft.AspNetCore.Mvc;
using TaskManagerApp.Model;
using TaskManagerApp.Model.Dto.WorkTables;
using TaskManagerApp.Services.WorkTables;

namespace TaskManagerApp.Controllers;

/// <summary>
/// Provides operations for managing work tables (creation, update, delete and retrieval).
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class WorkTableController : ControllerBase
{
    private readonly ILogger<WorkTableController> _logger;
    private readonly IWorkTableService _workTableService;

    /// <summary>
    /// Initializes a new instance of the <see cref="WorkTableController"/> class.
    /// </summary>
    /// <param name="logger">Logger instance.</param>
    /// <param name="workTableService">Service used for work table operations.</param>
    public WorkTableController(ILogger<WorkTableController> logger, IWorkTableService workTableService)
    {
        _logger = logger;
        _workTableService = workTableService;
    }

    /// <summary>
    /// Creates a new work table.
    /// </summary>
    /// <param name="createWorkTableDTO">The data used to create a new work table.</param>
    /// <returns>
    /// <c>true</c> if the work table was created successfully; otherwise <c>false</c>.
    /// </returns>
    [HttpPost("CreateWorkTable")]
    public async Task<ActionResult<bool>> CreateWorkTableAsync(
        [FromBody] CreateWorkTableDTO createWorkTableDTO)
    {
        var result = await _workTableService.AddWorkTableAsync(createWorkTableDTO);
        return Ok(result);
    }

    /// <summary>
    /// Deletes an existing work table.
    /// </summary>
    /// <param name="id">The identifier of the work table to delete.</param>
    /// <returns>
    /// <c>true</c> if the work table was deleted successfully; otherwise <c>false</c>.
    /// </returns>
    /// <remarks>
    /// The <paramref name="id"/> is taken from the query string.
    /// Example: <c>DELETE /api/WorkTable/DeleteWorkTable?id=1</c>.
    /// </remarks>
    [HttpDelete("DeleteWorkTable")]
    public async Task<ActionResult<bool>> DeleteWorkTableAsync([FromQuery] int id)
    {
        var result = await _workTableService.DeleteWorkTableAsync(id);
        return Ok(result);
    }

    /// <summary>
    /// Updates an existing work table.
    /// </summary>
    /// <param name="id">The identifier of the work table to update.</param>
    /// <param name="updateWorkTableDTO">The new values for the work table.</param>
    /// <returns>
    /// <c>true</c> if the work table was updated successfully; otherwise <c>false</c>.
    /// </returns>
    /// <remarks>
    /// The <paramref name="id"/> is taken from the query string.
    /// Example: <c>PUT /api/WorkTable/UpdateWorkTable?id=1</c>.
    /// </remarks>
    [HttpPut("UpdateWorkTable")]
    public async Task<ActionResult<bool>> UpdateWorkTableAsync(
        [FromQuery] int id,
        [FromBody] UpdateWorkTableDTO updateWorkTableDTO)
    {
        var result = await _workTableService.UpdateWorkTableAsync(id, updateWorkTableDTO);
        return Ok(result);
    }

    /// <summary>
    /// Gets a work table by its identifier.
    /// </summary>
    /// <param name="id">The identifier of the work table.</param>
    /// <returns>The requested <see cref="WorkTable"/> if found.</returns>
    /// <remarks>
    /// The <paramref name="id"/> is taken from the query string.
    /// Example: <c>GET /api/WorkTable/GetWorkTableById?id=1</c>.
    /// </remarks>
    [HttpGet("GetWorkTableById")]
    public async Task<ActionResult<WorkTable>> GetWorkTableByIdAsync([FromQuery] int id)
    {
        var result = await _workTableService.GetWorkTableByIdAsync(id);
        return Ok(result);
    }

    /// <summary>
    /// Gets all work tables.
    /// </summary>
    /// <returns>A collection of all <see cref="WorkTable"/> entities.</returns>
    [HttpGet("GetAllWorkTables")]
    public async Task<ActionResult<IEnumerable<WorkTable>>> GetAllWorkTablesAsync()
    {
        var result = await _workTableService.GetAllWorkTablesAsync();
        return Ok(result);
    }
}
