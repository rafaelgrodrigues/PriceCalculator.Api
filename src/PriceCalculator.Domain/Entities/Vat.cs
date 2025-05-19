namespace PriceCalculator.Domain.Entities;

public class Vat
{    
    public int Percentage { get; }
    public decimal Value { get; }
    
    private Vat(int percentage, decimal value)
    {
        Percentage = percentage;
        Value = value;
    }

    public static Vat Create(int percentage, decimal net) => new(percentage, ((decimal)percentage/100) * net);
}
