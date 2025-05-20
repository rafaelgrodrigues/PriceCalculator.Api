namespace PriceCalculator.Domain.Calculation;

public class NetCalculatorByGross : INetCalculator
{
    public NetCalculatorType StrategyType => NetCalculatorType.ByGross;

    public Task<decimal> Calculate(decimal gross, int vatPercentage)
        => Task.FromResult((gross * 100) / (100 + vatPercentage));
}
