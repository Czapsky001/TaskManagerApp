using System.ComponentModel.DataAnnotations;
using TaskManagerApp.Model;
namespace TaskManagerApp.Model;

public class SubTask
{
    [Key]
    public int Id { get; set; }
    
    [Required]
    [StringLength(100)]
    public string Name { get; set; }

    public string Description { get; set; }
    public TaskStatus Status { get; set; } = TaskStatus.Pending;
    
    public int ParentTaskId { get; set; }
    public TaskModel ParentTask { get; set; }

    public string CreatedByUserId { get; set; }
    public ApplicationUser CreatedByUser { get; set; }

    // Constructor that includes user ID
    public SubTask(string name, string description, string createdByUserId)
    {
        Name = name;
        Description = description;
        CreatedByUserId = createdByUserId;
    }
}
