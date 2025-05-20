
using PriceCalculator.Application.Resquests;

namespace PriceCalculator.Api.EndpointFilters
{
    public class PostPriceFilter : IEndpointFilter
    {
        public async ValueTask<object?> InvokeAsync(
            EndpointFilterInvocationContext context, 
            EndpointFilterDelegate next)
        {
            var result = next(context);
            var body = context.GetArgument<PriceRequest>(0);
            if (body == null)
                return TypedResults.UnprocessableEntity();

            return await next.Invoke(context);
        }
    }
}
