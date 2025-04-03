using FluentValidation;
using TaskManagerApp.Model.Dto.SubTasks;

namespace TaskManagerApp.Model.Validator;

public class UpdateSubTaskDTOValidator : AbstractValidator<UpdateSubTaskDTO>
{
    public UpdateSubTaskDTOValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MaximumLength(100).WithMessage("Name must be at most 100 characters.");

        RuleFor(x => x.Description)
            .MaximumLength(500).WithMessage("Description must be at most 500 characters.");

        RuleFor(x => x.Priority)
            .InclusiveBetween(1, 5).WithMessage("Priority must be between 1 and 5.");

        RuleFor(x => x.Status)
            .Must(value => Enum.IsDefined(typeof(TaskStatus), value))
            .WithMessage("Invalid status value.");

/*        RuleFor(x => x.DueDate)
            .GreaterThan(DateTime.UtcNow).WithMessage("DueDate must be in the future.");*/
    }
}
