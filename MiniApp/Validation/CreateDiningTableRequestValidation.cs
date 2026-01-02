using FluentValidation;
using MiniApp.Data.Context;
using MiniApp.DTOs.DiningTableDto;

namespace MiniApp.Validation;

public class CreateDiningTableRequestValidation : AbstractValidator<CreateDiningTableRequest>
{
    public CreateDiningTableRequestValidation(AppDbContext _context)
    {
        RuleFor(x => x.DiningTableNumber)
            .NotEmpty()
            .WithMessage("Dining table number is required.");

        RuleFor(x => x.Capacity)
            .GreaterThanOrEqualTo(1)
            .WithMessage("Capacity must be at least 1.");

        RuleFor(x => x.RestaurantId)
                    .Must(id => _context.Restaurants.Any(r=>r.Id==id))
                    .WithMessage("Seçilən Restaurant mövcud deyil.");
    }

}
