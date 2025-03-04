using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TaskManagerApp.Model;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace TaskManagerApp.DatabaseConnector;

public class UserContext : IdentityDbContext<ApplicationUser, IdentityRole, string>
{

    public UserContext(DbContextOptions<UserContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
    }
}