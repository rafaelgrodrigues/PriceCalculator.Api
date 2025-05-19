using AutoMapper;
using PriceCalculator.Application.Dtos;
using PriceCalculator.Domain.Entities;

namespace PriceCalculator.Application.Profiles;

public class PriceProfile : Profile
{
    public PriceProfile()
    {
        CreateMap<Vat, VatDto>().ReverseMap();
        CreateMap<Price, PriceDto>().ReverseMap();
    }
}
