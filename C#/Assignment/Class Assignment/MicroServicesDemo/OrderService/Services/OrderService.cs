using OrderServices.Models;

namespace OrderServices.Services
{
    public class OrderService : IOrderService
    {
        private readonly List<Order> _orders = new();
        private int _nextId = 1;

        public IEnumerable<Order> GetAll() => _orders;

        public Order? GetById(int id) => _orders.FirstOrDefault(o => o.Id == id);

        public Order Add(Order order)
        {
            order.Id = _nextId++;
            order.OrderDate = DateTime.UtcNow;
            _orders.Add(order);
            return order;
        }
    }
}
