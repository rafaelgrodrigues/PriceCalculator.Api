namespace PriceCalculator.Application.Dtos;

public class PriceDto
{
    public decimal Gross { get; set; }
    public decimal Net { get; set; }
    public VatDto VAT { get; set; }
}
