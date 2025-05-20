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

        RuleFor(vat => vat.Rate)
            .Must(rate => rate.Equals(10) || rate.Equals(13) || rate.Equals(20))
            .WithMessage("Must be 10, 13 or 20");
    }
}
