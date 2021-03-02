using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Data;
using WebApp.Models.ViewModels;

namespace WebApp.Areas.Admin.Models.ViewModels
{
    public class OrderViewModel
    {
        public List<Order> Orders { get; set; }
        public PagingInfo PagingInfo { get; set; }
    }
}
