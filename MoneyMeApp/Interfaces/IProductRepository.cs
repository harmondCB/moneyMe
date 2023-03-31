using MoneyMeApp.Models;
using System.Collections.Generic;

namespace MoneyMeApp.Interfaces
{
    public interface IProductRepository
    {
        ICollection<Product> GetProducts();

        Product GetProduct(int id);

        Product GetProduct(string name);

        Product AddProduct(Product product);
    }
}
