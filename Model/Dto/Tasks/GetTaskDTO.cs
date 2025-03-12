namespace TaskManagerApp.Model.Dto.Tasks;

public class GetTaskDTO
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime DueDate { get; set; }
    public int Priority { get; set; }
    public TaskStatus TaskStatus { get; set; }
    public UserDTO CreatedByUser { get; set; }
    public List<SubTaskDTO> SubTasks { get; set; }
}
