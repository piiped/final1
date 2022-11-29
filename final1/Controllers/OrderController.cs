using final1.Data;
using final1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;


namespace final1.Controllers
{
    public class OrderController : Controller
    {
        private readonly final1Context _context;
        private readonly Cart _cart;

        public OrderController(final1Context context, Cart cart)
        {
            _context = context;
            _cart = cart;
        }
        public IActionResult Checkout()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Checkout(Order order)
        {
            var cartItems = _cart.GetAllCartItems();
            _cart.CartItems = cartItems;

            if (_cart.CartItems.Count == 0) 
            {
                ModelState.AddModelError("","Cart is empty, please add a product first.");
            }
            if (ModelState.IsValid)
            {
                CreateOrder(order);
                _cart.ClearCart();
                return View("CheckoutComplete", order);

            }
            return View(order);
        }

        public IActionResult CheckoutComplete(Order order)
        {
            return View(order);
        }

        public void CreateOrder(Order order)
        {
            var cartItems = _cart.CartItems;

            foreach (var item in cartItems)
            {
                var orderItem = new OrderItem()
                {
                    Quantity = item.Quantity,
                    ProductId = item.Product.Id,
                    OrderId = order.Id,
                    Price = item.Product.Price * item.Quantity
                };
            order.OrderItem.Add(orderItem);
            order.OrderTotal += orderItem.Price;
            }
            _context.Order.Add(order);
            _context.SaveChanges();
        }
    }
}
