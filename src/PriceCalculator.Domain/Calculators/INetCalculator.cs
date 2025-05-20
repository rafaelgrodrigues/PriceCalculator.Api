namespace PriceCalculator.Domain.Calculation;

public interface INetCalculator
{
    NetCalculatorType StrategyType { get; }
    Task<decimal> Calculate(decimal baseValue, int vatRate);
}
