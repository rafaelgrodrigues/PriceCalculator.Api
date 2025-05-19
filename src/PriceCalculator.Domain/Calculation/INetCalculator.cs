namespace PriceCalculator.Domain.Calculation;

public interface INetCalculator
{
    decimal Calculate(decimal baseValue, int vatPercentage);
}
