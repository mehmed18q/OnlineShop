using Core;
using Core.Entities;
using Core.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class UserRepository(OnlineShopDbContext dbContext) : IUserRepository
    {
        private readonly OnlineShopDbContext _dbContext = dbContext;

        public async Task<User?> GetUserByUserName(string userName)
        {
            User? user = await _dbContext.Users.SingleOrDefaultAsync(user => user.UserName == userName);
            return user;
        }

        public async Task<bool> IsUserExist(string userName)
        {
            bool isUserExist = await _dbContext.Users.AnyAsync(user => user.UserName == userName);
            return isUserExist;
        }

        public async Task<Guid> InsertAsync(User user)
        {
            _ = await _dbContext.AddAsync(user);

            return user.Id;
        }
    }
}
