using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Data;

namespace WebApp.Models
{
    public class Courier
    {
       
            public string Id { get; set; }

            [Required]   
            public string FirstName { get; set; }
        
            [Required]
            public string LastName { get; set; }
            
            [Required]
            [DataType(DataType.PhoneNumber, ErrorMessage = "Неверный формат")]
            [RegularExpression(@"^\+[0-9]{2}([0-9]{10})$", ErrorMessage = "Неверный формат")]
            public string PhoneNumber { get; set; }
            public List<Order> personalOrders { get; set; }
        
    }
}
