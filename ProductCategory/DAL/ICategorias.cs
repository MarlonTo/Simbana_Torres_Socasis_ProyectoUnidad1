using Entities;
using Persistence.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public interface ICategorias
    {
        Task<Category?> GetCategoryById(int Id);
        Task<List<Category>> GetCategories();
        Task<string> CreateCategories(Category model);
        Task<string> EditCategories(Category model);
        Task<string> DeleteCategories(int Id);
    }

}
