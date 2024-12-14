using DAL;
using Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class ProductsLogic : IProducts
    {
        private readonly ApplicationDbContext _context;
        public ProductsLogic(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<string> CreateProduct(Product model)
        {
            try
            {
                await _context.AddAsync(model);
                await _context.SaveChangesAsync();
                return "Éxito";

            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<string> DeleteProduct(int Id)
        {
            try
            {
                var Productos = await _context.Products.FirstOrDefaultAsync(m => m.ProductID == Id);
                _context.Remove(Productos);
                await _context.SaveChangesAsync();
                return "Éxito";
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<string> EditProduct(Product model)
        {
            try
            {
                var Productos = await _context.Products.FirstOrDefaultAsync(m => m.ProductID == model.ProductID);
                Productos.ProductName = model.ProductName;
                Productos.UnitPrice = model.UnitPrice;
                Productos.UnitsInStock = model.UnitsInStock;
                _context.Update(Productos);
                await _context.SaveChangesAsync();
                return "Éxito";
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<Product?> GetProductById(int Id)
        {
            return await _context.Products.FirstOrDefaultAsync(m => m.ProductID == Id);
        }

        public async Task<List<Product>> GetProducts()
        {
            return await _context.Products.Include(m=>m.Category).ToListAsync();
        }
    }
}
