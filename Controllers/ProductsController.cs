using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PruebaTecnicaRossmon.DTOs;
using PruebaTecnicaRossmon.Services;
using System.Linq;
using System.Threading.Tasks;

namespace PruebaTecnicaRossmon.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ProductService _productService;

        public ProductsController(ProductService productService)
        {
            _productService = productService;
        }

        // GET: Products
        public async Task<IActionResult> Index()
        {
            if (!IsAuthenticated())
                return RedirectToPage("/Account/Login", new { area = "Identity" });

            var products = await _productService.GetProducts();
            return View(products);
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int id)
        {
            if (!IsAuthenticated())
                return RedirectToPage("/Account/Login", new { area = "Identity" });

            var products = await _productService.GetProducts();

            var product = products.FirstOrDefault(p => p.Id == id);

            if (product == null)
                return NotFound();

            return View(product);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            if (!IsAuthenticated())
                return RedirectToPage("/Account/Login", new { area = "Identity" });

            return View();
        }

        // POST: Products/Create
        [HttpPost]
        public async Task<IActionResult> Create(CreateProductDto dto)
        {
            if (!ModelState.IsValid)
                return View(dto);

            var success = await _productService.CreateProduct(dto);

            if (!success)
            {
                ModelState.AddModelError("", "Error al crear producto");
                return View(dto);
            }

            return RedirectToAction("Index");
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            if (!IsAuthenticated())
                return RedirectToPage("/Account/Login", new { area = "Identity" });

            var products = await _productService.GetProducts();

            var product = products.FirstOrDefault(p => p.Id == id);

            if (product == null)
                return NotFound();

            var dto = new UpdateProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Stock = product.Stock
            };

            return View(dto);
        }

        // POST: Products/Edit
        [HttpPost]
        public async Task<IActionResult> Edit(UpdateProductDto dto)
        {
            if (!ModelState.IsValid)
                return View(dto);

            var success = await _productService.UpdateProduct(dto);

            if (!success)
            {
                ModelState.AddModelError("", "Error al actualizar");
                return View(dto);
            }

            return RedirectToAction("Index");
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            if (!IsAuthenticated())
                return RedirectToPage("/Account/Login", new { area = "Identity" });

            var products = await _productService.GetProducts();

            var product = products.FirstOrDefault(p => p.Id == id);

            if (product == null)
                return NotFound();

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var success = await _productService.DeleteProduct(id);

            if (!success)
            {
                ModelState.AddModelError("", "Error al eliminar");
                return View();
            }

            return RedirectToAction("Index");
        }

        private bool IsAuthenticated()
        {
            return !string.IsNullOrEmpty(HttpContext.Session.GetString("JWT"));
        }
    }
}