using System.ComponentModel.DataAnnotations;

namespace TaskManagerApp.Model.Dto.SubTasks;

public class CreateSubTaskDTO
{
    [Required]
    public string CreatedByUserId { get; set; }
    [Required]
    public string Name { get; set; }
    public string Description { get; set; }
    public int ParentTaskId { get; set; }
}
