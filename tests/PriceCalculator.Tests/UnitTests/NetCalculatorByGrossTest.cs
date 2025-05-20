using PriceCalculator.Domain.Calculation;

namespace PriceCalculator.Tests.UnitTests;

public class NetCalculatorByGrossTest
{
    private readonly NetCalculatorByGross netCalculatorByGross;
    public NetCalculatorByGrossTest()
    {
          netCalculatorByGross = new NetCalculatorByGross();
    }
    [Theory]
    [InlineData(110, 10, 100)]
    [InlineData(113, 13, 100)]
    [InlineData(120, 20, 100)]
    [InlineData(110.1, 10, 100.09)]
    [InlineData(146.9, 13, 130)]
    [InlineData(193.92, 20, 161.6)]
    public async Task Calculate_When_ValidInputs(decimal gross, int vatRate, decimal expectedResult)
    {
        var result = await netCalculatorByGross.Calculate(gross, vatRate);
        Assert.Equal(expectedResult, result, 2);
    }
}
