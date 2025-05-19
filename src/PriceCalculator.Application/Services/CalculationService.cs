using PriceCalculator.Application.Dtos;
using PriceCalculator.Application.Interfaces;
using PriceCalculator.Application.Resquests;
using PriceCalculator.Domain.Calculation;
using PriceCalculator.Domain.Entities;

namespace PriceCalculator.Application.Services;

public class CalculationService : ICalculationService
{
    private CalculatorContext CalculatorContext;
    public PriceDto Calculate(PriceRequest priceRequest)
    {
        if (priceRequest.Net.HasValue)
        {
            var vat = Vat.Create(priceRequest.VatPercentage, priceRequest.Net.Value);
            var price = new Price(priceRequest.Net.Value, vat);

            return new PriceDto();
        }

        var baseValue = priceRequest.Gross ?? priceRequest.VatValue;

        CalculatorContext = new CalculatorContext(GetStrategy(priceRequest));
        priceRequest.Net = CalculatorContext.Calculate(baseValue.Value, priceRequest.VatPercentage);

        return Calculate(priceRequest);
    }

    private static INetCalculator GetStrategy(PriceRequest price)
    {
        if (price.Gross.HasValue)
            return new CalculatorByGrossStratagy();

        return new CalculatorByVatStratagy();
    }
}
