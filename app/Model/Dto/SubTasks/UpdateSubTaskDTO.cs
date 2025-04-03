namespace TaskManagerApp.Model.Dto.SubTasks;

public class UpdateSubTaskDTO
{
    public string Name { get; set; }
    public string Description { get; set; }
    public int Priority { get; set; }
    public TaskStatus Status { get; set; }
    public string CreatedByUserId { get; set; }
    public DateTime DueDate { get; set; }
    public int ParentTaskId { get; set; }
}
