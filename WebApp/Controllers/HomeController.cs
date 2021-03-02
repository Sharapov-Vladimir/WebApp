using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using WebApp.Data;
using WebApp.Models;
using WebApp.Models.ViewModels;
using WebApp.Services;


namespace WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IStoreRepository repo;
        private int pageSize = 6;

        public HomeController(ILogger<HomeController> logger , IStoreRepository repo)
        {
            _logger = logger;
            this.repo = repo;
            
        }
        
       [HttpGet]
        public IActionResult Index()
        {
            var model = new IndexViewModel();
            model.categories = repo.GetCategories();
           
            return View(model);
        }
       
        [HttpGet]
        public IActionResult List(string category , int page = 1)
        {
            var categories = repo.GetCategories();

            var products = categories
                .FirstOrDefault(c => c.Name == category)
                .Products
                .OrderBy(x => x.Id);
            
            ProductListViewModel model = new ProductListViewModel();

            model.Categories = categories;
            model.Products = products
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();
            model.CurrentCategory = category;
            
            model.PagingInfo = new PagingInfo
            {
                CurrentPage = page,
                ItemsPerPage = pageSize,
                TotalItems = products.Count()
            };
            
            return View(model);
        }
       
        
        
        [HttpPost]
        public IActionResult Order(ProductListViewModel model) 
        {
            if (ModelState.IsValid) 
            { 
            
                Order order = new Order(model.Form);
         
                var convertedLines = (List<CartLine>)JsonConvert.DeserializeObject(model.Form.JsonLines, typeof(List<CartLine>));

                foreach (var line in convertedLines)
                {
                    order.Lines.Add(new OrderedProduct(line));
                }

                repo.SaveOrder(order);
            
                return RedirectToAction("Index");
            }
            else
            {

                return RedirectToAction("Index");
            }

        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
       
    }
}
