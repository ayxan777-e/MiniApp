using FluentValidation;
using MiniApp.DTOs;

namespace MiniApp.Validation;

public class CreateDiningTableRequestValidation:AbstractValidator<CreateDiningTableRequest>
{
    public CreateDiningTableRequestValidation()
    {
        RuleFor(x => x.DiningTableNumber)
            .NotEmpty()
            .WithMessage("Dining table number is required.");

        RuleFor(x => x.Capacity)
            .GreaterThanOrEqualTo(1)
            .WithMessage("Capacity must be at least 1.");
    }
}
