using FluentValidation;

namespace TaskManagerApp.Model.Validator;

public class EnumValidator<T> : AbstractValidator<T> where T : struct, Enum
{
    public EnumValidator()
    {
        RuleFor(x => x).Must(value => Enum.IsDefined(typeof(T), value)).WithMessage($"Invalid value for {typeof(T).Name}");
    }
}
