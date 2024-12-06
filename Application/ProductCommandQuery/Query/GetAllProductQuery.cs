using AutoMapper;
using Core.Entities;
using Core.IRepositories;
using Infrastructure.Utilities;
using MediatR;

namespace Application.ProductCommandQuery.Query
{
    public class GetAllProductQuery : IRequest<List<GetProductQueryResponse>>
    {
        public string? Term { get; set; }
    }

    public class GetAllProductQueryHandler(IProductRepository repository, IMapper mapper, FileUtility fileUtility) : IRequestHandler<GetAllProductQuery, List<GetProductQueryResponse>>
    {
        private readonly IProductRepository _repository = repository;
        private readonly IMapper _mapper = mapper;
        private readonly FileUtility _fileUtility = fileUtility;

        public async Task<List<GetProductQueryResponse>> Handle(GetAllProductQuery request, CancellationToken cancellationToken)
        {
            List<Product> products = await _repository.GetAllAsync(request.Term);
            List<GetProductQueryResponse> result = _mapper.Map<List<GetProductQueryResponse>>(products);
            foreach (GetProductQueryResponse item in result)
            {
                //item.ThumbnailBase64 = _fileUtility.ConvertToBase64(product?.Thumbnail);
                item.ThumbnailUrl = _fileUtility.GetFileUrl(item?.ThumbnailUrl, nameof(Product));
            }
            return result;
        }
    }
}
