using Marvin.JsonPatch;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManagerApp.Model;
using TaskManagerApp.Model.Dto;
using TaskManagerApp.Model.Dto.Tasks;
using TaskManagerApp.Services.Task;

namespace TaskManagerApp.Controllers
{
	/// <summary>
	/// Provides operations for managing tasks, including creation, retrieval, update, and deletion.
	/// </summary>
	[ApiController]
	[Route("api/[controller]")]
	public class TaskController : ControllerBase
	{
		private readonly ITaskService _taskService;
		private readonly ILogger<TaskController> _logger;

		/// <summary>
		/// Initializes a new instance of the <see cref="TaskController"/> class.
		/// </summary>
		/// <param name="taskService">Service that handles task operations.</param>
		/// <param name="logger">Logger instance.</param>
		public TaskController(ITaskService taskService, ILogger<TaskController> logger)
		{
			_taskService = taskService;
			_logger = logger;
		}

		/// <summary>
		/// Retrieves all tasks.
		/// </summary>
		/// <returns>A collection of tasks.</returns>
		[HttpGet("GetAllTasks")]
		public async Task<ActionResult<IEnumerable<GetTaskDTO>>> GetAllTasks()
		{
			var tasks = await _taskService.GetAllTasksAsync();
			return Ok(tasks);
		}

		/// <summary>
		/// Creates a new task.
		/// </summary>
		/// <param name="createTaskDto">The data required to create a new task.</param>
		/// <returns><c>true</c> if the task was created successfully; otherwise <c>false</c>.</returns>
		[HttpPost("CreateTask")]
		public async Task<ActionResult<bool>> CreateTask([FromBody] CreateTaskDTO createTaskDto)
		{
			var task = await _taskService.AddTaskAsync(createTaskDto);
			return Ok(task);
		}

		/// <summary>
		/// Updates an existing task.
		/// </summary>
		/// <param name="id">The identifier of the task to update.</param>
		/// <param name="updateTaskDto">The updated task data.</param>
		/// <returns>The updated task.</returns>
		[HttpPut("UpdateTask/{id:int}")]
		public async Task<ActionResult<TaskModel>> UpdateTask(
			[FromRoute] int id,
			[FromBody] UpdateTaskDTO updateTaskDto)
		{
			var existingTask = await _taskService.GetTaskByIdAsync(id);

			if (existingTask == null)
				return NotFound();

			var updatedTask = await _taskService.UpdateTaskAsync(id, updateTaskDto);
			return Ok(updatedTask);
		}

		/// <summary>
		/// Deletes a task by its identifier.
		/// </summary>
		/// <param name="id">The identifier of the task to delete.</param>
		/// <returns><c>true</c> if the deletion was successful.</returns>
		[HttpDelete("DeleteTask/{id:int}")]
		public async Task<ActionResult<bool>> DeleteTask([FromRoute] int id)
		{
			var deletedTask = await _taskService.DeleteTaskAsync(id);

			if (deletedTask)
				return Ok(true);

			return BadRequest("Task with this id does not exist");
		}

		/// <summary>
		/// Retrieves tasks assigned to a specific user.
		/// </summary>
		/// <param name="id">The user identifier.</param>
		/// <returns>A collection of tasks belonging to the specified user.</returns>
		[HttpGet("GetTaskForUserId/{id}")]
		public async Task<ActionResult<IEnumerable<GetTaskDTO>>> GetTaskForUserId([FromRoute] string id)
		{
			var tasks = await _taskService.GetTasksForUserIdAsync(id);
			return Ok(tasks);
		}
	}
}
