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
    public class CategoriesLogic : ICategorias
    {
        private readonly ApplicationDbContext _context;
        public CategoriesLogic(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<string> CreateCategories(Category model)
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

        public async Task<string> DeleteCategories(int Id)
        {
            try
            {
                var category = await _context.Categories.FirstOrDefaultAsync(m=>m.CategoryID == Id);
                _context.Remove(category);
                await _context.SaveChangesAsync();
                return "Éxito";
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<string> EditCategories(Category model)
        {
            try
            {
                var categoria = await _context.Categories.FirstOrDefaultAsync(m => m.CategoryID == model.CategoryID);
                categoria.Description = model.Description;
                categoria.CategoryName = model.CategoryName;
                _context.Update(categoria);
                await _context.SaveChangesAsync();
                return "Éxito";
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<Category>> GetCategories()
        {
            return await _context.Categories.ToListAsync();
        }

        public async Task<Category?> GetCategoryById(int Id)
        {
            return await _context.Categories.FirstOrDefaultAsync(m => m.CategoryID == Id);
        }
    }
}
