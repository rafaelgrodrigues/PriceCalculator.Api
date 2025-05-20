using AutoMapper;
using Microsoft.Extensions.Logging;
using Moq;
using PriceCalculator.Application.Interfaces;
using PriceCalculator.Application.Services;
using PriceCalculator.Domain.Calculation;

namespace PriceCalculator.Tests.UnitTests;

public class CalculationServiceTest
{
    private readonly ICalculationService _calculationService;
    private readonly Mock<IEnumerable<INetCalculator>> _calculatorsMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly Mock<ILogger<CalculationService>> _loggerMock;


    public CalculationServiceTest()
    {
        _calculatorsMock = new Mock<IEnumerable<INetCalculator>>();
        _mapperMock = new Mock<IMapper>();
        _loggerMock = new Mock<ILogger<CalculationService>>();

        _calculationService = new CalculationService(
            _calculatorsMock.Object, _mapperMock.Object, _loggerMock.Object);
    }

}
