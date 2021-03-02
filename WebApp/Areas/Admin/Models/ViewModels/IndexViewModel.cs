using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Services;

namespace WebApp.Areas.Admin.Models.ViewModels
{
    public class IndexViewModel
    {
        private IStoreRepository repo;
        public IndexViewModel(IStoreRepository repo )
        {

            this.repo = repo;

        }

       
        public Array ColumnChart
        {
            get
            {
                return repo.GetOrders()
                 .Where(x=>x.IsDelivered==true)
                 .GroupBy(x => x.Date.Year)
                 .OrderByDescending(x => x.Key)
                 .Take(1)
                 .SelectMany(x => x)
                 .GroupBy(x => x.Date.ToString("MMMM"))
                 .Select(x => new { 
                     Month = x.Key, 
                     Orders = x.Count() , 
                     Amount = x.Select(x=>x.Amount)
                               .Aggregate((x,y)=>x+y)/1000
                 })
                 .ToArray();
            }
        }

        public Array PieChartCategory
        {
            get
            {
                return repo.GetOrders()
                 .Where(order => order.IsDelivered == true)
                 .GroupBy(order => order.Date.Year)
                 .OrderByDescending(gr => gr.Key)
                 .Take(1)
                 .SelectMany(ord => ord)
                 .SelectMany(l => l.Lines)
                 .GroupBy(product => 
                 product.Category, 
                 (category, products) => new { 
                     Category = category, 
                     Quantity = products.Count() 
                 })
                 .OrderByDescending(x => x.Quantity)
                 .ToArray();
            }
        }

       
    }
}
