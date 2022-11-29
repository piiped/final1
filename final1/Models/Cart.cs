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
        public List<CartItems> CartItems { get; set; }

        public static Cart GetCart(IServiceProvider services)
        {
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;

            var context = services.GetService<final1Context>();
            string cartId = session.GetString("Id") ?? Guid.NewGuid().ToString();

            session.SetString("Id", cartId);

            return new Cart(context) { Id = cartId };
        }

        public CartItems GetCartItems(Products Product)
        {
            return _context.CartItems.SingleOrDefault(ci =>
                ci.Product.Id == Product.Id && ci.CartId == Id);
        }

        public void AddToCart(Products Product, int quantity)
        {
            var CartItems = GetCartItems(Product);

            if (CartItems == null)
            {
                CartItems = new CartItems
                {
                    Product = Product,
                    Quantity = quantity,
                    CartId = Id
                };

                _context.CartItems.Add(CartItems);
            }
            else
            {
                CartItems.Quantity += quantity;
            }
            _context.SaveChanges();
        }



        public int ReduceQuantity(Products products)
        {
            var CartItems = GetCartItems(products);
            var remainingQuantity = 0;

            if (CartItems != null)
            {
                if(CartItems.Quantity > 1) 
                {
                    remainingQuantity = --CartItems.Quantity;
                }
                else
                {
                    _context.CartItems.Remove(CartItems);
                }
            }
            _context.SaveChanges();

            return remainingQuantity;
        }

        public int IncreaseQuantity(Products products)
        {
            var CartItems = GetCartItems(products);
            var remainingQuantity = 0;

            if (CartItems != null)
            {
                if (CartItems.Quantity > 0)
                {
                    remainingQuantity = ++CartItems.Quantity;
                }

            }
            _context.SaveChanges();

            return remainingQuantity;
        }

        public void RemoveFromCart(Products product)
        {
            var cartItem = GetCartItems(product);

            if (cartItem != null)
            {
                _context.CartItems.Remove(cartItem);
            }
            _context.SaveChanges();
        }

        public void ClearCart()
        {
            var cartItems = _context.CartItems.Where(ci => ci.CartId == Id);

            _context.CartItems.RemoveRange(cartItems);

            _context.SaveChanges();
        }

        public List<CartItems> GetAllCartItems()
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
