namespace TaskManagerApp.Model.Dto;

public class UpdateTaskDto
{
    public string Name { get; set; }
    public string Description { get; set; }
    public int Priority { get; set; }
    public TaskStatus TaskStatus { get; set; }
}
