﻿using System.Security.Permissions;

namespace final1.Models
{
    public class OrderItem
    {
        public int Id { get; set; }
        public int Quantity { get; set; }   
        public int Price { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public Order Order { get; set; }
        public Products Product { get; set; }
    }
}
