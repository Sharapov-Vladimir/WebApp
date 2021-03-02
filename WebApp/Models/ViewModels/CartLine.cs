using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Data;

namespace WebApp.Models.ViewModels
{
    [NotMapped]
    public class CartLine
    {
        
        public Product product { get; set; }
        public int quantity { get; set; }
        
        
    }
}
