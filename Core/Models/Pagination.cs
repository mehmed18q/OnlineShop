namespace Core.Models
{
    public record Pagination
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public int TotalCount { get; set; }
        public int PageCount => (int)Math.Ceiling((decimal)(TotalCount / PageSize));
        public int SkipSize => (PageNumber - 1) * PageSize;
    }
}
