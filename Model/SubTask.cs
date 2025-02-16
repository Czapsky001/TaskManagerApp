namespace TaskManagerApp.Model;

public class SubTask
{
    public int Id { get; set; }
    public string Name  { get; set; }
    public string Description { get; set; }
    public TaskStatus Status { get; set; } = TaskStatus.Pending;
    public int ParentTaskId { get; set; }
    public TaskModel ParentTask { get; set; }

    public SubTask(string name, string description)
    {
        Name = name;
        Description = description;
    }


}
