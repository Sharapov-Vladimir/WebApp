﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Models;
using WebApp.Models.ViewModels;

namespace WebApp.Data
{
    public class Product
    {
       
        public int Id { get; set; }
        
        
        public string Name { get; set; }

        
        public string Description { get; set; }
        
        public Category Category { get; set; }

        public int CategoryId { get; set; }

        public decimal Price { get; set; }
        
        public string ImgPath { get; set; }

        

       
        
    }
}
