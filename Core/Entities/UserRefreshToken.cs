namespace Core.Entities
{
    public class UserRefreshToken
    {
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public string RefreshToken { get; set; } = string.Empty;
        public int RefreshTokenTimeout { get; set; }
        public DateTime CreateDate { get; set; }
        public bool IsValid { get; set; }

        public virtual required User User { get; set; }
    }
}
