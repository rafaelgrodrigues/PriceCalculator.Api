namespace PriceCalculator.Application.Resquests;

public class PriceRequest
{
    public string? Net { get; set; }
    public string? Gross { get; set; }
    public string? VatValue { get; set; }
    public string VatRate { get; set; }
}
