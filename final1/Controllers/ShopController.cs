using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using final1.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace final1.Controllers
{
    public class ShopController : Controller
    {
       
        private readonly final1Context _context;

        public ShopController(final1Context context) 
        {
            _context = context;
        }
        public async Task<IActionResult> Index(string querry)
        {
            if (string.IsNullOrEmpty(querry))
            {
                return View(await _context.Products.ToListAsync());

            }
            else
            {
                return View(await _context.Products.Where(x => x.Name.Contains(querry)).ToListAsync());
            }
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
        #region category page 
        public async Task<IActionResult> Polish(string querry)
        {
            if (string.IsNullOrEmpty(querry))
            {
                var ps = _context.Products.Where(x => x.Category== "polish").ToList();
                return View(ps);
            }
            else { 
            return View(await _context.Products.Where(x=>x.Category == "polish" && x.Name.Contains(querry)).ToListAsync());
            }
        }

        public async Task<IActionResult> Fakenail(string querry)
        {
            if (string.IsNullOrEmpty(querry))
            {
                var ps = _context.Products.Where(x => x.Category == "fakenail").ToList();
                return View(ps);
            }
            else
            {
                return View(await _context.Products.Where(x => x.Category == "fakenail" && x.Name.Contains(querry)).ToListAsync());
            }
        }

        public async Task<IActionResult> Tools(string querry)
        {
            if (string.IsNullOrEmpty(querry))
            {
                var ps = _context.Products.Where(x => x.Category == "tools").ToList();
                return View(ps);
            }
            else
            {
                return View(await _context.Products.Where(x => x.Category == "tools" && x.Name.Contains(querry)).ToListAsync());
            }
        }
        #endregion

        #region fiter color
        [HttpGet]
        public ViewResult Polish_(string p)
        {

            if (string.IsNullOrEmpty(p))
            {
                var ps = _context.Products.Where(x => x.Category == "polish").ToList();
                return View(ps);
            }

            else
            {
                return View("Polish", _context.Products.Where(x => x.Category == "polish"&& x.Color.Contains(p)));
            }

        }

        public ViewResult Fakenail_(string n)
        {

            if (string.IsNullOrEmpty(n))
            {
                var ps = _context.Products.Where(x => x.Category == "fakenail").ToList();
                return View(ps);
            }

            else
            {
                return View("Polish", _context.Products.Where(x => x.Category == "fakenail" && x.Color.Contains(n)));
            }

        }
        #endregion
    }
}
