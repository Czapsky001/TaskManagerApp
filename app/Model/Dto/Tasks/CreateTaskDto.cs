using System.ComponentModel.DataAnnotations;

namespace TaskManagerApp.Model.Dto;

public class CreateTaskDTO
{
    [Required]
    public string CreatedByUserId { get; set; }
    [Required]
    public int TabId { get; set; }
    [Required]
    public string Name { get; set; }
    public string Description { get; set; }

}
