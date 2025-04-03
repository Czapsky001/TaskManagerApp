namespace TaskManagerApp.Model.Dto;
public class UpdateTaskDTO
{
    public string Name { get; set; }
    public string Description { get; set; }
    public int Priority { get; set; }
    public TaskStatus TaskStatus { get; set; }
    public string CreatedByUserId { get; set; }
    public int TabId { get; set; }
    public DateTime DueDate { get; set; }
}