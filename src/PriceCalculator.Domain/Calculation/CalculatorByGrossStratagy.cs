namespace PriceCalculator.Domain.Calculation;

public class CalculatorByGrossStratagy : INetCalculator
{
    public decimal Calculate(decimal gross, int vatPercentage) 
        => (gross * 100) / (100 + vatPercentage);
}
