using AutoMapper;
using Core.Entities;
using Infrastructure.Dto;

namespace Application
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            _ = CreateMap<Product, ProductDto>()
                .ForMember(dest => dest.PriceWithComma, opt => opt.MapFrom(src => src.Price.ToString("#,0")))
                .ReverseMap();
        }
    }
}
