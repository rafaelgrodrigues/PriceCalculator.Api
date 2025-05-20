namespace PriceCalculator.Domain.Calculation;

public class CalculatorByGrossStratagy : INetCalculator
{
    public CalculatorStrategyType StrategyType => CalculatorStrategyType.ByGross;

    public Task<decimal> Calculate(decimal gross, int vatPercentage)
        => Task.FromResult((gross * 100) / (100 + vatPercentage));
}
