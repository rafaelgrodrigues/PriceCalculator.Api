using PriceCalculator.Domain.Calculation;
using System.Threading.Tasks;

namespace PriceCalculator.Tests.UnitTests;

public class NetCalculatorByVatTest
{
    private readonly NetCalculatorByVat netCalculatorByVat;
    public NetCalculatorByVatTest()
    {
          netCalculatorByVat = new NetCalculatorByVat();
    }


    [Theory]
    [InlineData(10,10,100)]
    [InlineData(13,13,100)]
    [InlineData(20, 20, 100)]
    [InlineData(20.01, 20, 100)]
    [InlineData(20.10, 20, 100)]
    [InlineData(20.95, 10, 209.5)]
    public async Task Calculate_When_ValidInputs(decimal vatValues, int vatRate, decimal expectedResult)
    {
        var result = await netCalculatorByVat.Calculate(vatValues, vatRate);
        Assert.Equal(expectedResult, result, 0);
    }

}
