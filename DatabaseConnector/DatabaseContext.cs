using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TaskManagerApp.Model;

public class DatabaseContext : IdentityDbContext<ApplicationUser, IdentityRole, string>
{
    public DbSet<TaskModel> Tasks { get; set; }
    public DbSet<SubTask> SubTasks { get; set; }

    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<TaskModel>()
            .HasKey(t => t.Id);

        modelBuilder.Entity<ApplicationUser>()
            .HasMany(u => u.Tasks)
            .WithOne(t => t.CreatedByUser)
            .HasForeignKey(t => t.CreatedByUserId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<ApplicationUser>()
            .HasMany(u => u.SubTasks)
            .WithOne(s => s.CreatedByUser)
            .HasForeignKey(s => s.CreatedByUserId)
            .OnDelete(DeleteBehavior.Restrict);
    }

}
