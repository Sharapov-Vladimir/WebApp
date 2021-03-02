using System;
using System.Collections.Generic;
using WebApp.Models;
using WebApp.Models.ViewModels;


namespace WebApp.Data
{
    public class Order
    {
        public Order()
        {

        }
        public Order(OrderFormViewModel form)
        {
            Custumer = form.Custumer;
            PhoneNumber = form.PhoneNumber;
            Adress = form.Adress;
            Comment = form.Comment;
            Amount = decimal.Parse(form.Amount);
            Date = DateTime.Parse(form.Date);
            Lines = new List<OrderedProduct>();

        }
        public int Id { get; set; }

       
        public List<OrderedProduct> Lines { get; set; }

        
        public decimal Amount { get; set; }

        
        public string Custumer { get; set; }
        
        
        public string Adress { get; set; }
        
       
        public string PhoneNumber { get; set; }

        public string Comment { get; set; }
        
       
        public DateTime Date { get; set; }

        public bool IsActive { get; set; } = true;
        public bool IsDelivered { get; set; } = false;
        public Courier courier { get; set; }
        
       
    }
}
