namespace Core.Entities.Security
{
    public class Permission
    {
        public int Id { get; set; }
        public string Flag { get; set; } = null!;
        public string Title { get; set; } = null!;

        public virtual List<RolePermission> RolePermissions { get; set; } = [];
    }
}
