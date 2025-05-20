using AutoMapper;
using Microsoft.Extensions.Logging;
using PriceCalculator.Application.Dtos;
using PriceCalculator.Application.Interfaces;
using PriceCalculator.Application.Results;
using PriceCalculator.Domain.Calculation;
using PriceCalculator.Domain.Entities;
using PriceCalculator.Domain.Validators;

namespace PriceCalculator.Application.Services;

public class CalculationService(
    IEnumerable<INetCalculator> calculators,
    IMapper mapper,
    ILogger<CalculationService> logger,
    PriceValidator priceValidator) : ICalculationService
{
    private readonly IEnumerable<INetCalculator> _calculators = calculators;
    private readonly IMapper _mapper = mapper;
    private readonly ILogger<CalculationService> _logger = logger;
    private readonly PriceValidator _priceValidator = priceValidator;
    public async Task<Result<PriceDto>> Calculate(PriceRequestDto priceRequest)
    {
        try
        {
            if (priceRequest.Net.HasValue)
            {
                var vat = Vat.Create(priceRequest.VatPercentage, priceRequest.Net.Value);
                var price = new Price(priceRequest.Net.Value, vat);

                var validation = _priceValidator.Validate(price);
                if (!validation.IsValid)
                    return Result<PriceDto>.Failure(validation.Errors.Select(e => new Error(e.PropertyName, e.ErrorMessage)));

                var priceDto = _mapper.Map<PriceDto>(price);

                return Result<PriceDto>.Success(priceDto);
            }

            priceRequest.Net = await CalculateNet(priceRequest);

            return await Calculate(priceRequest);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            throw;
        }
    }

    private async Task<decimal> CalculateNet(PriceRequestDto priceRequest)
    {
        var strategyType = priceRequest.Gross.HasValue ?
            NetCalculatorType.ByGross : NetCalculatorType.ByVat;

        var baseValue = strategyType == NetCalculatorType.ByGross ?
            priceRequest.Gross.Value : priceRequest.VatValue.Value;

        var calculator = _calculators.First(calculator => calculator.StrategyType.Equals(strategyType));

        return await calculator.Calculate(baseValue, priceRequest.VatPercentage);
    }
}
