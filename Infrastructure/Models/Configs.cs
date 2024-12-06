namespace Infrastructure.Models
{
    public class Configs
    {
        public string TokenKey { get; set; } = null!;
        public string FileEncryptionKey { get; set; } = null!;
        public int TokenTimeout { get; set; }
        public int RefreshTokenTimeout { get; set; }
    }
}
