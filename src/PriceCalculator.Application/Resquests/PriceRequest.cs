using System.ComponentModel.DataAnnotations;

namespace PriceCalculator.Application.Resquests;

public class PriceRequestDto
{
    public decimal? Net { get; set; }
    public decimal? Gross { get; set; }
    public decimal? VatValue { get; set; }
    public int VatPercentage { get; set; }
}

public class PriceRequest
{
    public string? Net { get; set; }
    public string? Gross { get; set; }
    public string? VatValue { get; set; }
    public string VatPercentage { get; set; }
}
