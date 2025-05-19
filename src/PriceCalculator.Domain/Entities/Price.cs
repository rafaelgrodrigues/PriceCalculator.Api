namespace PriceCalculator.Domain.Entities;

public class Price(decimal net, Vat vat)
{
    public decimal Gross => Net + VAT.Value;
    public decimal Net { get; set; } = net;
    public Vat VAT { get; set; } = vat;
}
