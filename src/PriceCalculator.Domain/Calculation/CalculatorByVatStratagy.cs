namespace PriceCalculator.Domain.Calculation;

public class CalculatorByVatStratagy : INetCalculator
{
    public CalculatorStrategyType StrategyType => CalculatorStrategyType.ByVat;

    public Task<decimal> Calculate(decimal vatValue, int vatPercentage) 
        => Task.FromResult((vatValue * 100) / vatPercentage);
}
