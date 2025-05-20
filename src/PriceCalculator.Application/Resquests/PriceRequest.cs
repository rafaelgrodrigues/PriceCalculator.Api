using System.ComponentModel.DataAnnotations;

namespace PriceCalculator.Application.Resquests;

public class PriceRequest
{
    [RegularExpression("^[0-9]*\\.?[0-9]+$", ErrorMessage = "Please enter valid Net Value")]
    public decimal? Net { get; set; }
    public decimal? Gross { get; set; }
    public decimal? VatValue { get; set; }

    [AllowedValues([10, 15, 20])]
    public int VatPercentage { get; set; }
    
}
