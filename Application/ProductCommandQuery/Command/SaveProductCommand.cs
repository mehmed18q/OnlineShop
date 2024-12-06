using AutoMapper;
using Core.Entities;
using Core.IRepositories;
using Infrastructure.Interfaces;
using Infrastructure.Utilities;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.ProductCommandQuery.Command
{
    public class SaveProductCommand : IRequest<SaveProductCommandResponse>
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

    public class SaveProductCommandHandler(IProductRepository repository, IMapper mapper, IUnitOfWork unitOfWork, FileUtility fileUtility) : IRequestHandler<SaveProductCommand, SaveProductCommandResponse>
    {
        private readonly IProductRepository _repository = repository;
        private readonly IMapper _mapper = mapper;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly FileUtility _fileUtility = fileUtility;

        public async Task<SaveProductCommandResponse> Handle(SaveProductCommand request, CancellationToken cancellationToken)
        {
            Product product = _mapper.Map<Product>(request);
            // Save File in Database
            (product.Thumbnail, product.ThumbnailFileName, product.ThumbnailFileExtension, product.ThumbnailFileSize) = FileUtility.GetProductThumbnailInfo(request.Thumbnail);
            // Upload File in Directory
            product.ThumbnailFileName = _fileUtility.UploadFile(request.Thumbnail, nameof(Product));

            _ = await _repository.InsertAsync(product);
            _ = await _unitOfWork.SaveChangesAsync();

            SaveProductCommandResponse response = _mapper.Map<SaveProductCommandResponse>(product);

            return response;
        }
    }
}
