namespace Core.IRepositories
{
    public interface IRolePermissionRepository
    {
        Task<List<string>> GetUserRolePermissions(List<int> roleIds);
    }
}
