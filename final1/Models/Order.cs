using System.Collections.Generic;
using System.Data;

namespace final1.Models
{
    public class Order
    {
        public int Id { get; set; }
        public List<OrderItem> OrderItem { get; set; }
        public int OrderTotal { get; set; }
       
    }
}
