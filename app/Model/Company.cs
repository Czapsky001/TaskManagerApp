namespace TaskManagerApp.Model;

public class Company
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public ICollection<ApplicationUser> Employees { get; set; }
    public ICollection<WorkTable> WorkTables { get; set; } = new List<WorkTable>();
}

