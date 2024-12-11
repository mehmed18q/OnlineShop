using AutoMapper;
using Core.Entities;
using Core.IRepositories;
using Infrastructure;
using Infrastructure.Models;
using Infrastructure.Utilities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.ProductCommandQuery.Query
{
    public class GetProductQuery : IRequest<Response<GetProductQueryResponse>>
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

    public class GetProductQueryHandler(IProductRepository repository, IMapper mapper, FileUtility fileUtility, ILogger<GetProductQueryHandler> logger) : IRequestHandler<GetProductQuery, Response<GetProductQueryResponse>>
    {
        private readonly IProductRepository _repository = repository;
        private readonly IMapper _mapper = mapper;
        private readonly FileUtility _fileUtility = fileUtility;
        private readonly ILogger<GetProductQueryHandler> _logger = logger;

        public async Task<Response<GetProductQueryResponse>> Handle(GetProductQuery request, CancellationToken cancellationToken)
        {
            GetProductQueryResponse? response = null;
            try
            {
                Product? product = await _repository.GetAsync(request.Id);
                response = _mapper.Map<GetProductQueryResponse>(product);
                response.ThumbnailBase64 = _fileUtility.ConvertToBase64(product?.Thumbnail);
                response.ThumbnailUrl = _fileUtility.GetFileUrl(product?.ThumbnailFileName, nameof(Product));
                return Response.Result(response, ResponseMessage.Success);
            }
            catch (Exception e)
            {
                string message = $"In {nameof(GetProductQueryHandler)}: Error Message: {e.Message}. Exception: {e.InnerException}";
                _logger.LogError(message);
                return Response.Result(response, message, System.Net.HttpStatusCode.InternalServerError);
            }
        }
    }
}
