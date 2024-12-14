using DAL;
using Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ProductCategory.Areas.Negocio.Controllers
{
    [Area("Negocio")]
    [Authorize(Roles ="Admin")]
    public class CategoriasController : Controller
    {
        private readonly ICategorias _categorias;

        public CategoriasController(ICategorias categorias)
        {
            _categorias = categorias;
        }

        public async Task<IActionResult> Index()
        {
            var categoria = await _categorias.GetCategories();
            return View(categoria);
        }

        [HttpGet]
        public async Task<IActionResult> CrearCategoria(int id)
        {
            if (id != 0)
            {
                var categoria = await _categorias.GetCategoryById(id);
                return View(categoria);
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CrearCategoriaE(Category model)
        {
            if (model.CategoryID != 0)
            {
                var edicion = await _categorias.EditCategories(model);
                if (edicion == "Éxito")
                {
                    return RedirectToAction("Index");

                }
                else
                {
                    return RedirectToAction("CrearCategoria", new { id = model.CategoryID });
                }
            }
            else
            {
                var crear = await _categorias.CreateCategories(model);
                if (crear == "Éxito")
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return View("CrearCategoria");
                }
            }


        }

        [HttpGet]
        public async Task<IActionResult> EliminarCategoria(int id)
        {
            var edicion = await _categorias.DeleteCategories(id);
            return RedirectToAction("Index");
        }


    }
}
