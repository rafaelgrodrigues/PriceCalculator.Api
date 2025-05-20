using FluentValidation;
using PriceCalculator.Domain.Entities;

namespace PriceCalculator.Domain.Validators;

public class VatValidator : AbstractValidator<Vat>
{
    public VatValidator()
    {
        RuleFor(vat => vat.Value)
            .NotEmpty()
            .GreaterThan(0)
            .WithMessage("Must be greater than zero.");

        RuleFor(vat => vat.Percentage)
            .Must(percentage => percentage.Equals(10) || percentage.Equals(13) || percentage.Equals(20))
            .WithMessage("Must be 10, 13 or 20");
    }
}
