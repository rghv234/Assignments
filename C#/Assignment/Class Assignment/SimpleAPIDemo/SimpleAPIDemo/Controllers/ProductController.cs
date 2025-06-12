using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
    }
}
