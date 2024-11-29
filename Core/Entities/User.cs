namespace Core.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public string UserName { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string PasswordSalt { get; set; } = null!;
        public DateTime RegisterDate { get; set; }
        public DateTime? LastLoginDate { get; set; }
    }
}
