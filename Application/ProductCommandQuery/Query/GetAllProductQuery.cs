using AutoMapper;
using Core.Entities;
using Core.IRepositories;
using Core.Models;
using Infrastructure;
using Infrastructure.Models;
using Infrastructure.Utilities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.ProductCommandQuery.Query
{
    public class GetAllProductQuery : IRequest<ListResponse<List<GetProductQueryResponse>>>
    {
        public string? Term { get; set; }
        public Pagination Pagination { get; set; } = new();
    }

    public class GetAllProductQueryHandler(IProductRepository repository, IMapper mapper, FileUtility fileUtility, ILogger<GetAllProductQueryHandler> logger) : IRequestHandler<GetAllProductQuery, ListResponse<List<GetProductQueryResponse>>>
    {
        private readonly IProductRepository _repository = repository;
        private readonly IMapper _mapper = mapper;
        private readonly FileUtility _fileUtility = fileUtility;
        private readonly ILogger<GetAllProductQueryHandler> _logger = logger;

        public async Task<ListResponse<List<GetProductQueryResponse>>> Handle(GetAllProductQuery request, CancellationToken cancellationToken)
        {
            List<GetProductQueryResponse>? response = null;
            try
            {
                List<Product> products = await _repository.GetAllAsync(request.Term, request.Pagination);
                response = _mapper.Map<List<GetProductQueryResponse>>(products);
                foreach (GetProductQueryResponse item in response)
                {
                    //item.ThumbnailBase64 = _fileUtility.ConvertToBase64(product?.Thumbnail);
                    item.ThumbnailUrl = _fileUtility.GetFileUrl(item?.ThumbnailUrl, nameof(Product));
                }
                return Response.ListResult(response, ResponseMessage.Success, request.Pagination);
            }
            catch (Exception e)
            {
                string message = $"In {nameof(GetAllProductQueryHandler)}: Error Message: {e.Message}. Exception: {e.InnerException}";
                _logger.LogError(message);
                return Response.ListResult(response, message, request.Pagination, System.Net.HttpStatusCode.InternalServerError);
            }
        }
    }
}
