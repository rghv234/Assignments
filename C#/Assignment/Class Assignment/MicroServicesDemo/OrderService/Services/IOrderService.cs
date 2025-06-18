using OrderServices.Models;

namespace OrderServices.Services
{
    public interface IOrderService
    {
        IEnumerable<Order> GetAll();
        Order? GetById(int id);
        Order Add(Order order);
    }
}
