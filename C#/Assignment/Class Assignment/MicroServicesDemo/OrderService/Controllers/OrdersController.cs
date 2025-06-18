using Microsoft.AspNetCore.Mvc;
using OrderServices.Models;
using OrderServices.Services;

namespace OrderServices.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _service;

        public OrdersController(IOrderService service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Order>> Get() => Ok(_service.GetAll());

        [HttpGet("{id}")]
        public ActionResult<Order> Get(int id)
        {
            var order = _service.GetById(id);
            return order is null ? NotFound() : Ok(order);
        }

        [HttpPost]
        public ActionResult<Order> Post(Order order)
        {
            var created = _service.Add(order);
            return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
        }
    }
}
