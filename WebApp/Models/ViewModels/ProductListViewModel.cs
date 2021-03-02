using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Data;

namespace WebApp.Models.ViewModels
{
    public class ProductListViewModel
    {
        public List<Product> Products { get; set; }
        public OrderFormViewModel Form { get; set; }
        public PagingInfo PagingInfo { get; set; }
        public List<Category> Categories { get; set; }
        public string CurrentCategory { get; set; }
        
    }
}
