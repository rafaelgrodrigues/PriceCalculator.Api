
using PriceCalculator.Application.Resquests;
using PriceCalculator.Application.Resquests.Validators;
using PriceCalculator.Application.Results;

namespace PriceCalculator.Api.EndpointFilters
{
    public class PostPriceFilter : IEndpointFilter
    {
        public async ValueTask<object?> InvokeAsync(
            EndpointFilterInvocationContext context, 
            EndpointFilterDelegate next)
        {
            var validator = new PriceRequestValidator();

            var body = context.GetArgument<PriceRequest>(0);
            var requestValidation = validator.Validate(body);
            if (!requestValidation.IsValid)
                return TypedResults.UnprocessableEntity(requestValidation.Errors.Select(a => new Error(a.PropertyName, a.ErrorMessage)));

            return await next.Invoke(context);
        }
    }
}
