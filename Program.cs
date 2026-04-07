using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using PruebaTecnicaRossmon.Data;
using PruebaTecnicaRossmon.Services;

var builder = WebApplication.CreateBuilder(args);

// DB
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Identity 
builder.Services.AddDefaultIdentity<IdentityUser>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
})
.AddEntityFrameworkStores<ApplicationDbContext>();

// MVC
builder.Services.AddControllersWithViews();

// Conexión al api
builder.Services.AddHttpClient("Api", client =>
{
    client.BaseAddress = new Uri("https://localhost:7146/api/");
});

// Inyecta el servicio
builder.Services.AddScoped<ProductService>();

builder.Services.AddHttpContextAccessor();
builder.Services.AddSession();

var app = builder.Build();

// Pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseSession();

app.UseRouting();

// Seguridad
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

app.Run();
