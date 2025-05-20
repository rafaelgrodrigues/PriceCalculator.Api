using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using PriceCalculator.Api.EndpointFilters;
using PriceCalculator.Api.Extentions;
using PriceCalculator.Application.Dtos;
using PriceCalculator.Application.Interfaces;
using PriceCalculator.Application.Profiles;
using PriceCalculator.Application.Resquests;
using PriceCalculator.Application.Results;
using System.Runtime.CompilerServices;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddDependecyInjection();

builder.Services.AddAutoMapper(
    AppDomain.CurrentDomain
    .GetAssemblies()
    .First(assembly => assembly.GetName().Name.Equals("PriceCalculator.Application")));

builder.Services.AddSwaggerGen();
builder.Services.AddProblemDetails();

var app = builder.Build();

if (!app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    app.UseExceptionHandler();
}

app.UseHttpsRedirection();

app.MapPost("/Prices", async Task<Results<Created<PriceDto>, InternalServerError>> (
    [FromBody] PriceRequest request,
    ICalculationService service
    ) =>
{
    var result = await service.Calculate(request);

    if (result.IsSuccess)
        return TypedResults.Created(string.Empty, result.Data);

    return TypedResults.InternalServerError();
}).AddEndpointFilter<PostPriceFilter>()
  .Produces(422, typeof(IEnumerable<Error>));

app.Run();