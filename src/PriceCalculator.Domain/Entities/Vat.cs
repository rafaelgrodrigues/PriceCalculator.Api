namespace PriceCalculator.Domain.Entities;

public class Vat
{    
    public int Percentage { get; }
    public decimal Value { get; }
    
    private Vat(int percentage, decimal value)
    {
        Percentage = percentage;
        Value = Math.Round(value, 2, MidpointRounding.AwayFromZero);
    }

    public static Vat Create(int percentage, decimal net) 
        => new(percentage, ((decimal)percentage/100) * net);
}
