using Application.Interfaces;
using Core.IRepositories;
using Infrastructure.Utilities;
using Microsoft.Extensions.Logging;

namespace Application.Services
{
    public class PermissionService(IUserRoleRepository userRoleRepository, IRolePermissionRepository rolePermissionRepository, CacheUtility cacheUtility, ILogger<PermissionService> logger) : IPermissionService
    {
        private readonly IUserRoleRepository _userRoleRepository = userRoleRepository;
        private readonly IRolePermissionRepository _rolePermissionRepository = rolePermissionRepository;
        private readonly CacheUtility _cacheUtility = cacheUtility;
        private readonly ILogger<PermissionService> _logger = logger;

        public async Task<bool> CheckPermission(Guid userId, string permissionFlag)
        {
            try
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
            catch (Exception e)
            {
                string message = $"In {nameof(PermissionService)}.{nameof(CheckPermission)}: Error Message: {e.Message}. Exception: {e.InnerException}";
                _logger.LogError(message);
                return false;
            }
        }
    }
}
