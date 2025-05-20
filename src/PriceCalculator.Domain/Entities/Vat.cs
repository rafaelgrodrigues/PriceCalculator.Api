namespace PriceCalculator.Domain.Entities;

public class Vat
{    
    public int Rate { get; }
    public decimal Value { get; }
    
    private Vat(int rate, decimal value)
    {
        Rate = rate;
        Value = Math.Round(value, 2, MidpointRounding.AwayFromZero);
    }

    public static Vat Create(int rate, decimal net) 
        => new(rate, ((decimal)rate/100) * net);
}
