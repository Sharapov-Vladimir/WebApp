using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Models;
using WebApp.Services;

namespace WebApp.Data
{
    public static class DbInitializer
    {

        
        private static List<Category> Categories = new List<Category>
        {
            new Category
            {
                Name="Пицца",
                ImgPath="/icons/Categories/пицца.png"
            },
            new Category
            {
                Name="Бургеры",
                ImgPath="/icons/Categories/бургеры.png"
            },
            new Category
            {
                Name="Салаты",
                ImgPath="/icons/Categories/салаты.png"
            },
            new Category
            {
                Name="Роллы",
                ImgPath="/icons/Categories/роллы.png"
            },
            new Category
            {
                Name="Напитки",
                ImgPath="/icons/Categories/напитки.png"
            }
        };

        private static List<Product> Products = new List<Product>
        {
            new Product
            {
                Category = Categories.FirstOrDefault(c=>c.Name=="Пицца"),
                Name = "Американа",
                Description = "Описание.....",
                Price = 150,
                ImgPath = "/img/Products/Американа.jpeg"
            },
            new Product
            {
                Category = Categories.FirstOrDefault(c=>c.Name=="Пицца"),
                Name = "Маргарита",
                Description = "Описание.....",
                Price = 165,
                ImgPath = "/img/Products/Маргарита.jpeg"
            },
            new Product
            {
                Category = Categories.FirstOrDefault(c=>c.Name=="Пицца"),
                Name = "Четыре сыра",
                Description = "Описание.....",
                Price = 170,
                ImgPath = "/img/Products/Четыре сыра.jpeg"
            },
            new Product
            {
                Category = Categories.FirstOrDefault(c=>c.Name=="Пицца"),
                Name = "Карбонара",
                Description = "Описание.....",
                Price = 140,
                ImgPath = "/img/Products/Карбонара.jpeg"
            },
            new Product
            {
                Category = Categories.FirstOrDefault(c=>c.Name=="Пицца"),
                Name = "Баварская",
                Description = "Описание.....",
                Price = 175,
                ImgPath = "/img/Products/Баварская.jpeg"
            },
            new Product
            {
                Category = Categories.FirstOrDefault(c=>c.Name=="Пицца"),
                Name = "Кальцоне",
                Description = "Описание.....",
                Price = 180,
                ImgPath = "/img/Products/Кальцоне.jpeg"
            },
            new Product
            {
                Category = Categories.FirstOrDefault(c=>c.Name=="Роллы"),
                Name = "Филадельфия",
                Description = "Описание.....",
                Price = 370,
                ImgPath = "/img/Products/Филадельфия.jpeg"
            },
            new Product
            {
                Category = Categories.FirstOrDefault(c=>c.Name=="Роллы"),
                Name = "Калифорния",
                Description = "Описание.....",
                Price = 410,
                ImgPath = "/img/Products/Калифорния.jpeg"
            },
            new Product
            {
                Category = Categories.FirstOrDefault(c=>c.Name=="Роллы"),
                Name = "Дракон",
                Description = "Описание.....",
                Price = 560,
                ImgPath = "/img/Products/Дракон.jpeg"
            },
            new Product
            {
                Category = Categories.FirstOrDefault(c=>c.Name=="Роллы"),
                Name = "Лосось",
                Description = "Описание.....",
                Price = 680,
                ImgPath = "/img/Products/Лосось.jpeg"
            },
            new Product
            {
                Category = Categories.FirstOrDefault(c=>c.Name=="Роллы"),
                Name = "Фила",
                Description = "Описание.....",
                Price = 635,
                ImgPath = "/img/Products/Фила.jpeg"
            },
            new Product
            {
                Category = Categories.FirstOrDefault(c=>c.Name=="Салаты"),
                Name = "Цезарь",
                Description = "Описание.....",
                Price = 95,
                ImgPath = "/img/Products/Цезарь.jpeg"
            },
            new Product
            {
                Category = Categories.FirstOrDefault(c=>c.Name=="Салаты"),
                Name = "Грецкий",
                Description = "Описание.....",
                Price = 70,
                ImgPath = "/img/Products/Грецкий.jpeg"
            },
            new Product
            {
                Category = Categories.FirstOrDefault(c=>c.Name=="Салаты"),
                Name = "Цезарь с креветками",
                Description = "Описание.....",
                Price = 110,
                ImgPath = "/img/Products/Цезарь с креветками.jpeg"
            },
            new Product
            {
                Category = Categories.FirstOrDefault(c=>c.Name=="Салаты"),
                Name = "Тайский",
                Description = "Описание.....",
                Price = 85,
                ImgPath = "/img/Products/Тайский.jpeg"
            },
            new Product
            {
                Category = Categories.FirstOrDefault(c=>c.Name=="Салаты"),
                Name = "Веронезе",
                Description = "Описание.....",
                Price = 75,
                ImgPath = "/img/Products/Веронезе.jpeg"
            },
            new Product
            {
                Category = Categories.FirstOrDefault(c=>c.Name=="Напитки"),
                Name = "Borjomi",
                Description = "Описание.....",
                Price = 25,
                ImgPath = "/img/Products/Borjomi.jpeg"
            },
            new Product
            {
                Category = Categories.FirstOrDefault(c=>c.Name=="Напитки"),
                Name = "Pepsi",
                Description = "Описание.....",
                Price = 15,
                ImgPath = "/img/Products/Pepsi.jpeg"
            },
            new Product
            {
                Category = Categories.FirstOrDefault(c=>c.Name=="Напитки"),
                Name = "Ананас",
                Description = "Описание.....",
                Price = 35,
                ImgPath = "/img/Products/Ананас.jpeg"
            },
            new Product
            {
                Category = Categories.FirstOrDefault(c=>c.Name=="Напитки"),
                Name = "Апельсин",
                Description = "Описание.....",
                Price = 35,
                ImgPath = "/img/Products/Апельсин.png"
            },
            new Product
            {
                Category = Categories.FirstOrDefault(c=>c.Name=="Напитки"),
                Name = "Red Bull",
                Description = "Описание.....",
                Price = 35,
                ImgPath = "/img/Products/Red Bull.png"
            },
            new Product
            {
                Category = Categories.FirstOrDefault(c=>c.Name=="Напитки"),
                Name = "Томат",
                Description = "Описание.....",
                Price = 35,
                ImgPath = "/img/Products/Томат.png"
            },
            new Product
            {
                Category = Categories.FirstOrDefault(c=>c.Name=="Бургеры"),
                Name = "Чикен",
                Description = "Описание.....",
                Price = 85,
                ImgPath = "/img/Products/Чикен.png"
            },
            new Product
            {
                Category = Categories.FirstOrDefault(c=>c.Name=="Бургеры"),
                Name = "Чери",
                Description = "Описание.....",
                Price = 110,
                ImgPath = "/img/Products/Чери.png"
            },
            new Product
            {
                Category = Categories.FirstOrDefault(c=>c.Name=="Бургеры"),
                Name = "Дор блю",
                Description = "Описание.....",
                Price = 110,
                ImgPath = "/img/Products/Дор блю.png"
            },
            new Product
            {
                Category = Categories.FirstOrDefault(c=>c.Name=="Бургеры"),
                Name = "Биг грэм",
                Description = "Описание.....",
                Price = 140,
                ImgPath = "/img/Products/Биг грэм.png"
            },
            new Product
            {
                Category = Categories.FirstOrDefault(c=>c.Name=="Бургеры"),
                Name = "Бургер с лососем",
                Description = "Описание.....",
                Price = 145,
                ImgPath = "/img/Products/Бургер с лососем.png"
            }


        };

        private static List<IdentityRole> Roles = new List<IdentityRole>
        {
            new IdentityRole("courier"),
            new IdentityRole("admin")

        };

       public static void Initialize(RoleManager<IdentityRole> roleManager, UserManager<User> userManager,IStoreRepository repo)
        {
            var user = new User { UserName = "admin"};

            foreach (var role in Roles)
            {
               roleManager.CreateAsync(role).Wait();
            }

            userManager.CreateAsync(user, "admin").Wait();
            
            userManager.AddToRoleAsync(
                  userManager.FindByNameAsync(user.UserName).Result, 
                  "admin"
                
               ).Wait();

            foreach (var category in Categories)
            {
               repo.SaveCategory(category);
            }

            foreach (var product in Products)
            {
                repo.SaveProduct(product);
            }
        }
    }
}
