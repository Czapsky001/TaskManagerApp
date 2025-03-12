using System.ComponentModel.DataAnnotations;

namespace TaskManagerApp.Model;

public class TaskModel
{
    [Key]
    public int Id { get; set; }

    [Required]
    [StringLength(100)]
    public string Name { get; set; }

    public string Description { get; set; }
    public DateTime DueDate { get; set; }
    public int Priority { get; set; }
    public string CreatedByUserId { get; set; }
    public ApplicationUser CreatedByUser { get; set; }
    public TaskStatus TaskStatus { get; set; } = TaskStatus.Pending;

    public ICollection<SubTask> SubTasks { get; set; } = new List<SubTask>();
    public TaskModel(){}

    public TaskModel(string name, string description, DateTime dueDate, int priority)
    {
        Name = name;
        Description = description;
        DueDate = dueDate;
        Priority = priority;
    }
}
