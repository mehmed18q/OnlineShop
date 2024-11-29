using Core.Entities;

namespace Core.IRepositories
{
    public interface IProductRepository
    {
        Task<Product?> GetAsync(int id);
        Task<List<Product>> GetAllAsync(string? Term);
        Task<int> InsertAsync(Product product);
    }
}
