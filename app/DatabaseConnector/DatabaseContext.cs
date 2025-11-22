using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TaskManagerApp.Model;

public class DatabaseContext : IdentityDbContext<ApplicationUser, IdentityRole, string>
{
    public DbSet<TaskModel> Tasks { get; set; }
    public DbSet<SubTask> SubTasks { get; set; }
    public DbSet<Company> Companies { get; set; }
    public DbSet<WorkTable> WorkTables { get; set; }
    public DbSet<Tab> Tabs { get; set; }

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

        modelBuilder.Entity<SubTask>()
            .HasOne(st => st.ParentTask)
            .WithMany(t => t.SubTasks)
            .HasForeignKey(st => st.ParentTaskId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<SubTask>()
            .HasOne(st => st.CreatedByUser)
            .WithMany(st => st.SubTasks)
            .HasForeignKey(k => k.CreatedByUserId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Company>()
            .HasMany(c => c.WorkTables)
            .WithOne(w => w.Company)
            .HasForeignKey(w => w.CompanyId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<WorkTable>()
            .HasMany(w => w.Tabs)
            .WithOne(t => t.WorkTable)
            .HasForeignKey(t => t.WorkTableId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Tab>()
            .HasMany(t => t.Tasks)
            .WithOne(task => task.Tab)
            .HasForeignKey(task => task.TabId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<TaskModel>()
            .HasOne(t => t.Tab)
            .WithMany(task => task.Tasks)
            .HasForeignKey(task => task.TabId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<ApplicationUser>()
            .HasOne(u => u.Company)
            .WithMany(c => c.Employees)
            .HasForeignKey(u => u.CompanyId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}

