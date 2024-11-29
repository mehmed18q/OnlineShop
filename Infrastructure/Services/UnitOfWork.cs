using Core;
using Infrastructure.Interfaces;

namespace Infrastructure.Services
{
    public class UnitOfWork(OnlineShopDbContext dbContext) : IUnitOfWork
    {
        private readonly OnlineShopDbContext _dbContext = dbContext;

        public async Task<int> SaveChangesAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}
