namespace Core.Entities.Security
{
    public class Role
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public bool IsActive { get; set; }

        public virtual List<RolePermission> RolePermissions { get; set; } = [];
        public virtual List<UserRole> UserRoles { get; set; } = [];
    }
}
