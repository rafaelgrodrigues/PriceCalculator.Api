using FluentValidation;
using PriceCalculator.Domain.Entities;

namespace PriceCalculator.Domain.Validators;

public class PriceValidator : AbstractValidator<Price>
{
    private const string GreaterThanZeroErrorMessage = "Must be greater than zero.";
    public PriceValidator()
    {
        RuleFor(price => price.Net)
            .NotNull()
            .GreaterThan(0)
            .WithMessage(GreaterThanZeroErrorMessage);

        RuleFor(price => price.Gross)
            .NotNull()
            .GreaterThan(0)
            .WithMessage(GreaterThanZeroErrorMessage);

        RuleFor(price => price.VAT)
            .NotNull()
            .SetValidator(new VatValidator());
    }
}
