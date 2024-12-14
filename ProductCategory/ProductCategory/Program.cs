using BLL;
using DAL;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;

var builder = WebApplication.CreateBuilder(args);
var SecurityconnectionStrings = builder.Configuration.GetConnectionString("DefaultContext") ?? throw new InvalidOperationException("No se encontro connectionStrings");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(SecurityconnectionStrings));

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login"; // Ruta para la página de inicio de sesión
        options.LogoutPath = "/Account/Logout"; // Ruta para la página de cierre de sesión
        options.AccessDeniedPath = "/Account/AccessDenied"; // Página de acceso denegado
        options.ExpireTimeSpan = TimeSpan.FromMinutes(15); // Tiempo de expiración de la cookie
        options.SlidingExpiration = true; // Renovar automáticamente la cookie si está cerca de expirar
        options.Cookie.HttpOnly = true; // La cookie solo es accesible desde el servidor
        options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest; // Requiere HTTPS
        options.Cookie.SameSite = SameSiteMode.Strict; // Restringe el acceso de terceros a la cookie
    });

builder.Services.AddIdentity<IdentityUser, IdentityRole>(m =>
{
    m.Password.RequiredLength = 8;
    m.Password.RequireDigit = true;
    m.Password.RequireNonAlphanumeric = true;
    m.Password.RequireLowercase = true;
    m.Password.RequireUppercase = true;
    m.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    m.Lockout.MaxFailedAccessAttempts = 3;
    m.Lockout.AllowedForNewUsers = true;
}).AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();

builder.Services.AddScoped<IProducts, ProductsLogic>();   
builder.Services.AddScoped<ICategorias, CategoriesLogic>();

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    //pattern: "{area=Identity}/{controller=Cuenta}/{action=Index}/{id?}");
    pattern: "{area=Identity}/{controller=Seguridad}/{action=Login}/{id?}");

app.Run();
