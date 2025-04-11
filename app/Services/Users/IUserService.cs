using TaskManagerApp.Model;
using TaskManagerApp.Model.Dto;
using TaskManagerApp.Model.Dto.User;

namespace TaskManagerApp.Services.Users;

public interface IUserService
{
    Task<IEnumerable<UserDTO>> GetUsersByCompanyIdAsync(int companyId);
    Task<ApplicationUser> GetUserByUserIdAsync(string id);
    Task<bool> DeleteUserAsync(string id);
    Task<bool> UpdateUserAsync(string id, UpdateUserDTO updateUserDTO);
}
