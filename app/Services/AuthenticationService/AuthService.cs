using Microsoft.AspNetCore.Identity;
using TaskManagerApp.Model;
using TaskManagerApp.Model.Authentication;
using TaskManagerApp.Services.TokenService;

namespace TaskManagerApp.Services.AuthenticationService;

public class AuthService : IAuthService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly ITokenService _tokenService;

    public AuthService(UserManager<ApplicationUser> userManager, ITokenService tokenService, RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _tokenService = tokenService;
        _roleManager = roleManager;
    }
    public async Task<AuthResult> LoginAsync(string email, string password)
    {
        var managedUser = await _userManager.FindByEmailAsync(email);

        if (managedUser == null) return InvalidEmail(email);

        var isPasswordValid = await _userManager.CheckPasswordAsync(managedUser, password);
        if (!isPasswordValid) return InvalidPassword(email, managedUser.UserName);

        var roles = await _userManager.GetRolesAsync(managedUser);

        var accessToken = _tokenService.CreateToken(managedUser, roles);

        return new AuthResult(true, managedUser.Email, managedUser.UserName,accessToken);
    }
    public async Task<AuthResult> RegisterAsync(string email, string name, string surname, string username, string password, int companyId, string role)
    {
        var newUser = new ApplicationUser { UserName = username, Email = email, Name = name, SurName = surname, CompanyId = companyId};
        var result = await _userManager.CreateAsync(newUser, password);

        if (!result.Succeeded) return FailedRegistration(result, email, username);

        var roleExists = await _roleManager.RoleExistsAsync(role);
        if (!roleExists) return FailedRoleNotExists(role);

        await _userManager.AddToRoleAsync(newUser, role);

        return new AuthResult(true, email, username, "");
    }
    private static AuthResult FailedRegistration(IdentityResult result, string email, string username)
    {
        var authResult = new AuthResult(false, email, username, "");

        foreach (var error in result.Errors) authResult.ErrorMessages.Add(error.Code, error.Description);

        return authResult;
    }
    private static AuthResult FailedRoleNotExists(string roleName)
    {
        var result = new AuthResult(false, "", "", "");
        result.ErrorMessages.Add("Bad role Name", $"Role {roleName} not exists");
        return result;
    }
    private static AuthResult InvalidPassword(string email, string userName)
    {
        var result = new AuthResult(false, email, userName, "");
        result.ErrorMessages.Add("Bad credentials", "Invalid password or email");
        return result;
    }
    private static AuthResult InvalidEmail(string email)
    {
        var result = new AuthResult(false, email, "", "");
        result.ErrorMessages.Add("Bad credentials", "Invalid password or email");
        return result;
    }
}
