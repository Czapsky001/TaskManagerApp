using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManagerApp.Model.Dto.SubTasks;
using TaskManagerApp.Services.SubTasks;

namespace TaskManagerApp.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SubTaskController : ControllerBase
{
    private readonly ILogger<SubTaskController> _logger;
    private readonly ISubTaskService _subTaskService;

    public SubTaskController(ILogger<SubTaskController> logger, ISubTaskService subTaskService)
    {
        _logger = logger;
        _subTaskService = subTaskService;
    }
    [HttpGet("GetAllSubTasksForParentTaskId/{id}")]
    public ActionResult<IEnumerable<SubTaskDTO>> GetAllSubTasksForParentTaskId(int id)
    {
        try
        {
            var subTasks = _subTaskService.GetAllSubTasksForParentTaskId(id);
            return Ok(subTasks);
        }catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    [HttpPost("CreateSubTask")]
    public async Task<ActionResult<bool>> CreateSubTask(CreateSubTaskDTO createSubTaskDTO)
    {
        try
        {
            await _subTaskService.AddSubTaskAsync(createSubTaskDTO);
            return Ok(true); 
        }catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    [HttpGet("GetSubTasksForUserId/{id}")]
    public ActionResult<IEnumerable<SubTaskDTO>> GetSubTasksForUserId(string id)
    {
        try
        {
            var subTasks = _subTaskService.GetSubTasksForUserId(id);
            return Ok(subTasks);
        }catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

}
