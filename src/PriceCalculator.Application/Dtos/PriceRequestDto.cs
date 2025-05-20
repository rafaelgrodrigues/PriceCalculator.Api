namespace PriceCalculator.Application.Dtos;

public class PriceRequestDto
{
    public decimal? Net { get; set; }
    public decimal? Gross { get; set; }
    public decimal? VatValue { get; set; }
    public int VatRate { get; set; }
}
