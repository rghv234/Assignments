using ProductServices.Models;

namespace ProductServices.Services
{
    public interface IProductService
    {
        IEnumerable<Product> GetAll();
        Product? GetById(int id);
        Product Add(Product product);
        bool Delete(int id);
        Product? Update(int id, Product updatedProduct);
    }
}
