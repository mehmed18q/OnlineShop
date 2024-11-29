using Application.ProductCommandQuery.Command;
using Application.ProductCommandQuery.Query;
using AutoMapper;
using Core.Entities;

namespace Application
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            #region SaveProductCommand
            _ = CreateMap<SaveProductCommand, Product>();

            _ = CreateMap<Product, SaveProductCommandResponse>()
                .ForMember(destination => destination.ProductId, option => option.MapFrom(source => source.Id));
            #endregion

            #region GetProductQueryResponse
            _ = CreateMap<Product, GetProductQueryResponse>()
               .ForMember(destination => destination.PriceWithComma, option => option.MapFrom(source => source.Price.ToString("#,0")))
               .ForMember(destination => destination.Title, option => option.MapFrom(source => source.ProductName));
            #endregion
        }
    }
}
