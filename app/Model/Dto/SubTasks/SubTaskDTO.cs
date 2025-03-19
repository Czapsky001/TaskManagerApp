namespace TaskManagerApp.Model.Dto.SubTasks;

public class SubTaskDTO
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int ParentTaskId { get; set; }
}
