using System.Collections.Generic;
using System.Data;
using System;

namespace final1.Models
{
    public class Order
    {
        public int Id { get; set; }
        public List<OrderItem> OrderItem { get; set; } = new();
        public int OrderTotal { get; set; }
       
    }
}
