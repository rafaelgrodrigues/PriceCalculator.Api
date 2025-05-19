using PriceCalculator.Application.Dtos;
using PriceCalculator.Application.Interfaces;
using PriceCalculator.Application.Resquests;
using PriceCalculator.Domain.Calculation;
using PriceCalculator.Domain.Entities;

namespace PriceCalculator.Application.Services;

public class CalculationService(IEnumerable<INetCalculator> calculators) : ICalculationService
{
    private readonly IEnumerable<INetCalculator> _calculators = calculators;

    public PriceDto Calculate(PriceRequest priceRequest)
    {
        if (priceRequest.Net.HasValue)
        {
            var vat = Vat.Create(priceRequest.VatPercentage, priceRequest.Net.Value);
            var price = new Price(priceRequest.Net.Value, vat);

            return new PriceDto();
        }

        var strategyType = priceRequest.Gross.HasValue ?
            CalculatorStrategyType.ByGross : CalculatorStrategyType.ByVat;

        var baseValue = strategyType == CalculatorStrategyType.ByGross ?
            priceRequest.Gross.Value : priceRequest.VatValue.Value;

        var calculator = _calculators.First(calculator => calculator.StrategyType.Equals(strategyType));
        
        priceRequest.Net = calculator.Calculate(baseValue, priceRequest.VatPercentage);

        return Calculate(priceRequest);
    }
}
