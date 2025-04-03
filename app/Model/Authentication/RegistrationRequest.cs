using System.ComponentModel.DataAnnotations;

namespace TaskManagerApp.Model.Authentication;

public record RegistrationRequest
(
    [Required] string Email,
    [Required] string Name,
    [Required] string SurName,
    [Required] string UserName,
    [Required] string Password,
    [Required] int CompanyId,
    [Required] string Role
);
