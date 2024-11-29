using Application.Interfaces;
using AutoMapper;
using Core;
using Core.Entities;
using Infrastructure.Dto;
using System.Data.Entity;

namespace Application.Services
{
    public class ProductService(OnlineShopDbContext dbContext, IMapper mapper) : IProductService
    {
        private readonly OnlineShopDbContext _dbContext = dbContext;

        private readonly IMapper _mapper = mapper;

        public async Task<ProductDto> Add(ProductDto model)
        {
            Product product = _mapper.Map<Product>(model);
            _ = await _dbContext.AddAsync(product);
            _ = await _dbContext.SaveChangesAsync();

            model.Id = product.Id;
            return model;
        }

        public async Task<ProductDto> Get(int id)
        {
            Product? product = await _dbContext.Products.FindAsync(id);
            ProductDto model = _mapper.Map<ProductDto>(product);

            return model;
        }

        public async Task<List<ProductDto>> GetAll()
        {
            List<Product> products = await _dbContext.Products.ToListAsync();

            List<ProductDto> result = mapper.Map<List<ProductDto>>(products);

            return result;
        }
    }
}
