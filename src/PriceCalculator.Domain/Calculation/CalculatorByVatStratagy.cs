namespace PriceCalculator.Domain.Calculation;

public class CalculatorByVatStratagy : INetCalculator
{
    public decimal Calculate(decimal vatValue, int vatPercentage) 
        => (vatValue * 100) / vatPercentage;
}
