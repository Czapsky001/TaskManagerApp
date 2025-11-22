using Microsoft.AspNetCore.Mvc;
using TaskManagerApp.Model;
using TaskManagerApp.Model.Dto.WorkTables;
using TaskManagerApp.Services.WorkTables;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TaskManagerApp.Controllers;

[Route("api/[controller]")]
[ApiController]
public class WorkTableController : ControllerBase
{
    private readonly ILogger<WorkTableController> _logger;
    private readonly IWorkTableService _workTableService;
    public WorkTableController(ILogger<WorkTableController> logger, IWorkTableService workTableService)
    {
        _logger = logger;
        _workTableService = workTableService;
    }
    // GET: api/<WorkTableController>
    [HttpPost("CreateWorkTable")]
    public async Task<ActionResult<bool>> CreateWorkTable(CreateWorkTableDTO createWorkTableDTO)
    {
        try
        {

            return Ok(await _workTableService.AddWorkTableAsync(createWorkTableDTO));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message, ex);
            return BadRequest(ex.Message);
        }

    }
    [HttpDelete("DeleteWorkTable")]
    public async Task<ActionResult<bool>> DeleteWorkTableAsync(int id)
    {
        try
        {
            return Ok(await _workTableService.DeleteWorkTableAsync(id));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message, ex);
            return BadRequest(ex.Message);
        }
    }
    [HttpPut("UpdateWorkTable")]
    public async Task<ActionResult<bool>> UpdateWorkTableAsync(int id, UpdateWorkTableDTO updateWorkTableDTO)
    {
        try
        {
            return Ok(await _workTableService.UpdateWorkTableAsync(id, updateWorkTableDTO));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message, ex);
            return BadRequest(ex.Message);
        }
    }
    [HttpGet("GetWorkTableById")]
    public async Task<ActionResult<WorkTable>> GetWorkTableByIdAsync(int id)
    {
        try
        {
            return Ok(await _workTableService.GetWorkTableByIdAsync(id));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message, ex);
            return BadRequest(ex.Message);
        }
    }
    [HttpGet("GetAllWorkTables")]
    public async Task<IEnumerable<WorkTable>> GetAllWorkTablesAsync()
    {
        try
        {
            return await _workTableService.GetAllWorkTablesAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message, ex);
            return new List<WorkTable>();
        }
    }
}