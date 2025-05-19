namespace PriceCalculator.Domain.Calculation;

public class CalculatorByVatStratagy : INetCalculator
{
    public CalculatorStrategyType StrategyType => CalculatorStrategyType.ByVat;

    public decimal Calculate(decimal vatValue, int vatPercentage) 
        => (vatValue * 100) / vatPercentage;
}
