using AutoMapper;
using Microsoft.Extensions.Logging;
using Moq;
using PriceCalculator.Application.Dtos;
using PriceCalculator.Application.Interfaces;
using PriceCalculator.Application.Results;
using PriceCalculator.Application.Services;
using PriceCalculator.Domain.Calculation;
using PriceCalculator.Domain.Entities;

namespace PriceCalculator.Tests.UnitTests;

public class CalculationServiceTest
{
    private readonly ICalculationService _calculationService;
    private readonly List<INetCalculator> _calculators;
    private readonly Mock<INetCalculator> _calculatorMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly Mock<ILogger<CalculationService>> _loggerMock;


    public CalculationServiceTest()
    {
        _calculators = new List<INetCalculator>();
        _calculatorMock = new Mock<INetCalculator>();
        _mapperMock = new Mock<IMapper>();
        _loggerMock = new Mock<ILogger<CalculationService>>();

        _calculators.Add(_calculatorMock.Object);

        _calculationService = new CalculationService(
            _calculators, _mapperMock.Object, _loggerMock.Object);
    }

    //Test success - Net
    [Fact]
    public async void Calculate_When_ReceiveNet_Success()
    {
        var dto = new PriceRequestDto() { Net = 100, VatRate = 10 };
        var expectedData = new PriceDto()
        {
            Gross = 110,
            Net = 100,
            VAT = new VatDto() { Rate = 10, Value = 10 }
        };
        _mapperMock.Setup(mapper => mapper.Map<PriceDto>(It.IsAny<Price>()))
            .Returns(expectedData);

        _calculatorMock.Setup(calculator => calculator.Calculate(It.IsAny<decimal>(), It.IsAny<int>()))
            .ReturnsAsync(100);

        var expectedResult = Result<PriceDto>.Success(expectedData);

        var result = await _calculationService.Calculate(dto);

        Assert.Equal(expectedResult.IsSuccess, result.IsSuccess);
        Assert.Equal(expectedResult?.Data?.Gross, result?.Data?.Gross);
        Assert.Equal(expectedResult?.Data?.Net, result?.Data?.Net);
        Assert.Equal(expectedResult?.Data?.VAT.Value, result?.Data?.VAT.Value);
        Assert.Equal(expectedResult?.Data?.VAT.Rate, result?.Data?.VAT.Rate);
    }


    //Test success - Gross
    [Fact]
    public async void Calculate_When_ReceiveGross_Success()
    {
        var dto = new PriceRequestDto() { Gross = 110, VatRate = 10 };
        var expectedData = new PriceDto()
        {
            Gross = 110,
            Net = 100,
            VAT = new VatDto() { Rate = 10, Value = 10 }
        };
        _mapperMock.Setup(mapper => mapper.Map<PriceDto>(It.IsAny<Price>()))
            .Returns(expectedData);

        _calculatorMock.SetupGet(calculator => calculator.StrategyType)
            .Returns(NetCalculatorType.ByGross);

        _calculatorMock.Setup(calculator => calculator.Calculate(It.IsAny<decimal>(), It.IsAny<int>()))
            .ReturnsAsync(100);

        var expectedResult = Result<PriceDto>.Success(expectedData);

        var result = await _calculationService.Calculate(dto);

        Assert.Equal(expectedResult.IsSuccess, result.IsSuccess);
        Assert.Equal(expectedResult?.Data?.Gross, result?.Data?.Gross);
        Assert.Equal(expectedResult?.Data?.Net, result?.Data?.Net);
        Assert.Equal(expectedResult?.Data?.VAT.Value, result?.Data?.VAT.Value);
        Assert.Equal(expectedResult?.Data?.VAT.Rate, result?.Data?.VAT.Rate);
    }
    //Test success - Vat
    [Fact]
    public async void Calculate_When_ReceiveVat_Success()
    {
        var dto = new PriceRequestDto() { VatValue = 10, VatRate = 10 };
        var expectedData = new PriceDto()
        {
            Gross = 110,
            Net = 100,
            VAT = new VatDto() { Rate = 10, Value = 10 }
        };
        _mapperMock.Setup(mapper => mapper.Map<PriceDto>(It.IsAny<Price>()))
            .Returns(expectedData);

        _calculatorMock.SetupGet(calculator => calculator.StrategyType)
            .Returns(NetCalculatorType.ByVat);

        _calculatorMock.Setup(calculator => calculator.Calculate(It.IsAny<decimal>(), It.IsAny<int>()))
            .ReturnsAsync(100);

        var expectedResult = Result<PriceDto>.Success(expectedData);

        var result = await _calculationService.Calculate(dto);

        Assert.Equal(expectedResult.IsSuccess, result.IsSuccess);
        Assert.Equal(expectedResult?.Data?.Gross, result?.Data?.Gross);
        Assert.Equal(expectedResult?.Data?.Net, result?.Data?.Net);
        Assert.Equal(expectedResult?.Data?.VAT.Value, result?.Data?.VAT.Value);
        Assert.Equal(expectedResult?.Data?.VAT.Rate, result?.Data?.VAT.Rate);
    }

    //Test Failure
    [Fact]
    public async void Calculate_When_ReceiveVat_Failure()
    {
        var dto = new PriceRequestDto() { VatValue = 10};
        var expectedData = new PriceDto()
        {
            Gross = 110,
            Net = 100,
            VAT = new VatDto() { Rate = 10, Value = 10 }
        };
        _mapperMock.Setup(mapper => mapper.Map<PriceDto>(It.IsAny<Price>()))
            .Returns(expectedData);

        _calculatorMock.SetupGet(calculator => calculator.StrategyType)
            .Returns(NetCalculatorType.ByVat);

        _calculatorMock.Setup(calculator => calculator.Calculate(It.IsAny<decimal>(), It.IsAny<int>()))
            .ReturnsAsync(100);

        var errosList = new List<Error>
        {
            new("VAT.Value", ""),
        };
        var expectedResult = Result<PriceDto>.Failure(errosList);

        var result = await _calculationService.Calculate(dto);

        Assert.Equal(expectedResult.IsFailure, result.IsFailure);
        Assert.Contains(expectedResult.Errors, e => e.Property.Equals("VAT.Value"));
    }
}
