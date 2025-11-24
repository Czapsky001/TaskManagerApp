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
            return await _userManager.FindByIdAsync(id);
    }

    public async Task<IEnumerable<UserDTO>> GetUsersByCompanyIdAsync(int companyId)
    {
            var result = await _userManager.Users.Where(u => u.CompanyId == companyId).ToListAsync();
            return _mapper.Map<IEnumerable<UserDTO>>(result);
    }

    public async Task<bool> UpdateUserAsync(string id, UpdateUserDTO updateUserDTO)
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
    }
    public async Task<bool> DeleteUserAsync(string id)
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
    }
}
