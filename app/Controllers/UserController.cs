using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManagerApp.Model;
using TaskManagerApp.Model.Dto;
using TaskManagerApp.Model.Dto.User;
using TaskManagerApp.Services.Users;

namespace TaskManagerApp.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly ILogger<UserController> _logger;

    public UserController(ILogger<UserController> logger, IUserService userService)
    {
        _logger = logger;
        _userService = userService;
    }

    [HttpGet("GetUsersByCompanyId/{id}")]
    public async Task<ActionResult<IEnumerable<UserDTO>>> GetUsersByCompanyId(int id)
    {
        try
        {
            var result = await _userService.GetUsersByCompanyIdAsync(id);
            return Ok(result);
        }catch (Exception ex)
        {
            _logger.LogError(ex.Message, ex);
            return BadRequest();
        }
    }
    [HttpGet("GetUserById/{id}"), Authorize(Roles = "Admin")]
    public async Task<ActionResult<ApplicationUser>> GetUserById(string id)
    {
        try
        {
            return await _userService.GetUserByUserIdAsync(id);
        }catch (Exception ex)
        {
            _logger.LogError(ex.Message, ex);
            return BadRequest();
        }
    }

    [HttpDelete("DeleteUser/{id}")]
    public async Task<ActionResult<bool>> DeleteUser(string id)
    {
        try
        {
            return await _userService.DeleteUserAsync(id);
        }catch (Exception ex)
        {
            _logger.LogError(ex.Message, ex);
            return BadRequest();
        }
    }
    [HttpPut("UpdateUser/{id}")]
    public async Task<ActionResult<bool>> UpdateUser(string id, UpdateUserDTO updateUserDTO)
    {
        try
        {
            return await _userService.UpdateUserAsync(id, updateUserDTO);
        }catch (Exception ex)
        {
            _logger.LogError(ex.Message, ex);
            return BadRequest();
        }
    }
}
