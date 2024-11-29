namespace Core.Entities
{
    //[Table("Products", Schema = "Base")]
    public class Product
    {
        //[Key]
        public int Id { get; set; }

        //[MaxLength(128), Required]
        public required string ProductName { get; set; }

        public long Price { get; set; }
    }
}
