using TaskManagerApp.Model.Dto.Tasks;

namespace TaskManagerApp.Model.Dto.SubTasks;

public class GetSubTaskDTO
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime DueDate { get; set; }
    public int Priority { get; set; }
    public TaskStatus TaskStatus { get; set; }
    public ApplicationUser CreatedByUser { get; set; }
    public TaskModel ParentTask{ get; set; }
}
