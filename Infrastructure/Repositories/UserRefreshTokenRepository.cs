using Core;
using Core.Entities.Security;
using Core.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class UserRefreshTokenRepository(OnlineShopDbContext dbContext) : IUserRefreshTokenRepository
    {
        private readonly OnlineShopDbContext _dbContext = dbContext;

        public async Task<UserRefreshToken?> GetCurrentUserRefreshToken(Guid userId)
        {
            UserRefreshToken? userRefreshToken = await _dbContext.UserRefreshTokens.SingleOrDefaultAsync(userRefreshToken => userRefreshToken.UserId == userId && userRefreshToken.IsValid);
            return userRefreshToken;
        }

        public async Task<UserRefreshToken?> GetCurrentUserRefreshToken(string refreshToken)
        {
            UserRefreshToken? userRefreshToken = await _dbContext.UserRefreshTokens.SingleOrDefaultAsync(userRefreshToken => userRefreshToken.RefreshToken == refreshToken && userRefreshToken.IsValid);
            return userRefreshToken;
        }

        public async Task InsertAsync(UserRefreshToken userRefreshToken)
        {
            _ = await _dbContext.AddAsync(userRefreshToken);
        }

        public Task Update(UserRefreshToken userRefreshToken)
        {
            _ = _dbContext.Update(userRefreshToken);

            return Task.CompletedTask;
        }
    }
}
