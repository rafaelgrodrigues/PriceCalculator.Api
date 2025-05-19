namespace PriceCalculator.Application.Dtos;

public class PriceDto
{
    public required decimal Gross { get; set; }
    public required decimal Net { get; set; }
    public required VatDto VAT { get; set; }
}
