namespace PriceCalculator.Domain.Calculation;

public interface INetCalculator
{
    CalculatorStrategyType StrategyType { get; }
    decimal Calculate(decimal baseValue, int vatPercentage);
}
