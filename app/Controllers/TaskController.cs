using Marvin.JsonPatch;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManagerApp.Model;
using TaskManagerApp.Model.Dto;
using TaskManagerApp.Model.Dto.Tasks;
using TaskManagerApp.Services.Task;

namespace TaskManagerApp.Controllers;
[ApiController]
[Route("[controller]")]
public class TaskController : ControllerBase
{
    private readonly ITaskService _taskService;
    private readonly ILogger<TaskController> _logger;
    public TaskController(ITaskService taskService, ILogger<TaskController> logger)
    {
        _taskService = taskService;
        _logger = logger;
    }

    [HttpGet("GetAllTasks")]
    public async Task<ActionResult<IEnumerable<GetTaskDTO>>> GetAllTasks()
    {
        try
        {
            var tasks = await _taskService.GetAllTasksAsync();
            return Ok(tasks);
        } catch (Exception ex)
        {
            _logger.LogError(ex.Message, ex);
            return BadRequest();
        }
    }
    [HttpPost("CreateTask")]
    public async Task<ActionResult<bool>> CreateTask(CreateTaskDTO createTaskDto)
    {
        try
        {
            var task = await _taskService.AddTaskAsync(createTaskDto);
            return Ok(task);
        } catch (Exception ex)
        {
            _logger.LogError(ex.Message, ex);
            return false;
        }
    }

    [HttpPut("UpdateTask/{id}")]
    public async Task<ActionResult<TaskModel>> UpdateTask(int id, [FromBody] UpdateTaskDTO updateTaskDto)
    {
        try
        {
            var getTask = await _taskService.GetTaskByIdAsync(id);
            if (getTask == null) {
                return NotFound();
            }
            var updatedTask = await _taskService.UpdateTaskAsync(id, updateTaskDto);

            return Ok(updatedTask);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message, ex);
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("DeleteTask/{id}")]
    public async Task<ActionResult<bool>> DeleteTask(int id)
    {
        try
        {
            var deletedTask = await _taskService.DeleteTaskAsync(id);
            if (deletedTask)
            {
                return Ok(true);
            }
            return BadRequest("Task with this id does not exist");
        } catch (Exception ex)
        {
            _logger.LogError(ex.Message, ex);
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("GetTaskForUserId/{id}")]
    public async Task<ActionResult<IEnumerable<GetTaskDTO>>> GetTaskForUserId(string id)
    {
        try
        {
            var tasks = await _taskService.GetTasksForUserIdAsync(id);
            return Ok(tasks);
        }catch(Exception ex)
        {
            _logger.LogError(ex.Message, ex);
            return BadRequest(ex.Message);
        }
    }
}
