using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApp.Data;
using WebApp.Models.ViewModels;

namespace WebApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager )
        {
            _userManager = userManager;
            _signInManager = signInManager;
           
        }
       
       
        [HttpGet]
        public IActionResult Login(string returnUrl = null)
        {
            return View(new LoginViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                
                var result =
                    await _signInManager.PasswordSignInAsync(model.Name, model.Password, model.RememberMe, false);
                
                if (result.Succeeded)
                {
                    var user = await _userManager.FindByNameAsync(model.Name);
                    var roles = await _userManager.GetRolesAsync(user);
                   
                   
                        if (roles.Contains("admin"))
                        {
                            return RedirectToAction("Index","Home", new {area="Admin"});
                        }
                        if (roles.Contains("courier"))
                        {
                            return RedirectToAction("Index", "Home", new { area = "Employee" });
                        }

                        return RedirectToAction("Index", "Home");
                    
                }
                else
                {
                    ModelState.AddModelError("", "Неправильный логин и (или) пароль");
                }
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
           
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
