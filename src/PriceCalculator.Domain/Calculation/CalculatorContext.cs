namespace PriceCalculator.Domain.Calculation;

public class CalculatorContext(INetCalculator calculation)
{
    private INetCalculator Calculation = calculation;

    public void SetCalculation(INetCalculator calculation)
    {
        Calculation = calculation;
    }

    public decimal Calculate(decimal baseValue, int vatPercentage)
    {
        return Calculation.Calculate(baseValue, vatPercentage);
    }
}

