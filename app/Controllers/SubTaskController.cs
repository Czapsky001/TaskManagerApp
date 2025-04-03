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
    [HttpPut("UpdateSubTask/{id}")]
    public async Task<ActionResult<bool>> UpdateSubTask(int id, UpdateSubTaskDTO updateSubTaskDTO)
    {
        try
        {
            var subTaskToUpdate = await _subTaskService.UpdateSubTaskAsync(id, updateSubTaskDTO);
            return Ok(subTaskToUpdate);
        }catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    [HttpDelete("DeleteSubTask/{id}")]
    public async Task<ActionResult<bool>> DeleteSubTask(int id)
    {
        try
        {
            var subTaskToDelete = await _subTaskService.DeleteSubTaskAsync(id);
            return Ok(subTaskToDelete);
        }catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

}
