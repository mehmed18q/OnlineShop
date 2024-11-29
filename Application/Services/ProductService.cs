using Application.Interfaces;
using Core;
using Core.Entities;
using Infrastructure.Dto;
using Microsoft.EntityFrameworkCore;

namespace Application.Services
{
    public class ProductService(OnlineShopDbContext dbContext) : IProductService
    {
        private readonly OnlineShopDbContext _dbContext = dbContext;

        public async Task<ProductDto> Add(ProductDto model)
        {
            Product product = new()
            {
                ProductName = model.ProductName,
                Price = model.Price,
            };
            _ = await _dbContext.AddAsync(product);
            _ = await _dbContext.SaveChangesAsync();

            model.Id = product.Id;
            return model;
        }

        public async Task<ProductDto> Get(int id)
        {
            Product? product = await _dbContext.Products.FindAsync(id);
            ProductDto model = new()
            {
                Id = product.Id,
                Price = product.Price,
                ProductName = product.ProductName
            };

            return model;
        }

        public async Task<List<ProductDto>> GetAll()
        {
            List<ProductDto> products = await _dbContext.Products.Select(product => new ProductDto
            {
                Id = product.Id,
                Price = product.Price,
                ProductName = product.ProductName
            }).ToListAsync();

            return products;
        }
    }
}
