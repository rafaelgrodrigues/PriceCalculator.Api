using AutoMapper;
using PriceCalculator.Application.Dtos;
using PriceCalculator.Application.Interfaces;
using PriceCalculator.Application.Resquests;
using PriceCalculator.Application.Results;
using PriceCalculator.Domain.Calculation;
using PriceCalculator.Domain.Entities;

namespace PriceCalculator.Application.Services;

public class CalculationService(IEnumerable<INetCalculator> calculators, IMapper mapper) : ICalculationService
{
    private readonly IEnumerable<INetCalculator> _calculators = calculators;
    private readonly IMapper _mapper = mapper;
    public async Task<Result<PriceDto>> Calculate(PriceRequest priceRequest)
    {
        if (priceRequest.Net.HasValue)
        {
            var vat = Vat.Create(priceRequest.VatPercentage, priceRequest.Net.Value);
            var price = new Price(priceRequest.Net.Value, vat);

            var priceDto = _mapper.Map<PriceDto>(price);

            return Result<PriceDto>.Success(priceDto);
        }

        var strategyType = priceRequest.Gross.HasValue ?
            CalculatorStrategyType.ByGross : CalculatorStrategyType.ByVat;

        var baseValue = strategyType == CalculatorStrategyType.ByGross ?
            priceRequest.Gross.Value : priceRequest.VatValue.Value;

        var calculator = _calculators.First(calculator => calculator.StrategyType.Equals(strategyType));

        priceRequest.Net = await calculator.Calculate(baseValue, priceRequest.VatPercentage);

        return await Calculate(priceRequest);
    }
}
