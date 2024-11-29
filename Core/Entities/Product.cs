namespace Core.Entities
{
    public class Product
    {
        public int Id { get; set; }

        public string ProductName { get; set; } = null!;
        public string? Description { get; set; }

        public long Price { get; set; }
    }
}
