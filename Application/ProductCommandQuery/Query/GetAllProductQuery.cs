using AutoMapper;
using Core.Entities;
using Core.IRepositories;
using MediatR;

namespace Application.ProductCommandQuery.Query
{
    public class GetAllProductQuery : IRequest<List<GetProductQueryResponse>>
    {
        public string? Term { get; set; }
    }

    public class GetAllProductQueryHandler(IProductRepository repository, IMapper mapper) : IRequestHandler<GetAllProductQuery, List<GetProductQueryResponse>>
    {
        private readonly IProductRepository _repository = repository;
        private readonly IMapper _mapper = mapper;
        public async Task<List<GetProductQueryResponse>> Handle(GetAllProductQuery request, CancellationToken cancellationToken)
        {
            List<Product> products = await _repository.GetAllAsync(request.Term);
            List<GetProductQueryResponse> result = _mapper.Map<List<GetProductQueryResponse>>(products);

            return result;
        }
    }
}
