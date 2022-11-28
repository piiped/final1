using final1.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;

namespace final1.Models
{
    public class Cart
    {
        private readonly final1Context _context;

        public Cart(final1Context context)
        {
            _context = context;
        }

        public string Id { get; set; }
        public List<CartItem> CartItems { get; set; }

        public static Cart GetCart(IServiceProvider services)
        {
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;

            var context = services.GetService<final1Context>();
            string cartId = session.GetString("Id") ?? Guid.NewGuid().ToString();

            session.SetString("Id", cartId);

            return new Cart(context) { Id = cartId };
        }

        public CartItem GetCartItem(Products Product)
        {
            return _context.CartItems.SingleOrDefault(ci =>
                ci.Product.Id == Product.Id && ci.CartId == Id);
        }

        public void AddToCart(Products Product, int quantity)
        {
            var cartItem = GetCartItem(Product);

            if (cartItem == null)
            {
                cartItem = new CartItem
                {
                    Product = Product,
                    Quantity = quantity,
                    CartId = Id
                };

                _context.CartItems.Add(cartItem);
            }
            else
            {
                cartItem.Quantity += quantity;
            }
            _context.SaveChanges();
        }

        public List<CartItem> GetAllCartItems()
        {
            return CartItems ??
                (CartItems = _context.CartItems.Where(ci => ci.CartId == Id)
                    .Include(ci => ci.Product)
                    .ToList());                              
        }

        public int GetCartTotal()
        {
            return _context.CartItems
                .Where(ci => ci.CartId == Id)
                .Select(ci => ci.Product.Price * ci.Quantity)
                .Sum();
        }
    }
}
