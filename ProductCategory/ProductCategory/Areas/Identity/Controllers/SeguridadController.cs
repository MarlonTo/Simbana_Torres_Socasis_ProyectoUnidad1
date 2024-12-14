using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductCategory.Areas.Identity.Models;

namespace ProductCategory.Areas.Identity.Controllers
{
    [Area("Identity")]
    public class SeguridadController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public SeguridadController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> IniciarSesion(Usuario model)
        {
            try
            {
                var user = await _userManager.FindByNameAsync(model.UserName);
                if (user == null)
                {
                    throw new Exception($"Intento de inicio de sesión no válido.");
                }

                var resultado = await _signInManager.PasswordSignInAsync(model.UserName, model.PasswordHash, false, lockoutOnFailure: true);
                if (resultado.Succeeded)
                {
                    var roles = await _userManager.GetRolesAsync(user);

                    // Redirigir según el rol
                    if (roles.Contains("Admin"))
                    {
                        return RedirectToAction("Index", "Categorias", new {area = "Negocio"});
                    }
                    else if (roles.Contains("User"))
                    {
                        return RedirectToAction("Index", "Productos", new { area = "Negocio" });
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Error al inicar sesion");
                    return View("Login", model);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View("Login", model);
            }
           
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> CrearUsuarios()
        {
            ViewBag.Roles = _roleManager.Roles.Select(m=> new { Id = m.Id, Nombre = m.Name}).ToList(); // Obtiene la lista de roles
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CrearUsuarios(Usuario model)
        {
            try
            {
                var user = new IdentityUser
                {
                    UserName = model.UserName,
                    Email = model.Email,
                    PhoneNumber = model.PhoneNumber,
                };

                // Crear el usuario en la base de datos
                var result = await _userManager.CreateAsync(user, model.PasswordHash);

                if (result.Succeeded)
                {
                    var rol =await _roleManager.Roles.ToListAsync(); // Obtiene la lista de roles

                    var a = rol.FirstOrDefault(m => m.Id == model.IdRol.ToString());
                    await _userManager.AddToRoleAsync(user, a.Name);

                    // Redirigir a otra página
                    return RedirectToAction("Login", "Seguridad");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return View(model);
            }
            catch (Exception ex)
            {
                return Json(ex.Message);
            }

        }
    }
}
