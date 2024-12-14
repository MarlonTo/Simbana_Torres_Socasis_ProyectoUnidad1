using DAL;
using Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ProductCategory.Areas.Negocio.Controllers
{
    [Area("Negocio")]
    [Authorize(Roles = "User")]
    public class ProductosController : Controller
    {
        private readonly IProducts _products;
        private readonly ICategorias _categorias;

        public ProductosController(IProducts products, ICategorias categorias)
        {
            _products = products;
            _categorias = categorias;
        }

        public async Task<IActionResult> Index()
        {
            var productos = await _products.GetProducts();
            return View(productos);
        }

        [HttpGet]
        public async Task<IActionResult> CrearProducto(int id)
        {
            var categorias = await _categorias.GetCategories();
            ViewBag.Categorias = categorias.Select(m => new {Id = m.CategoryID, Nombre = m.CategoryName }).ToList();
            if (id != 0)
            {
                var Productos = await _products.GetProductById(id);
                return View(Productos);
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CrearProductoE(Product model)
        {
            if (model.ProductID != 0)
            {
                var edicion = await _products.EditProduct(model);
                if (edicion == "Éxito")
                {
                    return RedirectToAction("Index");

                }
                else
                {
                    return RedirectToAction("CrearProducto", new { id = model.ProductID });
                }
            }
            else
            {
                var crear = await _products.CreateProduct(model);
                if (crear == "Éxito")
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return View("CrearProducto");
                }
            }


        }

        [HttpGet]
        public async Task<IActionResult> EliminarProducto(int id)
        {
            var edicion = await _products.DeleteProduct(id);
            return RedirectToAction("Index");
        }
    }
}
