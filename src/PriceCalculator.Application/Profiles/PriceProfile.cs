using AutoMapper;
using PriceCalculator.Application.Dtos;
using PriceCalculator.Application.Resquests;
using PriceCalculator.Domain.Entities;

namespace PriceCalculator.Application.Profiles;

public class PriceProfile : Profile
{
    public PriceProfile()
    {
        CreateMap<Vat, VatDto>().ReverseMap();
        CreateMap<Price, PriceDto>().ReverseMap();
        CreateMap<PriceRequest, PriceRequestDto>()
            .ForMember(dest => dest.Net, opt => {
                opt.PreCondition(src => src.Net != null);
                opt.MapFrom(src => (decimal?)decimal.Parse(src.Net.Replace(',','.')));
                })
            .ForMember(dest => dest.Gross, opt => {
                opt.PreCondition(src => src.Gross != null);
                opt.MapFrom(src => (decimal?)decimal.Parse(src.Gross.Replace(',', '.')));
            })
            .ForMember(dest => dest.VatValue, opt => {
                opt.PreCondition(src => src.VatValue != null);
                opt.MapFrom(src => (decimal?)decimal.Parse(src.VatValue.Replace(',', '.')));
            })
            .ForMember(dest => dest.VatPercentage, opt => {
                opt.PreCondition(src => src.VatPercentage != null);
                opt.MapFrom(src => int.Parse(src.VatPercentage));
            });
    }
}
