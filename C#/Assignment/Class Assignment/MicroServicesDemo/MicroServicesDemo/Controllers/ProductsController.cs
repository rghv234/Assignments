using Microsoft.AspNetCore.Mvc;
using ProductServices.Models;
using ProductServices.Services;

namespace ProductServices.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _service;

        public ProductsController(IProductService service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Product>> Get() => Ok(_service.GetAll());

        [HttpGet("{id}")]
        public ActionResult<Product> Get(int id)
        {
            var product = _service.GetById(id);
            return product is null ? NotFound() : Ok(product);
        }

        [HttpPost]
        public ActionResult<Product> Post(Product product)
        {
            var created = _service.Add(product);
            return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public ActionResult<Product> Put(int id, Product product)
        {
            var updated = _service.Update(id, product);
            return updated is null ? NotFound() : Ok(updated);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var result = _service.Delete(id);
            return result ? NoContent() : NotFound();
        }
    }
}
