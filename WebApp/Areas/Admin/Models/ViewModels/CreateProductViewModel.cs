using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Data;
using WebApp.Models;

namespace WebApp.Areas.Admin.Models.ViewModels
{
    public class CreateProductViewModel
    {
      
        public List<Category> Categories { get; set; }

        public string SelectedCategory { get; set; }
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        
        [Required]
        public string Description { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:C2}")]
        [Range(1,100000)]
        [DataType(DataType.Currency)]
        public double Price { get; set; }
    }
}
