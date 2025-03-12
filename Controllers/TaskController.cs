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
public class TaskController : Controller
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
        }catch(Exception ex)
        {
            _logger.LogError(ex.Message, ex);
            return BadRequest();
        }
    }
    [HttpPost("CreateTask")]
    public async Task<ActionResult<bool>> CreateTask(CreateTaskDto createTaskDto)
    {
        try
        {
            var task = await _taskService.AddTaskAsync(createTaskDto);
            return Ok(task);
        }catch (Exception ex)
        {
            _logger.LogError(ex.Message, ex);
            return false;
        }
    }

    [HttpPut("UpdateTask/{id}")]
    public async Task<ActionResult<TaskModel>> UpdateTask(int id, [FromBody] UpdateTaskDto updateTaskDto)
    {
        try
        {
            updateTaskDto.Id = id;
            var updatedTask = await _taskService.UpdateTaskAsync(updateTaskDto);

            return Ok(updatedTask);
        }
        catch (ArgumentException ex)
        {
            _logger.LogError(ex.Message, ex);
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message, ex);
            return BadRequest("Error updating task.");
        }
    }



}
