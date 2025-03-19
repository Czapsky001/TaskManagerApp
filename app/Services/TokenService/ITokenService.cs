using TaskManagerApp.Model;

namespace TaskManagerApp.Services.TokenService;

public interface ITokenService
{
    public string CreateToken(ApplicationUser user, IEnumerable<string> roles);
}
