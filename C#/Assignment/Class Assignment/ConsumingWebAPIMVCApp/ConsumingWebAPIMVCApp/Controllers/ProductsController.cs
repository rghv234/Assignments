using ConsumingWebAPIMVCApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace ConsumingWebAPIMVCApp.Controllers
{
    public class ProductsController : Controller
    {
        private readonly Services.ProductService _service;
        public ProductsController(Services.ProductService service)
        {
            _service = service;
        }
        public async Task<IActionResult> Index(string searchItem)
        {
            var products = string.IsNullOrEmpty(searchItem) ?
                await _service.GetProductsAsync() : await _service.SearchProductByName(searchItem);

            ViewBag.SearchItem = searchItem;
            return View(products);
        }

        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(Product product)
        {
            await _service.CreateProduct(product);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int id)
        {
            var product = await _service.GetProductById(id);
            return View(product);
        }
        public async Task<IActionResult> Details(int id)
        {
            var product = await _service.GetProductById(id);
            return View(product);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Product product)
        {
            await _service.UpdateProductAsync(product.Id, product);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            var product = await _service.GetProductById(id);
            return View(product);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _service.DeleteProduct(id);
            return RedirectToAction("Index");
        }

    }
}
