using AutoMapper;
using Core.Entities;
using Core.IRepositories;
using Infrastructure.Utilities;
using MediatR;

namespace Application.ProductCommandQuery.Query
{
    public class GetProductQuery : IRequest<GetProductQueryResponse>
    {
        public int Id { get; set; }
    }

    public class GetProductQueryResponse
    {
        public int Id { get; set; }

        public string Title { get; set; } = null!;

        public string PriceWithComma { get; set; } = null!;
        public string? Description { get; set; }
        public string? ThumbnailBase64 { get; set; }
        public string? ThumbnailUrl { get; set; }
    }

    public class GetProductQueryHandler(IProductRepository repository, IMapper mapper, FileUtility fileUtility) : IRequestHandler<GetProductQuery, GetProductQueryResponse>
    {
        private readonly IProductRepository _repository = repository;
        private readonly IMapper _mapper = mapper;
        private readonly FileUtility _fileUtility = fileUtility;

        public async Task<GetProductQueryResponse> Handle(GetProductQuery request, CancellationToken cancellationToken)
        {
            Product? product = await _repository.GetAsync(request.Id);
            GetProductQueryResponse result = _mapper.Map<GetProductQueryResponse>(product);
            result.ThumbnailBase64 = _fileUtility.ConvertToBase64(product?.Thumbnail);
            result.ThumbnailUrl = _fileUtility.GetFileUrl(product?.ThumbnailFileName, nameof(Product));
            return result;
        }
    }
}
