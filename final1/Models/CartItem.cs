namespace final1.Models
{
    public class CartItem
    {
        public int Id { get; set; }
        public Products Product { get; set; }
        public int Quantity { get; set; }
        public string CartId { get; set; }
    }
}
