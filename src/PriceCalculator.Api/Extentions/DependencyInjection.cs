using PriceCalculator.Application.Interfaces;
using PriceCalculator.Application.Services;
using PriceCalculator.Domain.Calculation;

namespace PriceCalculator.Api.Extentions
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDependecyInjection(this IServiceCollection services) 
        {
            return services
                .AddScoped<ICalculationService, CalculationService>()
                .AddScoped<INetCalculator, NetCalculatorByGross>()
                .AddScoped<INetCalculator, NetCalculatorByVat>();
        }
    }
}
