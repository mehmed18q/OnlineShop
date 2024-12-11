using Core;
using Core.Entities;
using Core.IRepositories;
using Core.Models;
using Core.Utilities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class ProductRepository(OnlineShopDbContext dbContext) : IProductRepository
    {
        private readonly OnlineShopDbContext _dbContext = dbContext;

        public async Task<Product?> GetAsync(int id)
        {
            Product? product = await _dbContext.Products.FindAsync(id);
            return product;
        }

        public async Task<List<Product>> GetAllAsync(string? term, Pagination pagination)
        {
            List<Product> products = await _dbContext.Products.Paging(pagination).AsNoTracking().ToListAsync();

            if (!string.IsNullOrEmpty(term))
            {
                products = products.Where(product => product.ProductName.Contains(term, StringComparison.OrdinalIgnoreCase)
                || (product.Description != null && product.Description.Contains(term, StringComparison.OrdinalIgnoreCase))).ToList();
            }

            return products;
        }

        public async Task<int> InsertAsync(Product product)
        {
            _ = await _dbContext.AddAsync(product);

            return product.Id;
        }
    }
}
