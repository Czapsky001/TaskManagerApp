using AutoMapper;
using Microsoft.AspNetCore.Identity;
using TaskManagerApp.Model;
using TaskManagerApp.Model.Dto;
using Microsoft.EntityFrameworkCore;
using TaskManagerApp.Model.Dto.User;

namespace TaskManagerApp.Services.Users;

public class UserService : IUserService
{
    private readonly ILogger<UserService> _logger;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IMapper _mapper;

    public UserService(UserManager<ApplicationUser> userManager, ILogger<UserService> logger, IMapper mapper)
    {
        _logger = logger;
        _userManager = userManager;
        _mapper = mapper;
    }

    public async Task<ApplicationUser> GetUserByUserIdAsync(string id)
    {
        try
        {
            return await _userManager.FindByIdAsync(id);
        }catch (Exception ex)
        {
            _logger.LogError(ex.Message, ex);
            return null;
        }
    }

    public async Task<IEnumerable<UserDTO>> GetUsersByCompanyIdAsync(int companyId)
    {
        try
        {
            var result = await _userManager.Users.Where(u => u.CompanyId == companyId).ToListAsync();
            return _mapper.Map<IEnumerable<UserDTO>>(result);
        }catch (Exception ex)
        {
            _logger.LogError(ex.Message, ex);
            return new List<UserDTO>();
        }
    }

    public async Task<bool> UpdateUserAsync(string id, UpdateUserDTO updateUserDTO)
    {
        try
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                _logger.LogWarning($"User with id - {id} does not exist");
                return false;
            }
            var userToUpdate = _mapper.Map(updateUserDTO, user);
            var result = await _userManager.UpdateAsync(userToUpdate);
            if (result.Succeeded)
            {
                return true;
            }
            return false;
        }catch (Exception ex)
        {
            _logger.LogError(ex.Message, ex);
            return false;
        }
    }
    public async Task<bool> DeleteUserAsync(string id)
    {
        try
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                _logger.LogWarning($"User with id - {id} does not exist");
                return false;
            }
            var result = await _userManager.DeleteAsync(user);
            if (result.Succeeded)
            {
                return true;
            }
            return false;
        }catch (Exception ex)
        {
            _logger.LogError(ex.Message, ex);
            return false;
        }
    }
}
