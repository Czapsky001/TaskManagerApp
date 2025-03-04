using Microsoft.AspNetCore.Identity;

namespace TaskManagerApp.Model
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; } = string.Empty;
        public string SurName { get; set; } = string.Empty;

    }
}
