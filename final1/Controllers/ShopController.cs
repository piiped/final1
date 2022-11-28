using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using final1.Data;
using Microsoft.EntityFrameworkCore;

namespace final1.Controllers
{
    public class ShopController : Controller
    {
        private readonly final1Context _context;

        public ShopController(final1Context context) 
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _context.Products.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var products = await _context.Products
                .FirstOrDefaultAsync(m => m.Id == id);
            if (products == null)
            {
                return NotFound();
            }

            return View(products);
        }
    }
}
