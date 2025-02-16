using Microsoft.EntityFrameworkCore;
using TaskManagerApp.Model;


namespace TaskManagerApp.DatabaseConnector;

public class DatabaseContext : DbContext
{
    public DbSet<TaskModel> Tasks{ get; set; }
    public DbSet<SubTask> SubTasks { get; set; }

    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<TaskModel>()
            .HasKey(t => t.Id);
    }

}