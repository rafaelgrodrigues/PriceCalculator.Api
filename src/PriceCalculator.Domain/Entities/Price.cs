using System.Drawing;

namespace PriceCalculator.Domain.Entities;

public class Price(decimal net, Vat vat)
{
    public decimal Gross => Net + VAT.Value;
    public decimal Net { get; set; } = Math.Round(net, 2, MidpointRounding.AwayFromZero);
    public Vat VAT { get; set; } = vat;
}
