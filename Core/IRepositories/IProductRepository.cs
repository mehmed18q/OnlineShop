using Core.Entities;
using Core.Models;

namespace Core.IRepositories
{
    public interface IProductRepository
    {
        Task<Product?> GetAsync(int id);
        Task<List<Product>> GetAllAsync(string? term, Pagination pagination);
        Task<int> InsertAsync(Product product);
    }
}
