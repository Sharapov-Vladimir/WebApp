using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Data;
using WebApp.Models;

namespace WebApp.Services
{
    public interface IStoreRepository
    {
        public List<Product> GetProducts();
        public List<Order> GetOrders();
        public List<Courier> GetCouriers();
        public List<Category> GetCategories();

        public void SaveProduct(Product product);
        public void DeleteProduct(int productId);
        public void SaveCategory(Category category);
        public void DeleteCategory(int categoryId);
        public void SaveOrder(Order order);
        public void DeleteOrder(int orderId);
        public void SaveCourier(Courier person);
        public void DeleteCourier(string courierId);

    }
}
