using MoneyMeApp.Data;
using MoneyMeApp.Interfaces;
using MoneyMeApp.Models;
using System.Collections.Generic;
using System.Linq;

namespace MoneyMeApp.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly DataContext context;

        public ProductRepository(DataContext context)
        {
            this.context = context;
            this.context.ChangeTracker.QueryTrackingBehavior = Microsoft.EntityFrameworkCore.QueryTrackingBehavior.NoTracking;
        }

        public ICollection<Product> GetProducts()
        {
            return this.context.Products.OrderBy(p => p.Id).ToList();
        }

        public Product GetProduct(int id)
        {
            return this.context.Products.FirstOrDefault(p => p.Id == id);
        }

        public Product GetProduct(string name)
        {
            return this.context.Products.FirstOrDefault(p => p.Name == name);
        }

        public Product AddProduct(Product product)
        {
            this.context.Products.Add(product);
            this.context.SaveChanges();

            return product;
        }

    }
}
