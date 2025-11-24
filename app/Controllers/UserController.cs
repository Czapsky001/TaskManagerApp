using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManagerApp.Model;
using TaskManagerApp.Model.Dto;
using TaskManagerApp.Model.Dto.User;
using TaskManagerApp.Services.Users;

namespace TaskManagerApp.Controllers
{
    /// <summary>
    /// Provides user management operations such as reading, updating, and deleting users.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ILogger<UserController> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserController"/> class.
        /// </summary>
        /// <param name="logger">Logger instance.</param>
        /// <param name="userService">Service used for user-related operations.</param>
        public UserController(ILogger<UserController> logger, IUserService userService)
        {
            _logger = logger;
            _userService = userService;
        }

        /// <summary>
        /// Gets all users belonging to a specific company.
        /// </summary>
        /// <param name="id">The identifier of the company.</param>
        /// <returns>A collection of users belonging to the specified company.</returns>
        [HttpGet("GetUsersByCompanyId/{id}")]
        public async Task<ActionResult<IEnumerable<UserDTO>>> GetUsersByCompanyId([FromRoute] int id)
        {
            var result = await _userService.GetUsersByCompanyIdAsync(id);
            return Ok(result);
        }

        /// <summary>
        /// Gets a user by their unique identifier.
        /// </summary>
        /// <param name="id">The user identifier.</param>
        /// <returns>A user object if found.</returns>
        /// <remarks>Only administrators can access this endpoint.</remarks>
        [HttpGet("GetUserById/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ApplicationUser>> GetUserById([FromRoute] string id)
        {
            return await _userService.GetUserByUserIdAsync(id);
        }

        /// <summary>
        /// Deletes a user by their identifier.
        /// </summary>
        /// <param name="id">The user identifier.</param>
        /// <returns><c>true</c> if the user was successfully deleted; otherwise <c>false</c>.</returns>
        [HttpDelete("DeleteUser/{id}")]
        public async Task<ActionResult<bool>> DeleteUser([FromRoute] string id)
        {
            return await _userService.DeleteUserAsync(id);
        }

        /// <summary>
        /// Updates an existing user.
        /// </summary>
        /// <param name="id">The identifier of the user to update.</param>
        /// <param name="updateUserDTO">New user data.</param>
        /// <returns><c>true</c> if the update was successful; otherwise <c>false</c>.</returns>
        [HttpPut("UpdateUser/{id}")]
        public async Task<ActionResult<bool>> UpdateUser(
            [FromRoute] string id,
            [FromBody] UpdateUserDTO updateUserDTO)
        {
            return await _userService.UpdateUserAsync(id, updateUserDTO);
        }
    }
}
