using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApp.Data;
using WebApp.Services;

namespace WebApp.Areas.Employee.Controllers
{
    [Authorize(Roles = "courier")]
    [Area("Employee")]
    public class HomeController : Controller
    {
        private IStoreRepository repo;
       
        private  UserManager<User> userManager;
       

        public HomeController(IStoreRepository repo, UserManager<User> userManager)
        {
            this.repo = repo;
            this.userManager = userManager;
           
        }

        public  IActionResult Index()
        {

            var user = userManager.GetUserAsync(this.User).Result;
            var courier = repo.GetCouriers().FirstOrDefault(c => c.Id == user.Id);
            
            return View(courier);
        }

        public IActionResult Hot()
        {
            var orders = repo.GetOrders()
                .Where(order => (order.IsActive == true && order.courier == null))
                .OrderByDescending(o=>o.Date)
                .ToList();
           
            return View(orders);
        }

       

        public  IActionResult AcceptOrder(int OrderId)
        {
           
            var user = userManager.GetUserAsync(this.User).Result;
            var courier = repo.GetCouriers().FirstOrDefault(c => c.Id == user.Id);
            var order = repo.GetOrders().FirstOrDefault(o => o.Id == OrderId);
            
            order.courier = courier;
            courier.personalOrders.Add(order);
            repo.SaveCourier(courier);
            repo.SaveOrder(order);

            ViewData["message"] = "Заказ принят";
            return RedirectToAction("Hot");
        }

        public IActionResult ConfirmDelivery(int OrderId)
        {
            
            var user = userManager.GetUserAsync(this.User).Result;
            var courier = repo.GetCouriers().FirstOrDefault(c => c.Id == user.Id);
            var order = repo.GetOrders().FirstOrDefault(o => o.Id == OrderId);
            
            courier.personalOrders.Remove(order);
            order.IsDelivered = true;
            order.IsActive = false;
            courier.personalOrders.Add(order);
            
            repo.SaveCourier(courier);
            repo.SaveOrder(order);

            ViewData["message"] = "Статус изменен";
            return RedirectToAction("Index");
        }


        [HttpPost]
        public IActionResult Delete(int OrderId)
        {
           
            var order = repo.GetOrders().FirstOrDefault(o => o.Id == OrderId);
            var user = userManager.GetUserAsync(this.User).Result;
            var courier = repo.GetCouriers().FirstOrDefault(c => c.Id == user.Id);
           
            courier.personalOrders.Remove(order);
            order.courier = null;
            repo.SaveCourier(courier);
            repo.SaveOrder(order);

            TempData["message"] = "Заказ был удален";
            return RedirectToAction("MyOrders");
        }
        
       
    }
}
