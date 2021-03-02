
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApp.Services;
using WebApp.Models;
using Microsoft.AspNetCore.Identity;
using WebApp.Data;
using Microsoft.AspNetCore.Authorization;

namespace WebApp.Areas.Admin.Controllers
{
    [Authorize(Roles ="admin")]
    [Area("Admin")]

    public class EmployeeController : Controller
    {
        private IStoreRepository repo;
        private readonly UserManager<User> userManager;

        public EmployeeController(IStoreRepository repo, UserManager<User> userManager)
        {
            this.userManager = userManager;
            this.repo = repo;
        }

        [HttpGet]
        public IActionResult List()
        {
            var couriers = repo.GetCouriers();
            
            return View(couriers);
        }

        [HttpGet]
        public IActionResult Orders(string courierId)
        {
            var orders = repo.GetCouriers()
                .FirstOrDefault(p => p.Id == courierId)
                .personalOrders;
            
            return View(orders);
        }


        [HttpGet]
        public IActionResult Edit(string courierId)
        {
            var toEdit = repo.GetCouriers()
                .FirstOrDefault(c => c.Id == courierId);
           
            return View(toEdit);
        }

        [HttpPost]
        public IActionResult Edit(Courier courier)
        {
            if (ModelState.IsValid)
            {
                var user = userManager.FindByIdAsync(courier.Id).Result;
               
                user.PhoneNumber = courier.PhoneNumber;
                user.UserName = courier.PhoneNumber;
                userManager.UpdateAsync(user);
                repo.SaveCourier(courier);
                
                TempData["message"] = "Изменения приняты";
                return View(courier.Id);
            }
            
            return View(courier.Id);
        }


        [HttpGet]
        public IActionResult Create()
        {
            var toCreate = new Courier();

            return View(toCreate);
        }


        [HttpPost]
        public IActionResult Create(Courier courier)
        {
            
            if (ModelState.IsValid)
            {
                
                User user = new User { PhoneNumber = courier.PhoneNumber , UserName=courier.PhoneNumber};
               
                string password = "password";
                
                var result = userManager.CreateAsync(user, password).Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "courier").Wait();
                    courier.Id = user.Id;
                    repo.SaveCourier(courier);
                    TempData["message"] = "Курьер зарегистрирован";
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            
            return View();
        }
        
        [HttpPost]
        public IActionResult Delete(string courierId) 
        {
           
            repo.DeleteCourier(courierId);
            var toDelete = userManager.FindByIdAsync(courierId).Result;
            
            userManager.DeleteAsync(toDelete).Wait();
            
            TempData["message"] = "Аккаунт удален";
            return RedirectToAction("List");
        }

    }
}
