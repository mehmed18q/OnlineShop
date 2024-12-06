using Core;
using Core.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class UserRoleRepository(OnlineShopDbContext dbContext) : IUserRoleRepository
    {
        private readonly OnlineShopDbContext _dbContext = dbContext;

        public async Task<List<int>> GetUserRoles(Guid userId)
        {
            List<int> roles = await _dbContext.UserRoles.Where(userRole => userRole.UserId == userId && userRole.Role.IsActive).Select(userRole => userRole.RoleId).ToListAsync();
            return roles;
        }
    }
}
