using final1.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace final1.Models
{
    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new final1Context(serviceProvider.GetRequiredService<DbContextOptions<final1Context>>()))
            {
                if (context.Products.Any())    // Check if database contains any products
                {
                    return;     // Database contains products already
                }

                context.Products.AddRange(
                    new Products
                    {
                        Name = "Bröderna Lejonhjärta",
                        Detail = "Swedish",
                        Color = "green",
                        Category = "Polish",
                        Price = 139,
                        ImageUrl1 = "/images/lejonhjärta.jpg",
                        ImageUrl2 = "/images/lejonhjärta.jpg",
                        ImageUrl3 = "/images/lejonhjärta.jpg",
                        ImageUrl4 = "/images/lejonhjärta.jpg"
                    },

                    new Products
                    {
                        Name = "Nail",
                        Detail = "Swedish",
                        Color = "Yellow",
                        Category = "Polish",
                        Price = 90,
                        ImageUrl1 = "/images/lejonhjärta.jpg",
                        ImageUrl2 = "/images/lejonhjärta.jpg",
                        ImageUrl3 = "/images/lejonhjärta.jpg",
                        ImageUrl4 = "/images/lejonhjärta.jpg"
                    }

                );

                context.SaveChanges();
            }
        }
    }
}
