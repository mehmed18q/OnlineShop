using AutoMapper;
using Core.Entities;
using Core.IRepositories;
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
    }

    public class GetProductQueryHandler(IProductRepository repository, IMapper mapper) : IRequestHandler<GetProductQuery, GetProductQueryResponse>
    {
        private readonly IProductRepository _repository = repository;
        private readonly IMapper _mapper = mapper;
        public async Task<GetProductQueryResponse> Handle(GetProductQuery request, CancellationToken cancellationToken)
        {
            Product? product = await _repository.GetAsync(request.Id);
            GetProductQueryResponse result = _mapper.Map<GetProductQueryResponse>(product);

            return result;
        }
    }
}
