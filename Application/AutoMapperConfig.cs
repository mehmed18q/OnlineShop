using Application.AuthenticateCommandQuery.Command;
using Application.AuthenticateCommandQuery.Notification;
using Application.ProductCommandQuery.Command;
using Application.ProductCommandQuery.Query;
using AutoMapper;
using Core.Entities;
using Core.Entities.Security;

namespace Application
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {

            #region Product
            #region SaveProductCommand
            _ = CreateMap<SaveProductCommand, Product>()
                .ForMember(destination => destination.Thumbnail, option => option.Ignore())
                .ForMember(destination => destination.ThumbnailFileName, option => option.Ignore())
                .ForMember(destination => destination.ThumbnailFileExtension, option => option.Ignore())
                .ForMember(destination => destination.ThumbnailFileSize, option => option.Ignore());

            _ = CreateMap<Product, SaveProductCommandResponse>()
                .ForMember(destination => destination.ProductId, option => option.MapFrom(source => source.Id));
            #endregion

            #region GetProductQueryResponse
            _ = CreateMap<Product, GetProductQueryResponse>()
               .ForMember(destination => destination.PriceWithComma, option => option.MapFrom(source => source.Price.ToString("#,0")))
               .ForMember(destination => destination.Title, option => option.MapFrom(source => source.ProductName))
               .ForMember(destination => destination.ThumbnailUrl, option => option.MapFrom(source => source.ThumbnailFileName));
            #endregion
            #endregion

            #region User
            _ = CreateMap<RegisterCommand, User>()
                .ForMember(destination => destination.RegisterDate, option => option.MapFrom(_ => DateTime.Now));

            _ = CreateMap<User, LoginCommandResponse>();

            _ = CreateMap<AddRefreshTokenNotification, UserRefreshToken>()
                .ForMember(destination => destination.IsValid, option => option.MapFrom(_ => true))
                .ForMember(destination => destination.CreateDate, option => option.MapFrom(_ => DateTime.Now));
            #endregion
        }
    }
}
