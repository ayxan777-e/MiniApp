using FluentValidation;
using MiniApp.DTOs;

namespace MiniApp.Validation;

public class CreateRestaurantRequestValidation: AbstractValidator<CreateRestaurantRequest>
{
    public CreateRestaurantRequestValidation()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Restaurant name is required.")
            .MaximumLength(100)
            .WithMessage("Restaurant name cannot exceed 100 characters.");

        RuleFor(x => x.City)
            .NotEmpty()
            .WithMessage("City is required.");
    }
}
