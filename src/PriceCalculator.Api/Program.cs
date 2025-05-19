using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using PriceCalculator.Api.Extentions;
using PriceCalculator.Application.Dtos;
using PriceCalculator.Application.Interfaces;
using PriceCalculator.Application.Profiles;
using PriceCalculator.Application.Resquests;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddDependecyInjection();

builder.Services.AddAutoMapper(
    AppDomain.CurrentDomain
    .GetAssemblies()
    .First(assembly => assembly.GetName().Name.Equals("PriceCalculator.Application")));

builder.Services.AddSwaggerGen();

var app = builder.Build();

if (!app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapPost("/Prices", async Task<Results<Created<PriceDto>, BadRequest>> (
    [FromBody] PriceRequest request,
    ICalculationService service
    ) =>
{
    var result = await service.Calculate(request);

    if (result.IsSuccess)
        return TypedResults.Created(string.Empty, result.Data);

    return TypedResults.BadRequest();
});

app.Run();