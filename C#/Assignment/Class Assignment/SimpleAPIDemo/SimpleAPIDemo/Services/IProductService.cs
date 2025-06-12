namespace SimpleAPIDemo.Services
{
    public interface IProductService
    {
        IEnumerable<Models.Product> GetProducts();
        Models.Product? GetProduct(int id);
        Models.Product CreateProduct(Models.Product product);
        void UpdateProduct(int id, Models.Product updatedProduct);
        void DeleteProduct(int id);
    }
}
