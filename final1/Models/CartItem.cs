namespace final1.Models
{
    public class CartItems
    {
        public int Id { get; set; }
        public Products Product { get; set; }
        public int Quantity { get; set; }
        public string CartId { get; set; }
    }
}
