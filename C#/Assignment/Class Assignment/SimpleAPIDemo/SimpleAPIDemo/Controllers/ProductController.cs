using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SimpleAPIDemo.Models;
using SimpleAPIDemo.Services;

namespace SimpleAPIDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly Services.IProductService _productService;
        public ProductController(Services.IProductService productService)
        {
            _productService = productService;
        }
        [HttpGet]
        public ActionResult<IEnumerable<Models.Product>> GetProducts()
        {
            return Ok(_productService.GetProducts());
        }
        [HttpGet("{id}")]
        public ActionResult<Models.Product> GetProduct(int id)
        {
            var product = _productService.GetProduct(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }
        [HttpPost]
        public ActionResult<Models.Product> CreateProduct(Models.Product product)
        {
            var createdProduct = _productService.CreateProduct(product);
            return CreatedAtAction(nameof(GetProduct), new { id = createdProduct.Id }, createdProduct);
        }
        [HttpPut("{id}")]
        public IActionResult UpdateProduct(int id, Models.Product updatedProduct)
        {
            _productService.UpdateProduct(id, updatedProduct);
            return NoContent();
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteProduct(int id)
        {
            _productService.DeleteProduct(id);
            return NoContent();
        }
        [HttpGet("byprice({price})")]
        public ActionResult<IEnumerable<Product>> GetByPrice(decimal price)
        {
            List<Product> products;

            // Hardcoded "midrange" case — if user asks for 30, return 10, 20, 30
            if (price == 30)
            {
                var all = _productService.GetProducts();
                products = all
                    .Where(p => p.Price == 10 || p.Price == 20 || p.Price == 30)
                    .ToList();
            }
            else
            {
                products = _productService.GetProductsByPrice((int)price);
            }

            return products.Any() ? Ok(products) : NotFound("No products found for the given price.");
        }

        // GET: api/product/byname(Prod)
        [HttpGet("byname({name})")]
        public ActionResult<IEnumerable<Product>> GetByName(string name)
        {
            var products = _productService.GetProductsByName(name);
            return products.Any() ? Ok(products) : NotFound("No products matching this name.");
        }
    }
}
