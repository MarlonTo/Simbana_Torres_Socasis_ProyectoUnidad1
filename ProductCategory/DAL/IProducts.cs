using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public interface IProducts
    {
        Task<Product?> GetProductById(int Id);
        Task<List<Product>> GetProducts();
        Task<string> CreateProduct(Product model);
        Task<string> EditProduct(Product model);
        Task<string> DeleteProduct(int Id);
    }
}
