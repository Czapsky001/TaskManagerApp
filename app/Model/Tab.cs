namespace TaskManagerApp.Model;

public class Tab
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int WorkTableId { get; set; }
    public WorkTable WorkTable { get; set; }
    public ICollection<TaskModel> Tasks { get; set; } = new List<TaskModel>();
}