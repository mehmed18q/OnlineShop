namespace Core.IRepositories
{
    public interface IUserRoleRepository
    {
        Task<List<int>> GetUserRoles(Guid userId);
    }
}
