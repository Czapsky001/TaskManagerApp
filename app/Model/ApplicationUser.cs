using Microsoft.AspNetCore.Identity;

namespace TaskManagerApp.Model
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; } = string.Empty;
        public string SurName { get; set; } = string.Empty;
        public int? CompanyId { get; set; }
        public Company? Company { get; set; }
        public ICollection<TaskModel> Tasks { get; set; } = new List<TaskModel>();
        public ICollection<SubTask> SubTasks { get; set; } = new List<SubTask>();

    }
}
