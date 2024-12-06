using Application.Interfaces;
using Core.IRepositories;
using Infrastructure.Utilities;

namespace Application.Services
{
    public class PermissionService(IUserRoleRepository userRoleRepository, IRolePermissionRepository rolePermissionRepository, CacheUtility cacheUtility) : IPermissionService
    {
        private readonly IUserRoleRepository _userRoleRepository = userRoleRepository;
        private readonly IRolePermissionRepository _rolePermissionRepository = rolePermissionRepository;
        private readonly CacheUtility _cacheUtility = cacheUtility;

        public async Task<bool> CheckPermission(Guid userId, string permissionFlag)
        {
            string permissionCacheKey = userId.GetPermissionCacheKey();
            if (!_cacheUtility.TryGetValue(permissionCacheKey, out List<string>? permissions))
            {
                List<int> roles = await _userRoleRepository.GetUserRoles(userId);
                permissions = await _rolePermissionRepository.GetUserRolePermissions(roles);

                _cacheUtility.SetMemoryCache(permissionCacheKey, permissions, TimeSpan.FromMinutes(1));
            }

            return permissions?.Contains(permissionFlag) == true;
        }
    }
}
