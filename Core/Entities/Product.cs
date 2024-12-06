namespace Core.Entities
{
    public class Product
    {
        public int Id { get; set; }

        public string ProductName { get; set; } = null!;
        public string? Description { get; set; }

        public long Price { get; set; }
        public byte[]? Thumbnail { get; set; }
        public string? ThumbnailFileName { get; set; }
        public long? ThumbnailFileSize { get; set; }
        public string? ThumbnailFileExtension { get; set; }
    }
}
