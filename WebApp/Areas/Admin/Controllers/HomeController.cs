using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApp.Areas.Admin.Models.ViewModels;
using WebApp.Services;

namespace WebApp.Areas.Admin.Controllers
{
    [Authorize(Roles ="admin")]
    [Area("Admin")]
    public class HomeController : Controller
    {
        private IStoreRepository repo;

        public HomeController(IStoreRepository repo)
        {
            this.repo = repo;
        }
        public IActionResult Index()
        {
            var model = new IndexViewModel(repo);
          
            return View(model);
        }
    }
}
