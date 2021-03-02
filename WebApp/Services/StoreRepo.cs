using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Models;
using WebApp.Services;

namespace WebApp.Data
{
    public class StoreRepo : IStoreRepository
    {
        private StoreContext context;

        public StoreRepo(StoreContext context)
        {
            this.context = context;
        }

        public List<Order> GetOrders()
        {
            var orders = context.Orders
                .Include(o => o.Lines)
                .Include(o => o.courier)
                .OrderByDescending(o => o.Date)
                .ToList();
           
            return orders;  
        }

        public List<Product> GetProducts()
        {
            return context.Products.Include(p=>p.Category).ToList();
        }
       
        public List<Category> GetCategories()
        {
            return context.Categories.Include(c=>c.Products).ToList();
        }
       
        public List<Courier> GetCouriers()
        {
            return context.Couriers.Include(p=>p.personalOrders).ThenInclude(o => o.Lines).ToList();
        }
       
        public void SaveCourier(Courier person)
        {
           Courier dbEntry = context.Couriers
                    .FirstOrDefault(p => p.Id == person.Id);
                
            if (dbEntry != null)
            {
                    dbEntry.FirstName = person.FirstName;
                    dbEntry.LastName = person.LastName;
                    dbEntry.PhoneNumber = person.PhoneNumber;
                    dbEntry.personalOrders = person.personalOrders;
                  
            }
            else
            {
                context.Couriers.Add(person);
            }
            

            context.SaveChanges();
        }

        public void SaveProduct(Product product)
        {
            if (product.Id == 0)
            {
                context.Products.Add(product);
            }
            else
            {
                Product dbEntry = context.Products
                    .FirstOrDefault(p => p.Id == product.Id);
                if (dbEntry != null)
                {
                    dbEntry.Name = product.Name;
                    dbEntry.Description = product.Description;
                    dbEntry.Price = product.Price;
                    dbEntry.Category = product.Category;
                    dbEntry.ImgPath = product.ImgPath;
                }
            }
            
            context.SaveChanges();
        }
        public void SaveCategory(Category category)
        {


            Category dbEntry = context.Categories
                 .FirstOrDefault(c => c.Id == category.Id);
            if (dbEntry != null)
            {
                dbEntry.Name = category.Name;
                dbEntry.Products = category.Products;
                

            }
            else
            {
                context.Categories.Add(category);
            }


            context.SaveChanges();
        }
        public void SaveOrder(Order order)
        {
            if (order.Id == 0)
            {
                context.Orders.Add(order);
            }
            else
            {
                Order dbEntry = context.Orders.FirstOrDefault(o => o.Id == order.Id);
                if (dbEntry != null)
                {
                    dbEntry.Custumer = order.Custumer;
                    dbEntry.Adress = order.Adress;
                    dbEntry.Amount = order.Amount;
                    dbEntry.Comment = order.Comment;
                    dbEntry.Date = order.Date;
                    dbEntry.Lines = order.Lines;
                    dbEntry.PhoneNumber = order.PhoneNumber;
                    dbEntry.IsActive = order.IsActive;
                    dbEntry.IsDelivered = order.IsDelivered;
                    dbEntry.courier = order.courier;
                }
            }
            context.SaveChanges();
        }

        public void DeleteCategory(int categoryId)
        {
            Category toDelete = context.Categories.Include(c=>c.Products).FirstOrDefault(c => c.Id == categoryId);

            if (toDelete != null)
            {
                context.Categories.Remove(toDelete);
            }

            context.SaveChanges();
        }

        public void DeleteCourier(string courierId)
        {
            Courier toDelete = context.Couriers.Include(c=>c.personalOrders).FirstOrDefault(x => x.Id == courierId);

            if (toDelete != null)
            {
                context.Couriers.Remove(toDelete);
            }

            context.SaveChanges();
        }

        public void DeleteProduct(int productId)
        {
            Product toDelete = context.Products.FirstOrDefault(x => x.Id == productId);

            if (toDelete != null) 
            {
                context.Products.Remove(toDelete);  
            }
            
            context.SaveChanges();
        }

        public void DeleteOrder(int orderId)
        {
            Order toDelete = context.Orders
                .Include(o=>o.courier)
                .Include(o=>o.Lines)
                .FirstOrDefault(x => x.Id == orderId);

            if (toDelete != null)
            {
                context.Orders.Remove(toDelete);
            }
            
            context.SaveChanges();
        }
        
    }
}
