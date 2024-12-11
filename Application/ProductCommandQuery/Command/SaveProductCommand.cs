using AutoMapper;
using Core.Entities;
using Core.IRepositories;
using Infrastructure;
using Infrastructure.Interfaces;
using Infrastructure.Models;
using Infrastructure.Utilities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Application.ProductCommandQuery.Command
{
    public class SaveProductCommand : IRequest<Response<SaveProductCommandResponse>>
    {
        public string ProductName { get; set; } = null!;
        public long Price { get; set; }
        public string? Description { get; set; }
        public IFormFile? Thumbnail { get; set; }
    }

    public class SaveProductCommandResponse
    {
        public int ProductId { get; set; }
    }

    public class SaveProductCommandHandler(IProductRepository repository, IMapper mapper, IUnitOfWork unitOfWork, FileUtility fileUtility, ILogger<SaveProductCommandHandler> logger) : IRequestHandler<SaveProductCommand, Response<SaveProductCommandResponse>>
    {
        private readonly IProductRepository _repository = repository;
        private readonly IMapper _mapper = mapper;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly FileUtility _fileUtility = fileUtility;
        private readonly ILogger<SaveProductCommandHandler> _logger = logger;

        public async Task<Response<SaveProductCommandResponse>> Handle(SaveProductCommand request, CancellationToken cancellationToken)
        {
            SaveProductCommandResponse? response = null;
            try
            {
                Product product = _mapper.Map<Product>(request);
                // Save File in Database
                //(product.Thumbnail, product.ThumbnailFileName, product.ThumbnailFileExtension, product.ThumbnailFileSize) = FileUtility.GetProductThumbnailInfo(request.Thumbnail);
                // Upload File in Directory
                product.ThumbnailFileName = _fileUtility.UploadFile(request.Thumbnail, nameof(Product));

                _ = await _repository.InsertAsync(product);
                _ = await _unitOfWork.SaveChangesAsync();

                response = _mapper.Map<SaveProductCommandResponse>(product);
                return Response.Result(response, ResponseMessage.Success);
            }
            catch (Exception e)
            {
                string message = $"In {nameof(SaveProductCommandHandler)}: Error Message: {e.Message}. Exception: {e.InnerException}";
                _logger.LogError(message);
                return Response.Result(response, message, System.Net.HttpStatusCode.InternalServerError);
            }
        }
    }
}
