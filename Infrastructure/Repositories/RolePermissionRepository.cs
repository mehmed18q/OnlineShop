using Core;
using Core.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class RolePermissionRepository(OnlineShopDbContext dbContext) : IRolePermissionRepository
    {
        private readonly OnlineShopDbContext _dbContext = dbContext;

        public async Task<List<string>> GetUserRolePermissions(List<int> roleIds)
        {
            List<string> permissions = await _dbContext.RolePermissions
                 .Where(rolePermission => roleIds.Contains(rolePermission.RoleId))
                 .Select(rolePermission => rolePermission.Permission.Flag).ToListAsync();

            return permissions;
        }
    }
}
