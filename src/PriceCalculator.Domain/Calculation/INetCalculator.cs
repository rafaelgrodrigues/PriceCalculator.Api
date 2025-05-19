namespace PriceCalculator.Domain.Calculation;

public interface INetCalculator
{
    CalculatorStrategyType StrategyType { get; }
    Task<decimal> Calculate(decimal baseValue, int vatPercentage);
}
