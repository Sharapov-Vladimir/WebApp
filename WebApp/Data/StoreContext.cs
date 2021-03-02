using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Models;
using WebApp.Services;

namespace WebApp.Data
{
    public class StoreContext:DbContext
    {
        public StoreContext(DbContextOptions<StoreContext> options) : base(options) 
        {
            
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order>Orders { get; set; }
        public DbSet<Courier> Couriers { get; set; }

        public DbSet<Category> Categories { get; set; }
      
    }
}
