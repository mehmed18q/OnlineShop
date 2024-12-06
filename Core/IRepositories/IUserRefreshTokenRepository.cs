using Core.Entities.Security;

namespace Core.IRepositories
{
    public interface IUserRefreshTokenRepository
    {
        Task InsertAsync(UserRefreshToken userRefreshToken);

        Task Update(UserRefreshToken userRefreshToken);

        Task<UserRefreshToken?> GetCurrentUserRefreshToken(Guid userIds);
        Task<UserRefreshToken?> GetCurrentUserRefreshToken(string refreshToken);
    }
}
