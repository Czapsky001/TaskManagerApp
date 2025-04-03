using TaskManagerApp.Model.Authentication;

namespace TaskManagerApp.Services.AuthenticationService
{
    public interface IAuthService
    {
        Task<AuthResult> RegisterAsync(string email, string name, string surname, string username, string password, int companyId,string role);
        Task<AuthResult> LoginAsync(string email, string password);
    }
}
