using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApp.Areas.Admin.Models.ViewModels;
using WebApp.Data;
using WebApp.Models;
using WebApp.Services;

namespace WebApp.Areas.Admin.Controllers
{
    [Authorize(Roles = "admin")]
    [Area("Admin")]
    
    public class ProductController : Controller
    {
        private IWebHostEnvironment env;
        private IStoreRepository repo;
        
        public ProductController(IStoreRepository repo, IWebHostEnvironment env)
        {
            this.env = env;
            this.repo = repo;
        }

        [HttpGet]
        public IActionResult List(int SelectedCategoryId=0)
        {
            var model = new ListViewModel();
            
            model.Categories = repo.GetCategories();
            model.SelectedCategory = repo.GetCategories()
                .FirstOrDefault(c => c.Id == SelectedCategoryId);
            
            return View(model);
        }

        [HttpGet]
        public IActionResult CreateProduct()
        {
            CreateProductViewModel model = new CreateProductViewModel();
            model.Categories = repo.GetCategories();
            return View(model);
        }

        [HttpPost]
        public IActionResult CreateProduct(CreateProductViewModel model, IFormFile file)
        {
            var categories = repo.GetCategories();
            model.Categories = categories;
           
            if (file == null)
            {
                ModelState.AddModelError("img", "Добавьте изображение");
            }
            else if (!file.ContentType.Contains("image"))
            {
                ModelState.AddModelError("img", "Неверный формат изображения");
            }

            if (ModelState.IsValid)
            {

                var product = new Product
                {
                    Name = model.Name,
                    Description = model.Description,
                    Price = (decimal)model.Price 
                    
                };
                var category = categories.FirstOrDefault(c => c.Name == model.SelectedCategory);
                string root = env.WebRootPath;
                string path = @"\img\products\";

               
                 using (var fs = new FileStream(Path.Combine(root + path, file.FileName), FileMode.Create, FileAccess.Write))
                 {
                     file.CopyTo(fs);
                 }
              
               
                product.ImgPath = path + file.FileName;
                product.Category = category;
                category.Products.Add(product);
                repo.SaveProduct(product);
               
                TempData["message"] = "Продукт добавлен";
                return View(model);

            }
            return View(model);
        }

        [HttpGet]
        public IActionResult CreateCategory()
        {
            Category model = new Category();
            
            return View(model);
        }
        [HttpPost]
        public IActionResult CreateCategory(Category model, IFormFile file)
        {
            if (file == null)
            {
                ModelState.AddModelError("img", "Добавьте изображение");
            }
            else if (!file.ContentType.Contains("image"))
            {
                ModelState.AddModelError("img", "Неверный формат изображения");
            }

            if (ModelState.IsValid)
            {
                string root = env.WebRootPath;
                string path = @"\img\products\";

                using (var fs = new FileStream(Path.Combine(root+path, file.FileName), FileMode.Create, FileAccess.Write))
                {
                    file.CopyTo(fs);
                }

                model.ImgPath = path+file.FileName;
                repo.SaveCategory(model);
                
                TempData["message"] = "Категория добавлена";
                return View(model);
            }
            return View(model);
           
        }

        [HttpGet]
        public IActionResult EditCategory(int SelectedCategoryId)
        {
            var model = repo.GetCategories().FirstOrDefault(c => c.Id == SelectedCategoryId);

            return View(model);
        }

        [HttpPost]
        public IActionResult EditCategory(Category model, IFormFile file)
        {
            if (file != null)
            {
                if (!file.ContentType.Contains("image"))
                {
                    ModelState.AddModelError("img", "Неверный формат изображения");
                }
            }
            
            if (ModelState.IsValid)
            {
                var category = repo.GetCategories().FirstOrDefault(c => c.Id == model.Id);
                
                if (file != null)
                {
                    string root = env.WebRootPath;
                    string path = @"\img\products\";
                    
                    using (var fs = new FileStream(Path.Combine(root+path, file.FileName), FileMode.Create, FileAccess.Write))
                    {
                        
                        file.CopyTo(fs);
                        System.IO.File.Delete(category.ImgPath);
                    }

                    category.ImgPath =path+file.FileName;
                }
                
                category.Name = model.Name;
                repo.SaveCategory(category);

                TempData["message"] = "Изменения приняты";
                return RedirectToAction("List");
            }

            return View(model);
        }
       
        [HttpGet]
        public IActionResult EditProduct(int ProductId)
        {
           
            CreateProductViewModel model = new CreateProductViewModel();
            var product = repo.GetProducts().FirstOrDefault(p => p.Id == ProductId);
           
            model.Name = product.Name;
            model.Description = product.Description;
            model.Price = (double)product.Price;
            model.Categories = repo.GetCategories();
            model.SelectedCategory = product.Category.Name;
            model.Id = ProductId;
        
            return View(model);
        }

        [HttpPost]
        public IActionResult EditProduct(CreateProductViewModel model, IFormFile file)
        {
            var product = repo.GetProducts().FirstOrDefault(p => p.Id == model.Id);
            var categories = repo.GetCategories();
            model.Categories = categories;

            if (file != null)
            {
                if (!file.ContentType.Contains("image"))
                {
                    ModelState.AddModelError("img", "Неверный формат изображения");
                }
            }
           
            if (ModelState.IsValid)
            {
                var category = categories.FirstOrDefault(c => c.Name == model.SelectedCategory);
              
                if (file != null)
                {
                    string root = env.WebRootPath;
                    string path = @"\img\products\";
                   
                    using (var fs = new FileStream(Path.Combine(root+path, file.FileName), FileMode.Create, FileAccess.Write))
                    {
                        file.CopyTo(fs);
                        System.IO.File.Delete(root+product.ImgPath);

                    }

                    product.ImgPath = path+file.FileName;
                }

                product.Name = model.Name;
                product.Price = (decimal)model.Price; 
                product.Description = model.Description;
                product.Category = category;
                repo.SaveProduct(product);
                
                TempData["message"] = "Продукт был изменен";
                return RedirectToAction("List");
            }
            
            return View(model);
        }

        [HttpPost]
        public IActionResult DeleteProduct(int ProductId)
        {
            repo.DeleteProduct(ProductId);
            
            TempData["message"] = "Продукт удален";
            return RedirectToAction("List");
        }

        [HttpPost]
        public IActionResult DeleteCategory(int SelectedCategoryId)
        {
            repo.DeleteCategory(SelectedCategoryId);
            
            TempData["message"] = "Категория удалена";
            return RedirectToAction("List");
        }
    }
}
