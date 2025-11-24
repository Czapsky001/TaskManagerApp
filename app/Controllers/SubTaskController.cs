using Microsoft.AspNetCore.Mvc;
using TaskManagerApp.Model.Dto.SubTasks;
using TaskManagerApp.Services.SubTasks;

namespace TaskManagerApp.Controllers
{
    /// <summary>
    /// Provides operations for managing subtasks, including creation, update, deletion,
    /// and retrieval based on parent task or user.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class SubTaskController : ControllerBase
    {
        private readonly ILogger<SubTaskController> _logger;
        private readonly ISubTaskService _subTaskService;

        /// <summary>
        /// Initializes a new instance of the <see cref="SubTaskController"/> class.
        /// </summary>
        /// <param name="logger">Logger instance.</param>
        /// <param name="subTaskService">Service used for subtask operations.</param>
        public SubTaskController(ILogger<SubTaskController> logger, ISubTaskService subTaskService)
        {
            _logger = logger;
            _subTaskService = subTaskService;
        }

        /// <summary>
        /// Retrieves all subtasks for a given parent task.
        /// </summary>
        /// <param name="id">The identifier of the parent task.</param>
        /// <returns>A collection of subtasks associated with the specified parent task.</returns>
        [HttpGet("GetAllSubTasksForParentTaskId/{id}")]
        public ActionResult<IEnumerable<SubTaskDTO>> GetAllSubTasksForParentTaskId([FromRoute] int id)
        {
            var subTasks = _subTaskService.GetAllSubTasksForParentTaskId(id);
            return Ok(subTasks);
        }

        /// <summary>
        /// Creates a new subtask.
        /// </summary>
        /// <param name="createSubTaskDTO">The data used to create the subtask.</param>
        /// <returns><c>true</c> if the subtask was created successfully; otherwise <c>false</c>.</returns>
        [HttpPost("CreateSubTask")]
        public async Task<ActionResult<bool>> CreateSubTask([FromBody] CreateSubTaskDTO createSubTaskDTO)
        {
            await _subTaskService.AddSubTaskAsync(createSubTaskDTO);
            return Ok(true);
        }

        /// <summary>
        /// Retrieves all subtasks assigned to a specific user.
        /// </summary>
        /// <param name="id">The identifier of the user.</param>
        /// <returns>A collection of subtasks assigned to the specified user.</returns>
        [HttpGet("GetSubTasksForUserId/{id}")]
        public ActionResult<IEnumerable<SubTaskDTO>> GetSubTasksForUserId([FromRoute] string id)
        {
            var subTasks = _subTaskService.GetSubTasksForUserId(id);
            return Ok(subTasks);
        }

        /// <summary>
        /// Updates an existing subtask.
        /// </summary>
        /// <param name="id">The identifier of the subtask to update.</param>
        /// <param name="updateSubTaskDTO">The new data for the subtask.</param>
        /// <returns>
        /// <c>true</c> if the subtask was updated successfully; otherwise <c>false</c>.
        /// </returns>
        [HttpPut("UpdateSubTask/{id}")]
        public async Task<ActionResult<bool>> UpdateSubTask(
            [FromRoute] int id,
            [FromBody] UpdateSubTaskDTO updateSubTaskDTO)
        {
            var subTaskToUpdate = await _subTaskService.UpdateSubTaskAsync(id, updateSubTaskDTO);
            return Ok(subTaskToUpdate);
        }

        /// <summary>
        /// Deletes an existing subtask.
        /// </summary>
        /// <param name="id">The identifier of the subtask to delete.</param>
        /// <returns>
        /// <c>true</c> if the subtask was deleted successfully; otherwise <c>false</c>.
        /// </returns>
        [HttpDelete("DeleteSubTask/{id}")]
        public async Task<ActionResult<bool>> DeleteSubTask([FromRoute] int id)
        {
            var subTaskToDelete = await _subTaskService.DeleteSubTaskAsync(id);
            return Ok(subTaskToDelete);
        }
    }
}
