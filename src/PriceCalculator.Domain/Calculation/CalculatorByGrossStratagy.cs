namespace PriceCalculator.Domain.Calculation;

public class CalculatorByGrossStratagy : INetCalculator
{
    public CalculatorStrategyType StrategyType => CalculatorStrategyType.ByGross;

    public decimal Calculate(decimal gross, int vatPercentage) 
        => (gross * 100) / (100 + vatPercentage);
}
