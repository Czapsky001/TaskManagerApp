namespace TaskManagerApp.Model;

public class WorkTable
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int CompanyId { get; set; }
    public Company Company { get; set; }
    public ICollection<Tab> Tabs { get; set; } = new List<Tab>();
}
