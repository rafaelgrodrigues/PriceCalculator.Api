namespace PriceCalculator.Domain.Calculation;

public class NetCalculatorByVat : INetCalculator
{
    public NetCalculatorType StrategyType => NetCalculatorType.ByVat;

    public Task<decimal> Calculate(decimal vatValue, int vatPercentage) 
        => Task.FromResult((vatValue * 100) / vatPercentage);
}
