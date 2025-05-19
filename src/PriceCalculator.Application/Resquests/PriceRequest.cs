namespace PriceCalculator.Application.Resquests;

public class PriceRequest
{
    public decimal? Net { get; set; }
    public decimal? Gross { get; set; }
    public decimal? VatValue { get; set; }
    public int VatPercentage { get; set; }
    
}
