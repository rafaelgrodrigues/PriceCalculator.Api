using FluentValidation;
using System.Text.RegularExpressions;

namespace PriceCalculator.Application.Resquests.Validators;

public class PriceRequestValidator : AbstractValidator<PriceRequest>
{
    private const string RegexDecimal = @"^[0-9]+[\.\,][0-9]{1,2}$";
    private const string GreaterThanErrorMEssage = "Must be a numeric value greater than zero.";
    private const string InvalidNumericErrorMEssage = "Must be a numeric value. Correct format is *#.##";

    public PriceRequestValidator()
    {
        RuleFor(request => request)
            .Must(request => request.Net != null || request.Gross != null || request.VatValue != null)
            .WithMessage("Inform one of values (Net/Gross/VatValue)");

        RuleFor(request => request)
            .Must(request => new[] { request.Net, request.Gross, request.VatValue }.Count(c => c != null) == 1)
            .WithMessage($"Inform only one of values (Net/Gross/VatValue)");

        RuleFor(request => request.Net)
           .Matches(RegexDecimal)
           .WithMessage(InvalidNumericErrorMEssage)
           .When(request => request.Net != null);

        RuleFor(request => request.Net)
            .Must(request => decimal.Parse(request) > 0)
            .WithMessage(GreaterThanErrorMEssage)
            .When(request => ValidateDecimalPattern(request.Net ?? string.Empty));

        RuleFor(request => request.Gross)
            .Matches(RegexDecimal)
            .When(request => request.Gross != null)
            .WithMessage(InvalidNumericErrorMEssage);

        RuleFor(request => request.Gross)
            .Must(request => decimal.Parse(request) > 0)
            .WithMessage(GreaterThanErrorMEssage)
            .When(request => ValidateDecimalPattern(request.Gross ?? string.Empty));

        RuleFor(request => request.VatValue)
            .Matches(RegexDecimal)
            .When(request => request.VatValue != null)
            .WithMessage(InvalidNumericErrorMEssage);

        RuleFor(request => request.VatValue)
            .Must(request => decimal.Parse(request) > 0)
            .WithMessage(GreaterThanErrorMEssage)
            .When(request => ValidateDecimalPattern(request.VatValue ?? string.Empty));

        RuleFor(request => request.VatPercentage)
            .Must(percentage => percentage.Equals("10") || percentage.Equals("13") || percentage.Equals("20"))
            .WithMessage("Inform a valid VatPercentage(10, 13 or 20)");
    }
    private bool ValidateDecimalPattern(string value) => Regex.IsMatch(value, RegexDecimal);
}
