using FluentValidation;
using TaskManagerApp.Model.Dto.User;

namespace TaskManagerApp.Model.Validator;

public class UpdateUserDTOValidator : AbstractValidator<UpdateUserDTO>
{
    public UpdateUserDTOValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required.")
            .MaximumLength(100)
            .WithMessage("Email must be at most 100 characters.");

        RuleFor(x => x.PhoneNumber)
            .NotEmpty().WithMessage("Phone number is required.")
            .Matches(@"^\+?[0-9]{9,15}$").WithMessage("Invalid phone number format.");

        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Name is required")
            .MaximumLength(100)
            .WithMessage("Name must be at most 100 characters.");

        RuleFor(x => x.SurName)
            .NotEmpty()
            .WithMessage("Surname is required")
            .MaximumLength(100)
            .WithMessage("Surname must be at most 100 characters");

    }
}
