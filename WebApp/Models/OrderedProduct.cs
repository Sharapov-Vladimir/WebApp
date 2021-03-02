using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Data;
using WebApp.Models.ViewModels;

namespace WebApp.Models
{
    public class OrderedProduct
    {
        public OrderedProduct()
        {
                
        }
        public OrderedProduct(CartLine cartLine)
        {
            Name = cartLine.product.Name;
            Description = cartLine.product.Description;
            Category = cartLine.product.Category.Name;
            Price = cartLine.product.Price;
            Quantity = cartLine.quantity;

        }
        public int Id { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }

        public string Category { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }
      
    }
}
