using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApp.Areas.Admin.Models.ViewModels;
using WebApp.Models.ViewModels;
using WebApp.Services;

namespace WebApp.Areas.Admin.Controllers
{
    [Authorize(Roles = "admin")]
    [Area("Admin")]
    
    public class OrderController : Controller
    {
        private IStoreRepository repo;
        private int pageSize = 10;

        public OrderController(IStoreRepository repo)
        {
            this.repo = repo;
        }
       
        [HttpGet]
        public IActionResult Archive(int page = 1)
        {
            var model = new OrderViewModel();
            var orders = repo.GetOrders()
               .Where(order => order.IsActive == false);
           
            model.Orders = orders.Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();
            
            model.PagingInfo = new PagingInfo
            {
                ItemsPerPage = pageSize,
                TotalItems = orders.Count(),
                CurrentPage = page
            };

            return View(model);  
        }

        [HttpGet]
        public IActionResult Hot(int page = 1)
        {
            var model = new OrderViewModel();
            var orders = repo.GetOrders()
                .Where(order => (order.IsActive == true && order.courier == null));
           
            model.Orders = orders.Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();
            model.PagingInfo = new PagingInfo
            {
                ItemsPerPage = pageSize,
                TotalItems = orders.Count(),
                CurrentPage = page
            };
           
            return View(model);
        }

        [HttpGet]
        public IActionResult Dispatched(int page=1)
        {
      
            var model = new OrderViewModel();
            var orders = repo.GetOrders().Where(order => (order.IsActive == true && (order.courier != null)));
            
            model.Orders = orders.Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();
            model.PagingInfo = new PagingInfo
            {
                ItemsPerPage = pageSize,
                TotalItems = orders.Count(),
                CurrentPage = page
            };
           
            return View(model);
        }
        
        [HttpPost]
        public IActionResult Delete(int OrderId , string returnToAction)
        {
            
            repo.DeleteOrder(OrderId);

            TempData["message"] = "Заказ был удален";
            return RedirectToAction(returnToAction);
        }

        [HttpPost]
        public IActionResult ToArchive (int OrderId, string returnToAction)
        {
            
            var order = repo.GetOrders().FirstOrDefault(x => x.Id == OrderId);
            
               
                order.IsActive = false;
                repo.SaveOrder(order);


            TempData["message"] = "Заказ помещен в архив";
            return RedirectToAction(returnToAction);
            
        }
    }
}
