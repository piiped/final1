using final1.Data;
using final1.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace final1.Controllers
{
    public class CartController : Controller
    {
        private readonly final1Context _context;
        private readonly Cart _cart;

        public CartController(final1Context context, Cart cart)
        {
            _cart = cart;
        }

        public IActionResult Index()
        {
            var items = _cart.GetAllCartItems();
            _cart.CartItems = items;
            return View(_cart);
        }

        public IActionResult AddToCart(int id)
        {
            var selectedPd = GetPdById(id);

            if (selectedPd != null)
            {
                _cart.AddToCart(selectedPd, 1);
            }

            return RedirectToAction("Index", "Shop");
        }
        public Products GetPdById(int id)
        {
            return _context.Products.FirstOrDefault(b => b.Id == id);
        }
    }
}