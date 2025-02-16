using System.ComponentModel.DataAnnotations;

namespace TaskManagerApp.Model.Dto;

public class CreateTaskDto
{
    [Required]
    public string Name { get; set; }
    public string Description { get; set; }

}
