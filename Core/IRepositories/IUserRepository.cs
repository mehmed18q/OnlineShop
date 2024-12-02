using Core.Entities;

namespace Core.IRepositories
{
    public interface IUserRepository
    {
        Task<User?> GetUserByUserName(string userName);
        Task<bool> IsUserExist(string userName);
        Task<Guid> InsertAsync(User user);
    }
}
