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

        public object CartItems { get; internal set; }

        public CartController(final1Context context, Cart cart)
        {
            _context = context;
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
       
        public IActionResult AddToCartp(int id)
        {
            var selectedPd = GetPdById(id);

            if (selectedPd != null)
            {
                _cart.AddToCart(selectedPd, 1);
             
            }
            return RedirectToAction("Polish", "Shop");
        }
        public IActionResult AddToCartf(int id)
        {
            var selectedPd = GetPdById(id);

            if (selectedPd != null)
            {
                _cart.AddToCart(selectedPd, 1);

            }
            return RedirectToAction("Fakenail", "Shop");
        }
        public IActionResult AddToCartt(int id)
        {
            var selectedPd = GetPdById(id);

            if (selectedPd != null)
            {
                _cart.AddToCart(selectedPd, 1);

            }
            return RedirectToAction("Tools", "Shop");
        }


        public IActionResult RemoveFromCart(int id)
        {
            var selectedPd = GetPdById(id);

            if (selectedPd != null)
            {
                _cart.RemoveFromCart(selectedPd);
            }

            return RedirectToAction("Index");
        }

        public IActionResult ReduceQuantity(int id)
        {
            var selectedPd = GetPdById(id);

            if (selectedPd != null)
            {
                _cart.ReduceQuantity(selectedPd);
            }

            return RedirectToAction("Index");
        }

        public IActionResult IncreaseQuantity(int id)
        {
            var selectedPd = GetPdById(id);

            if (selectedPd != null)
            {
                _cart.IncreaseQuantity(selectedPd);
            }

            return RedirectToAction("Index");
        }

        public IActionResult ClearCart()
        {
            _cart.ClearCart();

            return RedirectToAction("Index");
        }

        public Products GetPdById(int id)
        {
            return _context.Products.FirstOrDefault(b => b.Id == id);
        }
    }
}