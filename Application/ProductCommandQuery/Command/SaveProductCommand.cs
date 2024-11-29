using AutoMapper;
using Core.Entities;
using Core.IRepositories;
using Infrastructure.Interfaces;
using MediatR;

namespace Application.ProductCommandQuery.Command
{
    public class SaveProductCommand : IRequest<SaveProductCommandResponse>
    {
        public string ProductName { get; set; } = null!;
        public int CategoryId { get; set; }
        public long Price { get; set; }
        public string? Description { get; set; }
    }

    public class SaveProductCommandResponse
    {
        public int ProductId { get; set; }
    }

    public class SaveProductCommandHandler(IProductRepository repository, IMapper mapper, IUnitOfWork unitOfWork) : IRequestHandler<SaveProductCommand, SaveProductCommandResponse>
    {
        private readonly IProductRepository _repository = repository;
        private readonly IMapper _mapper = mapper;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<SaveProductCommandResponse> Handle(SaveProductCommand request, CancellationToken cancellationToken)
        {
            Product product = _mapper.Map<Product>(request);

            _ = await _repository.InsertAsync(product);
            _ = await _unitOfWork.SaveChangesAsync();

            SaveProductCommandResponse response = _mapper.Map<SaveProductCommandResponse>(product);

            return response;
        }
    }
}
