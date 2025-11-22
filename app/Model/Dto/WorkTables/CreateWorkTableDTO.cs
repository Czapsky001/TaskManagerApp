using System.ComponentModel.DataAnnotations;

namespace TaskManagerApp.Model.Dto.WorkTables;

public class CreateWorkTableDTO
{
    [Required]
    public string CreatedByUserId { get; set; }
    [Required]
    public string Name { get; set; }

    public int CompanyId { get; set; }
}
