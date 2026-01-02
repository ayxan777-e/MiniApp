using FluentValidation;
using MiniApp.DTOs.ReservationDate;

namespace MiniApp.Validation;

public class UpdateReservationRequestValidation:AbstractValidator<UpdateReservationRequest>
{
    public UpdateReservationRequestValidation()
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

        RuleFor(x => x.DiningTableId)
            .GreaterThan(0)
            .WithMessage("DiningTableId must be a valid id.");
    }
}
