using FluentValidation;
using MiniApp.DTOs;

namespace MiniApp.Validation;

public class CreateReservationRequestValidation:AbstractValidator<CreateReservationRequest>
{
    public CreateReservationRequestValidation()
    {
        RuleFor(x => x.CustomerName)
            .NotEmpty()
            .WithMessage("Customer name is required.");

        RuleFor(x => x.GuestCount)
            .GreaterThanOrEqualTo(1)
            .WithMessage("Guest count must be at least 1.");

        RuleFor(x => x.ReservationDate)
            .GreaterThan(DateTime.Now)
            .WithMessage("Reservation date must be in the future.");
    }
}
