using PriceCalculator.Application.Dtos;
using PriceCalculator.Application.Resquests;
using PriceCalculator.Application.Results;

namespace PriceCalculator.Application.Interfaces;

public interface ICalculationService
{
    Task<Result<PriceDto>> Calculate(PriceRequest priceRequest);
}
