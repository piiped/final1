using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using final1.Models;

namespace final1.Data
{
    public class final1Context : DbContext
    {
        public final1Context (DbContextOptions<final1Context> options)
            : base(options)
        {
        }

        public DbSet<Products> Products { get; set; }
    }
}
