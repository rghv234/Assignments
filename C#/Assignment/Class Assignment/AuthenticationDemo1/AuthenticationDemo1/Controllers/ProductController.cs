using AuthenticationDemo1.Models;
using AuthenticationDemo1.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthenticationDemo1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize] // Require JWT authentication
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepo _repo;

        public ProductsController(IProductRepo repo)
        {
            _repo = repo;
        }

        [HttpGet("AllProducts")]
        public async Task<IActionResult> Get()
        {
            try
            {
                var products = _repo.GetAllProducts();
                if (products == null)
                {
                    return NotFound(new { message = "No products found." });
                }
                return Ok(products);
            }
            catch (Exception ex)
            {

                throw new Exception("An error occurred while retrieving products.", ex);

            }
        }
        [HttpGet("productbyid/{id:int}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            try
            {
                var product = _repo.GetProductById(id);
                if (product == null)
                {
                    return NotFound(new { message = "Product not found." });
                }
                return Ok(product);
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while retrieving product with ID {id}.", ex);
            }
        }
        [HttpGet("productbyname/{name}")]
        public IActionResult GetProductByName(string name)
        {
            try
            {
                var products = _repo.GetProductByName(name);
                if (products == null || !products.Any())
                {
                    return NotFound(new { message = "No products found with the specified name." });
                }
                return Ok(products);
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while retrieving products with name {name}.", ex);
            }
        }
        [HttpGet("searchbyprice/{price:int}")]
        public IActionResult SearchProductsByPrice(int price)
        {
            try
            {
                var products = _repo.SearchProductsByPrice(price);
                if (products == null || !products.Any())
                {
                    return NotFound(new { message = "No products found with the specified price." });
                }
                return Ok(products);
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while searching for products with price {price}.", ex);
            }
        }
        [HttpPost("addproduct")]
        public async Task<IActionResult> AddProduct([FromBody] Product product)
        {
            if (product == null)
            {
                return BadRequest(new { message = "Product data is required." });
            }
            try
            {
                var result = _repo.AddProduct(product);
                return CreatedAtAction(nameof(GetProductById), new { id = product.Id }, result);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while adding the product.", ex);
            }
        }
        [HttpPut("updateproduct/{id:int}")]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] Product product)
        {
            if (product == null || id != product.Id)
            {
                return BadRequest(new { message = "Invalid product data." });
            }
            try
            {
                var result = _repo.UpdateProduct(id, product);
                if (result == null)
                {
                    return NotFound(new { message = "Product not found." });
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while updating product with ID {id}.", ex);
            }
        }
        [HttpDelete("deleteproduct/{id:int}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            try
            {
                var result = _repo.DeleteProduct(id);
                if (result == null)
                {
                    return NotFound(new { message = "Product not found." });
                }
                return Ok(new { message = "Product deleted successfully." });
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while deleting product with ID {id}.", ex);
            }
        }
    }
}
